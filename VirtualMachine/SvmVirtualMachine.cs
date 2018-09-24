using SVM.VirtualMachine.Debug;
using System.Reflection;

namespace SVM
{
    #region Using directives
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.IO;
    using SVM.VirtualMachine;
    using System.Linq;
    using System.Runtime.InteropServices;
    #endregion

    /// <summary>
    /// Implements the Simple Virtual Machine (SVM) virtual machine 
    /// </summary>
    public sealed class SvmVirtualMachine : IVirtualMachine
    {


        #region Constants
        private const string CompilationErrorMessage = "An SVM compilation error has occurred at line {0}.\r\n\r\n{1}";
        private const string RuntimeErrorMessage = "An SVM runtime error has occurred.\r\n\r\n{0}";
        private const string InvalidOperandsMessage = "The instruction \r\n\r\n\t{0}\r\n\r\nis invalid because there are too many operands. An instruction may have no more than one operand.";
        private const string InvalidLabelMessage = "Invalid label: the label {0} at line {1} is not associated with an instruction.";
        private const string ProgramCounterMessage = "Program counter violation; the program counter value is out of range";
        #endregion

        #region Fields
        private IDebugger debugger = null;
        private List<IInstruction> program = new List<IInstruction>();
        private Dictionary<string, int> programlabels = new Dictionary<string, int>();
        private List<int> breakpointList = new List<int>(); 
        private Stack stack = new Stack();
        private int programCounter = 0;
        #endregion

        #region Constructors

        /// <summary>
        /// https://blogs.msdn.microsoft.com/shawnfa/2004/06/07/checking-for-a-valid-strong-name-signature/
        /// Uses the information gathered from here to implement the below code used in SvmVirtualMachine && the strongNamesigver
        /// </summary>
        /// <param name="wszFilePath"></param>
        /// <param name="fForceVerification"></param>
        /// <param name="pfWasVerified"></param>
        /// <returns></returns>


        public SvmVirtualMachine()
        {
            #region Task 5 - Debugging 
            foreach (string dllsDirectoryPath in Directory.EnumerateFiles(Environment.CurrentDirectory, "*.dll"))
            {
                try
                {
                    bool notForced = false;
                    bool verified = SafeNativeMethods.StrongNameSignatureVerification(dllsDirectoryPath, false, ref notForced);
                    if (
                        (notForced && verified)
                        ||
                        (!notForced && verified)
                        ||
                        (!notForced && !verified)
                        )
                    {
                        Assembly assembly = Assembly.LoadFile(dllsDirectoryPath);
                        Type[] AllTypesInDll = assembly.GetTypes();
                        foreach (Type singleType in AllTypesInDll)
                        {

                            if (singleType.GetInterface("IDebugger") != null)
                            {
                                debugger = (IDebugger)Activator.CreateInstance(singleType);
                                debugger.VirtualMachine = this;
                                return;
                            }

                        }
                    }

                }
                catch (System.Exception ex)
                {
                }
            }
            // Do something here to find and create an instance of a type which implements 
            // the IDebugger interface, and assign it to the debugger field
            #endregion
        }
        #endregion

        /*
        static Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {

            return System.Reflection.Assembly.ReflectionOnlyLoad(args.Name);

        }*/ 
        #region Entry Point
        static void Main(string[] args)
        {
            //AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(CurrentDomain_ReflectionOnlyAssemblyResolve);

            if (CommandLineIsValid(args))
            {
                SvmVirtualMachine vm = new SvmVirtualMachine();
                try
                {
                    vm.Compile(args[0]);
                    vm.Run();
                }
                catch (SvmCompilationException)
                {
                }
                catch (SvmRuntimeException err)
                {
                    Console.WriteLine(RuntimeErrorMessage, err.Message);
                }
            }
            Console.ReadKey();
        }
        #endregion

        #region Properties
        /// <summary>
        ///  Gets a reference to the virtual machine stack.
        ///  This is used by executing instructions to retrieve
        ///  operands and store results
        /// </summary>
        public Stack Stack
        {
            get
            {
                return stack;
            }
        }

        /// <summary>
        /// Accesses the virtual machine 
        /// program counter (see programCounter in the Fields region).
        /// This can be used by executing instructions to 
        /// determine their order (ie. line number) in the 
        /// sequence of executing SML instructions
        /// </summary>
        public int ProgramCounter
        {
            #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
            get => programCounter;
            set => programCounter = value;

            #endregion
        }

        public Dictionary<string, int> ProgramLabels
        {
            get => programlabels;
            set => programlabels = value;
        }
        #endregion

        #region Public Methods

        #endregion

        #region Non-public Methods


        /// <summary>
        /// Reads the specified file and tries to 
        /// compile any SML instructions it contains
        /// into an executable SVM program
        /// </summary>
        /// <param name="filepath">The path to the 
        /// .sml file containing the SML program to
        /// be compiled</param>
        /// <exception cref="SvmCompilationException">
        /// If file is not a valid SML program file or 
        /// the SML instructions cannot be compiled to an
        /// executable program</exception>
        private void Compile(string filepath)
        {
            if (!File.Exists(filepath))
            {
                throw new SvmCompilationException("The file " + filepath + " does not exist");
            }

            int lineNumber = 0;
            try
            {
                using (StreamReader sourceFile = new StreamReader(filepath))
                {
                    while (!sourceFile.EndOfStream)
                    {
                        string instruction = sourceFile.ReadLine();
                        if (!String.IsNullOrEmpty(instruction) &&
                            !String.IsNullOrWhiteSpace(instruction))
                        {
                            ParseInstruction(instruction, lineNumber);
                            lineNumber++;
                        }
                    }
                }
            }
            catch (SvmCompilationException err)
            {
                Console.WriteLine(CompilationErrorMessage, lineNumber, err.Message);
                throw;
            }
        }
        /// <summary>
        /// Gives the 9 instructions before/after.. Safer than passing the entire List of instructions. 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private List<IInstruction> GetList(int number)
        {
            List<IInstruction> displaylist = new List<IInstruction>();
            for (int i = number-4; i <= number+4; i++)
            {
                if(i >= 0 && i < program.Count)
                {
                    displaylist.Add(program[i]);
                }
            }
            return displaylist;
        }

        /// <summary>
        /// Executes a compiled SML program 
        /// </summary>
        /// <exception cref="SvmRuntimeException">
        /// If an unexpected error occurs during
        /// program execution
        /// </exception>
        private void Run()
        {
            DateTime start = DateTime.Now;

            #region TASK 2 - TO BE IMPLEMENTED BY THE STUDENT

            for (ProgramCounter = 0; ProgramCounter < program.Count; ProgramCounter++)
            {
                if (breakpointList.Contains(ProgramCounter))
                {
                    IDebugFrame debugFrame = new DebuggerFrame(GetList(programCounter), program[ProgramCounter]);
                    debugger.Break(debugFrame);
                }
                program[ProgramCounter].VirtualMachine = this;
                program[ProgramCounter].Run();

            }
            #region TASKS 5 & 7 - MAY REQUIRE MODIFICATION BY THE STUDENT
            // For task 5 (debugging), you should construct a IDebugFrame instance and
            // call the Break() method on the IDebugger instance stored in the debugger field
            #endregion
            #endregion

            long memUsed = System.Environment.WorkingSet;
            TimeSpan elapsed = DateTime.Now - start;
            Console.WriteLine(String.Format(
                                        "\r\n\r\nExecution finished in {0} milliseconds. Memory used = {1} bytes",
                                        elapsed.Milliseconds,
                                        memUsed));
        }

        /// <summary>
        /// Parses a string from a .sml file containing a single
        /// SML instruction
        /// </summary>
        /// <param name="instruction">The string representation
        /// of an instruction</param>
        private void ParseInstruction(string instruction, int lineNumber)
        {
            #region TASK 5 & 7 - MAY REQUIRE MODIFICATION BY THE STUDENT
            #endregion
            
            string[] tokens = null;

            if (instruction.StartsWith("*"))
            {
                instruction = instruction.Remove(0,2);
                breakpointList.Add(lineNumber);
            }


            if (instruction.StartsWith("%"))
            {
                //specfically done so that you can put labels at either the end or the start
                // in the spec it doesn't say a label must be at the start or the end. 

                int a = instruction.IndexOf('%',0);
                int b = instruction.IndexOf('%', a+1);
                string label = instruction.Substring(a + 1, (b - a) - 1);
                programlabels[label]= lineNumber;

                instruction = instruction.Remove(a, (b - a)+1);

            }

            if (instruction.Contains("\""))
            {
               
                tokens = instruction.Split(new char[] { '\"' }, StringSplitOptions.RemoveEmptyEntries);

                // Remove any unnecessary whitespace
                for (int i = 0; i < tokens.Length; i++)
                {
                    tokens[i] = tokens[i].Trim();
                }
            }
            else
            {
                // Tokenize the instruction string by separating on spaces
                tokens = instruction.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }


            // Ensure the correct number of operands
            if (tokens.Length > 3)
            {
                throw new SvmCompilationException(String.Format(InvalidOperandsMessage, instruction));
            }

            


            switch (tokens.Length)
            {
                case 1:
                    program.Add(JITCompiler.CompileInstruction(tokens[0]));
                    break;
                case 2:
                    program.Add(JITCompiler.CompileInstruction(tokens[0], tokens[1].Trim('\"')));
                    break;
                case 3:
                    program.Add(JITCompiler.CompileInstruction(tokens[0], tokens[1].Trim('\"'), tokens[2].Trim('\"')));
                    break;
            }
            
        }



        #region Validate command line
        /// <summary>
        /// Verifies that a valid command line has been supplied
        /// by the user
        /// </summary>
        private static bool CommandLineIsValid(string[] args)
        {
            bool valid = true;

            if (args.Length != 1)
            {
                DisplayUsageMessage("Wrong number of command line arguments");
                valid = false;
            }

            if (valid && !args[0].EndsWith(".sml", StringComparison.CurrentCultureIgnoreCase))
            {
                DisplayUsageMessage("SML programs must be in a file named with a .sml extension");
                valid = false;
            }

            return valid;
        }

        /// <summary>
        /// Displays comamnd line usage information for the
        /// SVM virtual machine 
        /// </summary>
        /// <param name="message">A custom message to display
        /// to the user</param>
        static void DisplayUsageMessage(string message)
        {
            Console.WriteLine("The command line arguments are not valid. {0} \r\n", message);
            Console.WriteLine("USAGE:");
            Console.WriteLine("svm program_name.sml");
        }
        #endregion
        #endregion

        #region System.Object overrides
        /// <summary>
        /// Determines whether the specified <see cref="System.Object">Object</see> is equal to the current <see cref="System.Object">Object</see>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object">Object</see> to compare with the current <see cref="System.Object">Object</see>.</param>
        /// <returns><b>true</b> if the specified <see cref="System.Object">Object</see> is equal to the current <see cref="System.Object">Object</see>; otherwise, <b>false</b>.</returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Serves as a hash function for this type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="System.Object">Object</see>.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String">String</see> that represents the current <see cref="System.Object">Object</see>.
        /// </summary>
        /// <returns>A <see cref="System.String">String</see> that represents the current <see cref="System.Object">Object</see>.</returns>
        public override string ToString()
        {
            return base.ToString();
        }

        public void ChangeToLabel(string label)
        {
            if(programlabels.ContainsKey(label))
                programCounter = programlabels[label] -1;

        }
        #endregion

    }

    internal static class SafeNativeMethods
    {
        [DllImport("mscoree.dll", CharSet = CharSet.Unicode)]
        public static extern bool StrongNameSignatureVerification(string wszFilePath, bool fForceVerification, ref bool pfWasVerified);
    }
}

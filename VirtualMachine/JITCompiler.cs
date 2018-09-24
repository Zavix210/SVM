namespace SVM.VirtualMachine
{
    #region Using directives
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Diagnostics;
    using System.Linq;
    #endregion
    /// <summary>
    /// Utility class which generates compiles a textual representation
    /// of an SML instruction into an executable instruction instance
    /// </summary>
    internal static class JITCompiler
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Public methods
        public static void DispalyAssembly(Assembly a)
        {
            Console.WriteLine("*******Contents in Assembly*********");
            Console.WriteLine("Information:{0}", a.FullName);
            Type[] asm = a.GetTypes();
            foreach (Type tp in asm)
            {
                Console.WriteLine("Type:{0}", tp);
            }
        }
        #endregion

        #region Non-public methods
        internal static IInstruction CompileInstruction(string opcode)
        {
            IInstruction instruction = null;

            #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
            instruction = IInstructionCurrentAssembly(opcode);

            if (instruction == null)
            {
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
                                if (singleType.Name.ToLower() == opcode.ToLower())
                                {
                                    if (singleType.GetInterface("IInstruction") != null)
                                    {
                                        instruction = (IInstruction)Activator.CreateInstance(singleType);
                                        return instruction;
                                    }
                                }
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                    }
                }
            }

            #endregion
            if (instruction == null)
                throw new SvmRuntimeException(String.Format("Can not find opcode: {0}"
                                                , opcode));

            return instruction;
        }

        internal static IInstruction CompileInstruction(string opcode, params string[] operands)
        {
            IInstructionWithOperand instruction = null;

            #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT

            instruction = (IInstructionWithOperand)IInstructionCurrentAssembly(opcode, operands);
            if (instruction == null)
            {
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
                            //Assembly assembly = Assembly.ReflectionOnlyLoadFrom(dllsDirectoryPath);
                            Assembly assembly = Assembly.LoadFrom(dllsDirectoryPath);
                            Type[] AllTypesInDll = assembly.GetTypes();
                            foreach (Type singleType in AllTypesInDll)
                            {
                                if (singleType.Name.ToLower() == opcode.ToLower())
                                {
                                    if (singleType.GetInterface("IInstructionWithOperand") != null)
                                    {
                                        //Assembly assembly_act = Assembly.LoadFrom(dllsDirectoryPath);

                                        instruction = (IInstructionWithOperand)Activator.CreateInstance(assembly.GetType(singleType.FullName));
                                        instruction.Operands = operands;
                                        return instruction;
                                    }
                                }
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                    }
                }
            }
            #endregion
            if(instruction == null)
                throw new SvmRuntimeException(String.Format("Can not find opcode: {0}, Operands: {1}"
                                                , opcode, operands));
            return instruction;
        }
        #endregion

        /// <summary>
        /// Gets the current assembly and looks for the OPcode, if found returns instance of OPcode 
        /// if no OPcode found return null.
        /// </summary>
        /// <param name="opcode"></param>
        /// <returns></returns>
        internal static IInstruction IInstructionCurrentAssembly(string opcode)
        {
            var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();

            var allTypes = currentAssembly.GetTypes();
            IInstruction instruction = null;
            foreach (Type an in allTypes)
            {
                if (an.Name.ToLower() == opcode.ToLower())
                {
                    if (an.GetInterface("IInstruction") != null)
                    {
                        instruction = (IInstruction)Activator.CreateInstance(an);
                        return instruction;
                    }
                }
            }
            return instruction;
        }

        /// <summary>
        /// Gets the current assembly and looks for the OPcode if found 
        /// sets operands and returns if not return null.
        /// </summary>
        /// <param name="opcode"></param>
        /// <param name="operands"></param>
        /// <returns></returns>
        internal static IInstruction IInstructionCurrentAssembly(string opcode, string[] operands)
        {
            var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var allTypes = currentAssembly.GetTypes();
            IInstructionWithOperand instruction = null;
            foreach (Type an in allTypes)
            {

                if (an.Name.ToLower() == opcode.ToLower())
                {
                    if (an.GetInterface("IInstructionWithOperand") != null)
                    {

                        instruction = (IInstructionWithOperand)Activator.CreateInstance(an);
                        instruction.Operands = operands;
                        return instruction;
                    }
                }
            }
            return instruction;
        }
    }
}

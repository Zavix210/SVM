using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SVM.VirtualMachine.Debug
{
    public class DebuggerFrame : IDebugFrame
    {   
        public DebuggerFrame(List<IInstruction> instructionlist, IInstruction instruction)
        {

            CurrentInstruction = instruction;
            CodeFrame = instructionlist;
        }
        public IInstruction CurrentInstruction
        {
            get;
        }

        public List<IInstruction> CodeFrame
        {
            get;  
        }
    }
}

using SVM.VirtualMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SML_Extensions
{
    public class BltInt : BaseInstructionWithOperand
    {
        public override void Run()
        {
            if(Operands.Length != 2)
                throw new SvmRuntimeException(String.Format(BaseInstructionOperands.StackUnderflowMessage,
                                                this.ToString()));
            if (VirtualMachine.Stack.Count < 1 || Operands[0].Length < 1 || Operands[1].Length < 1)
            {
                throw new SvmRuntimeException(String.Format(BaseInstructionOperands.StackUnderflowMessage,
                                                this.ToString()));
            }
            if (Operands[0].GetType() != typeof(string) || Operands[1].GetType() != typeof(string))
            {
                throw new SvmRuntimeException(String.Format(BaseInstructionOperands.OperandOfWrongTypeMessage,
                                                this.ToString()));
            }


            int stackstring;

            try
            {
                stackstring = (int)VirtualMachine.Stack.Pop();
                VirtualMachine.Stack.Push(stackstring);
            }
            catch
            {
                throw new SvmRuntimeException(String.Format(BaseInstructionOperands.OperandOfWrongTypeMessage,
                                                this.ToString()));
            }


            if (int.TryParse(Operands[0], out int operand))
            {
                if (operand < stackstring)
                {
                    VirtualMachine.ChangeToLabel((string)Operands[1]);
                }
            }
            else
                throw new SvmRuntimeException(String.Format(BaseInstructionOperands.OperandOfWrongTypeMessage,
                                            this.ToString()));

        }
    }
}

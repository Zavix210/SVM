using SVM.VirtualMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SML_Extensions
{
    public class NotEqu : BaseInstructionWithOperand
    {
        public override void Run()
        {

            if (Operands[0].GetType() != typeof(string) || VirtualMachine.Stack.Count < 2)
            {
                throw new SvmRuntimeException(String.Format(BaseInstructionOperands.OperandOfWrongTypeMessage,
                                                this.ToString()));
            }

            int first;
            if (VirtualMachine.Stack.Peek() is int)
            { 
                first = (int)VirtualMachine.Stack.Pop();
                
            }
            else
                throw new SvmRuntimeException(String.Format(BaseInstructionOperands.OperandOfWrongTypeMessage,
                                                   this.ToString()));

            if (!(VirtualMachine.Stack.Peek() is int))
                throw new SvmRuntimeException(String.Format(BaseInstructionOperands.OperandOfWrongTypeMessage,
                                                   this.ToString()));
            if (first != (int)VirtualMachine.Stack.Peek())
            {
                VirtualMachine.Stack.Push(first);
                VirtualMachine.ChangeToLabel((string)Operands[0]);
            }

        }
    }
}

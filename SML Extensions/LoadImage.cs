using SVM.VirtualMachine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;

namespace SML_Extensions
{
    public class LoadImage : BaseInstructionWithOperand
    {
        public override void Run()
        {
            if(Operands.Length == 0 )
                throw new SvmRuntimeException(String.Format(BaseInstructionOperands.OperandOfWrongTypeMessage,
                                                this.ToString()));
            if (Operands[0].GetType() != typeof(string) || File.Exists(Operands[0]))
            {
                    throw new SvmRuntimeException(String.Format(BaseInstructionOperands.OperandOfWrongTypeMessage,
                                                    this.ToString()));
            }
            try
            {
                Image image1 = Image.FromFile(@"" + Operands[0], true);

                VirtualMachine.Stack.Push(image1);
            }
            catch (System.IO.FileNotFoundException)
            {
                throw new SvmRuntimeException(String.Format(BaseInstructionOperands.OperandOfWrongTypeMessage, this.ToString()));
            }

        }
    }
}

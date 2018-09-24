using SVM.VirtualMachine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SML_Extensions
{
    class DisplayImage : BaseInstructionOperands
    {
        public override void Run()
        {
            if (VirtualMachine.Stack.Count == 0 || !(VirtualMachine.Stack.Peek() is Image))
            {
                throw new SvmRuntimeException(String.Format(BaseInstructionOperands.StackUnderflowMessage,
                                                this.ToString()));
            }

            try
            {
                Image firstack = (Image) VirtualMachine.Stack.Pop();
                Display displayImage = new Display(firstack);
                displayImage.ShowDialog();
            }
            catch(SvmRuntimeException)
            {
                    throw new SvmRuntimeException(String.Format(BaseInstructionOperands.StackUnderflowMessage,
                                                  this.ToString()));
            }
        }
    }
}

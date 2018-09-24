namespace SVM.SimpleMachineLanguage
{
    #region Using directives
    using System;
    using SVM.VirtualMachine;
    #endregion
    /// <summary>
    /// Implements the SML Decr  instruction
    /// Decrements the integer value stored on top of the stack, 
    /// leaving the result on the stack
    /// </summary>
    public class Decr : BaseInstructionOperands
    {

        #region TASK 3 - TO BE IMPLEMENTED BY THE STUDENT
        public override void Run()
        {
            if (VirtualMachine.Stack.Count == 0)
            {
                throw new SvmRuntimeException(String.Format(BaseInstructionOperands.StackUnderflowMessage,
                                                this.ToString()));
            }
            var firstack = VirtualMachine.Stack.Pop();
            if (firstack is int)
            {
                firstack = (int)firstack - 1;
                VirtualMachine.Stack.Push(firstack);
            }
            else
            {
                throw new SvmRuntimeException(String.Format(BaseInstructionOperands.OperandOfWrongTypeMessage,
                                                this.ToString()));
            }
        }
        #endregion
    }
}

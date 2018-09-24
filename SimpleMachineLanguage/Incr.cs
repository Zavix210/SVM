namespace SVM.SimpleMachineLanguage
{
    #region Using directives
    using System;
    using SVM.VirtualMachine;
    #endregion
    /// <summary>
    /// Implements the SML Incr  instruction
    /// Increments the integer value stored on top of the stack, 
    /// leaving the result on the stack
    /// </summary>
    public class Incr : BaseInstructionOperands
    {
        #region TASK 3 - TO BE IMPLEMENTED BY THE STUDENT

        #endregion


        public override void Run()
        {
            if (VirtualMachine.Stack.Count == 0)
            {
                throw new SvmRuntimeException(String.Format(BaseInstructionOperands.StackUnderflowMessage,
                                                this.ToString()));
            }

            if (VirtualMachine.Stack.Peek() is int)
            {
                int firstack = (int)VirtualMachine.Stack.Pop();
                firstack = firstack + 1;
                VirtualMachine.Stack.Push(firstack);
            }
            else
            {
                throw new SvmRuntimeException(String.Format(BaseInstructionOperands.OperandOfWrongTypeMessage,
                                                this.ToString()));
            }
        }
    }
}

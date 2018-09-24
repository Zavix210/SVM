using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SVM.VirtualMachine;
using System.Collections;
using SVM.SimpleMachineLanguage;
using System.Diagnostics;
using SML_Extensions;
using System.Drawing;


namespace UnitTestProject2
{

    [TestClass]
    public class InstructionBGrintTests
    {

        [TestMethod]
        public void InstructionBGrint_Correct()
        {
            string[] opcodeInput = new string[] { "6", "addone" };
            int Myvalue = 5;

            Stack stack = new Stack();
            stack.Push(Myvalue);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);
            mock.Setup(p => p.ChangeToLabel(It.IsAny<string>()));
            BGrint remove = new BGrint
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();

            mock.Verify(x => x.ChangeToLabel(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void InstructionBGrint_CorrectNotCalled()
        {
            string[] opcodeInput = new string[] { "5", "addone" };
            int Myvalue = 6;

            Stack stack = new Stack();
            stack.Push(Myvalue);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);
            mock.Setup(p => p.ChangeToLabel(It.IsAny<string>()));
            BGrint remove = new BGrint
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();

            mock.Verify(x => x.ChangeToLabel(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionBGrint_InCorrectStack()
        {

            string[] opcodeInput = new string[] { "5", "addone" };

            Stack stack = new Stack();

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            BGrint remove = new BGrint
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();
        }

        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionBGrint_InCorrectStackItem()
        {

            string[] opcodeInput = new string[] { "5" };

            Stack stack = new Stack();
            stack.Push("j");
            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            BGrint remove = new BGrint
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();
        }

        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionBGrint_InCorrectStackOperand()
        {

            string[] opcodeInput = new string[] { "s", "addone" };

            Stack stack = new Stack();
            stack.Push(2);
            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            BGrint remove = new BGrint
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();
        }
    }

    [TestClass]
    public class InstructionNotEquTests
    {
        [TestMethod]
        public void InstructionNotEqu_CorrectNever()
        {
            string[] opcodeInput = new string[] {"addone" };
            int Myvalue = 5;
            int secondValue = 5;

            Stack stack = new Stack();
            stack.Push(Myvalue);
            stack.Push(secondValue);
            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);
            mock.Setup(p => p.ChangeToLabel(It.IsAny<string>()));
            NotEqu remove = new NotEqu
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();

            mock.Verify(x => x.ChangeToLabel(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void InstructionNotEqu_CorrectCalled()
        {
            string[] opcodeInput = new string[] {"addone" };
            int Myvalue = 6;
            int Myvalue2 = 10;

            Stack stack = new Stack();
            stack.Push(Myvalue);
            stack.Push(Myvalue2);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);
            mock.Setup(p => p.ChangeToLabel(It.IsAny<string>()));
            NotEqu remove = new NotEqu
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();

            mock.Verify(x => x.ChangeToLabel(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionNotEqu_InCorrectStack()
        {

            string[] opcodeInput = new string[] { "5", "addone" };

            Stack stack = new Stack();

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            NotEqu remove = new NotEqu
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();
        }

        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionNotEqu_InCorrectStackItem()
        {

            string[] opcodeInput = new string[] { "5" };

            Stack stack = new Stack();
            stack.Push("j");
            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            NotEqu remove = new NotEqu
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();
        }

        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionNotEqu_InCorrectStackOperand()
        {

            string[] opcodeInput = new string[] { "s", "addone" };

            Stack stack = new Stack();
            stack.Push(2);
            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            NotEqu remove = new NotEqu
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();
        }
    }

    [TestClass]
    public class InstructionEquIntTests
    {

        [TestMethod]
        public void InstructionEquint_Correct()
        {
            string[] opcodeInput = new string[] { "5", "addone" };
            int Myvalue = 5;

            Stack stack = new Stack();
            stack.Push(Myvalue);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);
            mock.Setup(p => p.ChangeToLabel(It.IsAny<string>()));
            EquInt remove = new EquInt
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();

            mock.Verify(x => x.ChangeToLabel(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void InstructionEquint_CorrectNotCalled()
        {
            string[] opcodeInput = new string[] { "5", "addone" };
            int Myvalue = 6;

            Stack stack = new Stack();
            stack.Push(Myvalue);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);
            mock.Setup(p => p.ChangeToLabel(It.IsAny<string>()));
            EquInt remove = new EquInt
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();

            mock.Verify(x => x.ChangeToLabel(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionEquInt_InCorrectStack()
        {

            string[] opcodeInput = new string[] { "5", "addone" };

            Stack stack = new Stack();

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            EquInt remove = new EquInt
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();
        }

        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionEquInt_InCorrectStackItem()
        {

            string[] opcodeInput = new string[] { "5" };

            Stack stack = new Stack();
            stack.Push("j");
            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            EquInt remove = new EquInt
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();
        }

        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionEquint_InCorrectStackOperand()
        {

            string[] opcodeInput = new string[] { "s", "addone" };

            Stack stack = new Stack();
            stack.Push(2);
            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            EquInt remove = new EquInt
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();
        }
    }

    [TestClass]
    public class InstructionBltIntTests
    {
        [TestMethod]
        public void InstructionBltInt_Correct()
        {
            string[] opcodeInput = new string[] { "5", "addone" };
            int Myvalue = 6; 

            Stack stack = new Stack();
            stack.Push(Myvalue);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);
            mock.Setup(p => p.ChangeToLabel(It.IsAny<string>()));
            BltInt remove = new BltInt
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();

            mock.Verify(x => x.ChangeToLabel(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionBltInt_InCorrectStack()
        {

            string[] opcodeInput = new string[] { "5", "addone" };

            Stack stack = new Stack();

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            BltInt remove = new BltInt
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();
        }

        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionBltInt_InCorrectStackItem()
        {

            string[] opcodeInput = new string[] { "5"};

            Stack stack = new Stack();
            stack.Push("j");
            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            BltInt remove = new BltInt
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();
        }

        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionBltInt_InCorrectStackOperand()
        {

            string[] opcodeInput = new string[] { "s", "addone" };

            Stack stack = new Stack();
            stack.Push(2);
            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            BltInt remove = new BltInt
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();
        }
    }

    [TestClass]
    public class InstructionRemoveTests
    {
        [TestMethod]
        public void InstructionRemove_Correct()
        {
            string opcodeInput = "5";
            Stack stack = new Stack();
            stack.Push(opcodeInput);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Remove remove = new Remove
            {
                VirtualMachine = mock.Object
            };
            remove.Run();

            Assert.AreEqual(mock.Object.Stack.Count, 0);
        }


        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionRemove_InCorrectStack()
        {
            Stack stack = new Stack();


            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Remove remove = new Remove
            {
                VirtualMachine = mock.Object
            };
            remove.Run();
        }


    }

    [TestClass]
    public class InstructionLoadStringTests
    {
        [TestMethod]
        public void InstructionLoadString_Correct()
        {
            string[] opcodeInput = new string[] { "5" };
            Stack stack = new Stack();

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            LoadString loadInt = new LoadString
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            loadInt.Run();

            Assert.AreEqual(mock.Object.Stack.Pop(), opcodeInput[0]);
        }

        // checks to see if no item on Operands
        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionLoadString_InCorrect()
        {
            string[] opcodeInput = new string[0];
            Stack stack = new Stack();

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            LoadString loadInt = new LoadString
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            loadInt.Run();

            Stack ExpectedStack = new Stack();
            ExpectedStack.Push(int.Parse(opcodeInput[0]));
        }
    }

    [TestClass]
    public class InstructionLoadIntTests
    {
        [TestMethod]
        public void InstructionLoadInt_Correct()
        {
            string[] opcodeInput = new string[] { "5" };
            Stack stack = new Stack();

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            LoadInt loadInt = new LoadInt
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            loadInt.Run();

            Stack ExpectedStack = new Stack();
            ExpectedStack.Push(int.Parse(opcodeInput[0]));

            Assert.AreEqual(mock.Object.Stack.Pop(), ExpectedStack.Pop());
        }

        // checks to see if no item on Operands
        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionLoadInt_InCorrect()
        {
            string[] opcodeInput = new string[0];
            Stack stack = new Stack();

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            LoadInt loadInt = new LoadInt
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            loadInt.Run();

            Stack ExpectedStack = new Stack();
            ExpectedStack.Push(int.Parse(opcodeInput[0]));

        }
    }

    [TestClass]
    public class InstructionSubtractTests
    {
        [TestMethod]
        public void InstructionSubtract_Correct()
        {
            int input = 5;
            int secondinput = 6;
            int expected = input - secondinput;
            Stack stack = new Stack();
            stack.Push(input);
            stack.Push(secondinput);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Subtract subtract = new Subtract
            {
                VirtualMachine = mock.Object
            };
            subtract.Run();

            Assert.AreEqual(mock.Object.Stack.Pop(), expected);
        }


        [TestMethod]
        public void InstructionSubtract_CorrectMultipleItems()
        {
            int input = 5;
            int secondinput = 6;
            int thridinput = 10;
            int expected = secondinput - thridinput;
            Stack stack = new Stack();
            stack.Push(input);
            stack.Push(secondinput);
            stack.Push(thridinput);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Subtract add = new Subtract
            {
                VirtualMachine = mock.Object
            };
            add.Run();

            Assert.AreEqual(mock.Object.Stack.Pop(), expected);
        }

        // checks to see if one item on stack throws exception
        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionSubtract_FailStack_One()
        {
            int input = 5;

            Stack stack = new Stack();
            stack.Push(input);
            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Subtract add = new Subtract
            {
                VirtualMachine = mock.Object
            };
            add.Run();
        }

        // checks to see if no items on stack throws exception
        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionAdd_FailStack()
        {
            Stack stack = new Stack();
            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Subtract add = new Subtract
            {
                VirtualMachine = mock.Object
            };
            add.Run();
        }

        // checks to see if the second input being wrong throws excpetion
        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionSubtract_Fail()
        {
            int input = 5;
            string secondinput = "6";

            Stack stack = new Stack();
            stack.Push(input);
            stack.Push(secondinput);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Subtract add = new Subtract
            {
                VirtualMachine = mock.Object
            };
            add.Run();
        }

        // checks to see if the first input being wrong throws excpetion
        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionSubtract_Failfirst()
        {
            string input = "6";
            int secondinput = 5;

            Stack stack = new Stack();
            stack.Push(input);
            stack.Push(secondinput);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Subtract add = new Subtract
            {
                VirtualMachine = mock.Object
            };
            add.Run();
        }
    }

    [TestClass]
    public class InstructionAddTests
    {
        //checks add actually adds
        [TestMethod]
        public void InstructionAdd_Correct()
        {
            int input = 5;
            int secondinput = 6;
            int expected = input + secondinput;
            Stack stack = new Stack();
            stack.Push(input);
            stack.Push(secondinput);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Add add = new Add
            {
                VirtualMachine = mock.Object
            };
            add.Run();

            Assert.AreEqual(mock.Object.Stack.Pop(), expected);
        }

        //checks to make sure the first two items on the stack are added, (has multiple items on stack) 
        [TestMethod]
        public void InstructionAdd_CorrectMultipleItems()
        {
            int input = 5;
            int secondinput = 6;
            int thridinput = 10;
            int expected = secondinput + thridinput;
            Stack stack = new Stack();
            stack.Push(input);
            stack.Push(secondinput);
            stack.Push(thridinput);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Add add = new Add
            {
                VirtualMachine = mock.Object
            };
            add.Run();

            Assert.AreEqual(mock.Object.Stack.Pop(), expected);
        }

        // checks to see if one item on stack throws exception
        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionAdd_FailStack_One()
        {
            int input = 5;

            Stack stack = new Stack();
            stack.Push(input);
            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Add add = new Add
            {
                VirtualMachine = mock.Object
            };
            add.Run();
        }

        // checks to see if no items on stack throws exception
        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionAdd_FailStack()
        {
            Stack stack = new Stack();
            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Add add = new Add
            {
                VirtualMachine = mock.Object
            };
            add.Run();
        }

        // checks to see if the second input being wrong throws excpetion
        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionAdd_Fail()
        {
            int input = 5;
            string secondinput = "6";

            Stack stack = new Stack();
            stack.Push(input);
            stack.Push(secondinput);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Add add = new Add
            {
                VirtualMachine = mock.Object
            };
            add.Run();
        }

        // checks to see if the first input being wrong throws excpetion
        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void InstructionAdd_Failfirst()
        {
            string input = "6";
            int secondinput = 5;

            Stack stack = new Stack();
            stack.Push(input);
            stack.Push(secondinput);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Add add = new Add
            {
                VirtualMachine = mock.Object
            };
            add.Run();
        }
    }

    [TestClass]
    public class InstrucitonDecrTests
    {

        //checks that integer is decreased by the value of one if the decr instruction is called.
        [TestMethod]
        public void InstructionDecr_Correct()
        {
            int input = 5;
            int expected = input - 1;
            Stack stack = new Stack();
            stack.Push(input);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Decr decr = new Decr
            {
                VirtualMachine = mock.Object
            };
            decr.Run();

            Assert.AreEqual(mock.Object.Stack.Pop(), expected);
        }

        //checks that the actual top item on the stack is being decreased. 
        [TestMethod]
        public void InstructionDecr_CorrectTopItemDecreased()
        {
            int input = 5;
            int secondinput = 10;
            int expected = secondinput - 1;
            Stack stack = new Stack();
            stack.Push(input);
            stack.Push(secondinput);
            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Decr decr = new Decr
            {
                VirtualMachine = mock.Object
            };
            decr.Run();

            Assert.AreEqual(mock.Object.Stack.Pop(), expected);
        }

        //checks to see if an exception is throwen when a string is given as the opperand
        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Decr] 1 )")]
        public void InstructionDecr_Fail()
        {
            string input = "j";

            Stack stack = new Stack();
            stack.Push(input);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);
            Decr decr = new Decr
            {
                VirtualMachine = mock.Object
            };
            decr.Run();
        }


        //checks that stack exception is throwen when stack is empty
        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "A stack underflow error has occurred. (at [line Decr] 1 )")]
        public void InstructionDecr_FailStack()
        {
            Stack stack = new Stack();

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);
            Decr decr = new Decr
            {
                VirtualMachine = mock.Object
            };
            decr.Run();
        }

    }

    [TestClass]
    public class InstrucitonIncrTests
    {
        // checks to see if a single integer is increased by one. 
        [TestMethod]
        public void InstructionIncr_Correct()
        {
            int input = 5;
            int expected = input + 1;
            Stack stack = new Stack();
            stack.Push(input);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Incr incr = new Incr
            {
                VirtualMachine = mock.Object
            };
            incr.Run();

            Assert.AreEqual(mock.Object.Stack.Pop(), expected);
        }

        //checks that the actual top item on the stack is being decreased.
        [TestMethod]
        public void InstructionIncr_CorrectTopItemIncreased()
        {
            int input = 5;
            int secondInput = 10; 
            int expected = secondInput + 1;
            Stack stack = new Stack();
            stack.Push(input);
            stack.Push(secondInput);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Incr incr = new Incr
            {
                VirtualMachine = mock.Object
            };
            incr.Run();

            Assert.AreEqual(mock.Object.Stack.Pop(), expected);
        }

        //checks to see if an exception is throwen when a string is given as the opperand
        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Incr] 1 )")]
        public void InstructionIncr_Fail_Type()
        {
            string input = "j";

            Stack stack = new Stack();
            stack.Push(input);

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);
            Incr incr = new Incr
            {
                VirtualMachine = mock.Object
            };
            incr.Run();
        }

        // checks to see if a exception is given when stack doesn't have enough items on it 
        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "A stack underflow error has occurred. (at [line Incr] 1 )")]
        public void InstructionIncr_FailStack()
        {
            Stack stack = new Stack();

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            Incr incr = new Incr
            {
                VirtualMachine = mock.Object
            };
            incr.Run();
        }
    }

    [TestClass]
    public class LoadImageTests
    {
        //Checks to see if an image is loaded correctly by comparing its byte array.
        [TestMethod]
        public void LoadImage_Correct()
        {
            string[] opcodeInput = new string[] { "C:\\Temp\\dog.jpg" };
            Stack stack = new Stack();

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            LoadImage remove = new LoadImage
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();

            Image image1 = Image.FromFile(@"" + opcodeInput[0], true);
            Image stackImage = (Image)mock.Object.Stack.Pop();
            ImageConverter imageConverter = new ImageConverter();

            byte[] imagefromtest = (byte[])imageConverter.ConvertTo(image1, typeof(byte[]));
            byte[] stackimagetest =(byte[]) imageConverter.ConvertTo(stackImage, typeof(byte[]));
            CollectionAssert.AreEqual(imagefromtest, stackimagetest);
        }

        // checks to see if IncorrectOperand is handled
        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void LoadImage_InCorrectOperand()
        {
            string[] opcodeInput = new string[] { "2" };
            Stack stack = new Stack();

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            LoadImage remove = new LoadImage
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();
        }


        // checks to see if no operands loading is handled
        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException), "The operand on the stack is of the wrong type. (at [line Add] 1 )")]
        public void LoadImage_NoOperands()
        {
            string[] opcodeInput = new string[0];
            Stack stack = new Stack();

            Mock<IVirtualMachine> mock = new Mock<IVirtualMachine>(MockBehavior.Strict);
            mock.Setup(p => p.Stack).Returns(stack);

            LoadImage remove = new LoadImage
            {
                VirtualMachine = mock.Object,
                Operands = opcodeInput
            };
            remove.Run();
        }
    }
}

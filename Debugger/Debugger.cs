using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVM.VirtualMachine;
using SVM.VirtualMachine.Debug;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace Debuggers
{
    public class Debugger : IDebugger
    {
        #region TASK 5 - TO BE IMPLEMENTED BY THE STUDENT
        #endregion
        private IVirtualMachine virtualMachine = null;

        public IVirtualMachine VirtualMachine { set => virtualMachine = value; }
        private BoolObject boolObject = new BoolObject();
        private ThreadingTest threading;

        private Thread GetThread;
        private SML_Debugger sML_Debugger;
        private delegate void UpdateUI(List<IInstruction> codef, IInstruction curr, Stack st);
        public void Break(IDebugFrame debugFrame)
        {

            if (GetThread == null || !GetThread.IsAlive)
            {
                boolObject.GetturnOff = true;


                sML_Debugger = new SML_Debugger(boolObject, debugFrame.CodeFrame, debugFrame.CurrentInstruction, virtualMachine.Stack);
                threading = new ThreadingTest(sML_Debugger, boolObject);

                ThreadStart threadStart = new ThreadStart(threading.StartUiThread);
                GetThread = new Thread(threadStart);
                GetThread.Start();
            }
            else
            {
                lock (boolObject)
                {
                    boolObject.GetturnOff = true;
                    UpdateUI updateui = new UpdateUI(sML_Debugger.Update);
                    sML_Debugger.Invoke(updateui, debugFrame.CodeFrame, debugFrame.CurrentInstruction, virtualMachine.Stack);
                }
            }
  
            while (boolObject.GetturnOff)
            {
                Thread.Sleep(100); // sleep for one second and keep checking back to see if they have pressed contuine 
            }
        }
    }

    public class BoolObject
    {
        private bool turnOff;

        public bool GetturnOff
        {
            get
            {
                return turnOff;
            }
            set
            {
                turnOff = value;
            }
        }
    }
    public class ThreadingTest
    {
        BoolObject boolObject;
        SML_Debugger sML;
       
        public ThreadingTest(SML_Debugger sML_Debugger, BoolObject bo)
        {
            sML = sML_Debugger;
            boolObject = bo;
        }

        public void StartUiThread()
        {
            Application.Run(sML);
            boolObject.GetturnOff = false;
        }
    }
}

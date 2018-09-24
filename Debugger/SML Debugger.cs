using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SVM.VirtualMachine;
using System.Collections;

namespace Debuggers
{
    public partial class SML_Debugger : Form
    {
        private List<IInstruction> codeFrame;
        private IInstruction currentInstruction;
        private BoolObject boolObject_sent;
        
        public void Update(List<IInstruction> codeFrame, IInstruction currentInstruction, Stack stack)
        {
            button1.Enabled = true;
            listView1.Clear();
            listView2.Clear();
            listView1.View = View.Details;
            listView1.Columns.Add("Instructions: ", 280, HorizontalAlignment.Center);

            foreach (IInstruction x in codeFrame)
            {
                if (x == currentInstruction)
                {
                    ListViewItem selected = new ListViewItem(x.ToString())
                    {
                        UseItemStyleForSubItems = false,
                        BackColor = Color.DarkSeaGreen,
                        ForeColor = Color.DarkRed
                    };
                    listView1.Items.Add(selected);
                }
                else
                {
                    ListViewItem items = new ListViewItem(x.ToString());
                    listView1.Items.Add(items);
                }

            }

            listView2.View = View.Details;
            listView2.Columns.Add("Stack: ", listView2.Width - 4, HorizontalAlignment.Center);

            foreach (object x in stack)
            {
                ListViewItem items = new ListViewItem(x.ToString());
                listView2.Items.Add(items);
               
            }

            this.codeFrame = codeFrame;
            this.currentInstruction = currentInstruction;
            this.Refresh();
        }

        public SML_Debugger(BoolObject boolObject, List<IInstruction> codeFrame, IInstruction currentInstruction, System.Collections.Stack stack)
        {
            boolObject_sent = boolObject;
            InitializeComponent();
            listView1.View = View.Details;
            listView1.Columns.Add("Instructions: ", 280, HorizontalAlignment.Center);

            foreach (IInstruction x in codeFrame)
            {
                if (x == currentInstruction)
                {
                    ListViewItem selected = new ListViewItem(x.ToString())
                    {
                        UseItemStyleForSubItems = false,
                        BackColor = Color.DarkSeaGreen,
                        ForeColor = Color.DarkRed
                    };
                    listView1.Items.Add(selected);
                }
                else
                {
                    ListViewItem items = new ListViewItem(x.ToString());
                    listView1.Items.Add(items);
                }

            }

            listView2.View = View.Details;
            listView2.Columns.Add("Stack: ", listView2.Width - 4, HorizontalAlignment.Center);

            foreach (object x in stack)
            {
                ListViewItem items = new ListViewItem(x.ToString());
                listView2.Items.Add(items); 
            }

            this.codeFrame = codeFrame;
            this.currentInstruction = currentInstruction;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            lock(boolObject_sent)
            {
                boolObject_sent.GetturnOff = false;
            }
        }
    }
}

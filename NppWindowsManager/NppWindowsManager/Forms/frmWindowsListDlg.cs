using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kbg.NppPluginNET;
using Kbg.NppPluginNET.PluginInfrastructure;

namespace Gdi.TabsManager
{
    public partial class frmWindowsListDlg : Form
    {
        public event EventHandler<CustomEventArgs> SetActiveDocument;
        public frmWindowsListDlg()
        {
            InitializeComponent();
        }

        internal void AddToList(string file)
        {
            listBox1.Items.Add(file);
            listBox1.Update();
            listBox1.Refresh();
        }

        internal void ClearList()
        {
            listBox1.Items.Clear();
            listBox1.Update();
            listBox1.Refresh();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var itemText = listBox1.GetItemText(listBox1.SelectedItem);
            SetActiveDocument?.Invoke(this, new CustomEventArgs(itemText, listBox1.SelectedIndex));
            listBox1.Update();
            listBox1.Refresh();
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            listBox1.Update();
            listBox1.Refresh();
            this.Invalidate();
        }

        private void FrmWindowsListVisibleChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_SETMENUITEMCHECK,
                                  PluginBase._funcItems.Items[Main.idMyDlg]._cmdID, 0);
            }
        }
    }
    public class CustomEventArgs : EventArgs
    {
        public CustomEventArgs(string message, int index)
        {
            Message = message;
            Index = index;
        }

        public string Message { get; set; }

        public int Index { get; set; }
    }
}

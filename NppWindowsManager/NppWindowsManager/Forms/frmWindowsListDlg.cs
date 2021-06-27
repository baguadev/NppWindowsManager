using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
        }

        internal void ClearList()
        {
            listBox1.Items.Clear();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var itemText = listBox1.GetItemText(listBox1.SelectedItem);
            SetActiveDocument?.Invoke(this, new CustomEventArgs(itemText, listBox1.SelectedIndex));
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotNetList.Core;
using Microsoft.FSharp.Collections;
using DotNetList.FunctionalExtensions;

namespace DotNetList
{
    public partial class Form1 : Form
    {
        private List<string> Elements = new List<string>();
        private MyList myList = new MyList();

        private Mode mode = Mode.Insert;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mode == Mode.Insert)
            {
                string newElement = textBox1.Text;
                if (!string.IsNullOrWhiteSpace(newElement))
                {
                    if (myList.FindElementInList(newElement, Elements).IsNone())
                    {
                        Elements = myList.AddElementToList(newElement, Elements);
                        UpdateList();
                    }
                }
            }
            else if (mode == Mode.Remove)
            {
                int index = listBox1.SelectedIndex;
                Elements = myList.RemoveElementToList(index, Elements);
                UpdateList();
                mode = Mode.Insert;
                button1.Text = "Add";
            }
        }

        private void UpdateList()
        {
            BindingList<string> bl = new BindingList<string>(Elements);
            listBox1.DataSource = bl;
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            mode = Mode.Remove;
            button1.Text = "Remove";
        }
    }

    public enum Mode
    {
        Insert,
        Remove
    }
}

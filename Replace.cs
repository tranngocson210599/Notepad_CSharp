using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Replace : Form
    {
        static class Functions
        {
            public static int GoToLineNumber { get; set; }

            public static int MaxNumberOfLines { get; set; }

            public static string TextToFind { get; set; }

            public static string ReplacementText { get; set; }
        }
        public Replace()
        {
            InitializeComponent();
        }
        public bool FindNextClicked { get; private set; }
        public bool ReplaceAllClicked { get; private set; }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                //Indicate what button the user has clicked
                FindNextClicked = true;
                ReplaceAllClicked = false;
                //And set the functions text to find property to the entered text
                Functions.TextToFind = textBox1.Text;
            }
        }
    }
}

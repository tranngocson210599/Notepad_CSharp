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
    public partial class Find : Form
    {
        public Find()
        {
            InitializeComponent();
        }
        static class Functions
        {
            public static int GoToLineNumber { get; set; }

            public static int MaxNumberOfLines { get; set; }

            public static string TextToFind { get; set; }

            public static string ReplacementText { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Functions.TextToFind = textBox1.Text;
            //Close the form
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Find_Load(object sender, EventArgs e)
        {
                if (Functions.TextToFind != null)
                textBox1.Text = Functions.TextToFind;
        }
    }
}

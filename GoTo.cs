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
    public partial class GoTo : Form
    {
        public GoTo()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        static class Functions
        {
            public static int GoToLineNumber { get; set; }

            public static int MaxNumberOfLines { get; set; }

            public static string TextToFind { get; set; }

            public static string ReplacementText { get; set; }
           // public bool GoToClicked { get; private set; }
        }
        public bool GoToClicked { get; private set; }
        private void lineTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Let the user input only numbers
            int isNumber = 0;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBox1.Text) > Functions.MaxNumberOfLines)
                MessageBox.Show("The line number is beyond the total number of lines");
            else
            {
                //Set the go to indicator to true
                GoToClicked = true;
                //Pass the value to the GoToLineNumber property
                Functions.GoToLineNumber = int.Parse(textBox1.Text);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GoToClicked = false;
            this.Close();
        }

        private void GoTo_Load(object sender, EventArgs e)
        {
            if (Functions.GoToLineNumber != 0)
                textBox1.Text = Functions.GoToLineNumber.ToString();
        }
    }
}

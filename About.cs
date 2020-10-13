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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            label1.Text = string.Format("Name : {0}", Application.ProductName);
            label2.Text = string.Format("Version : {0}", Application.ProductVersion);
            label3.Text = string.Format("Copyright 2020");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

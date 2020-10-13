using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
namespace Notepad
{
   
    public partial class Notepad : Form
    {
        string path;
        private void StatusBarChange()
        {
           
            if (statusStrip1.Visible)
            {
                
                statusBarToolStripMenuItem.Checked = false;
              
                statusStrip1.Visible = false;
               
            }
            else
            {
                
                statusBarToolStripMenuItem.Checked = true;
               
                statusStrip1.Visible = true;
            }
        }
        private void StatusBarUpdate()
        {
            
            int statusBarLine = richTextBox1.GetLineFromCharIndex(richTextBox1.GetFirstCharIndexOfCurrentLine());
           
            int statusBarColumn = richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine();
           
            toolStripStatusLabel1.Text = "Ln " + statusBarLine.ToString() + ", Col " + statusBarColumn.ToString();
            
        }
        public Notepad()
        {
            InitializeComponent();
        }
        public void Goto(int line) 

        {
            int index;
            index = richTextBox1.GetFirstCharIndexFromLine(line - 1);

            richTextBox1.Select(index, 0);

            richTextBox1.Focus();

        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            path = string.Empty;
            richTextBox1.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog of = new OpenFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true, Multiselect = false })
            {
                if (of.ShowDialog() == DialogResult.OK)
                    using (StreamReader sr = new StreamReader(of.FileName))
                    {
                        path = of.FileName;
                        Task<string> text = sr.ReadToEndAsync();
                        richTextBox1.Text = text.Result;
                    }

            }
        }

        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(path))
            {
                using (SaveFileDialog sf = new SaveFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true })
                {
                    if (sf.ShowDialog() == DialogResult.OK)
                        using (StreamWriter sw = new StreamWriter(sf.FileName))
                        {
                            await sw.WriteLineAsync(richTextBox1.Text);
                        }

                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    await sw.WriteLineAsync(richTextBox1.Text);
                }
            }
        }

        private async void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(path))
            {
                using (SaveFileDialog sf = new SaveFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true })
                {
                    if (sf.ShowDialog() == DialogResult.OK)
                        using (StreamWriter sw = new StreamWriter(sf.FileName))
                        {
                            await sw.WriteLineAsync(richTextBox1.Text);
                        }

                }
            }
        }

        private async void eXitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length == 0)
                Application.Exit();
            if (richTextBox1.Modified == true)

            {
                DialogResult dr = MessageBox.Show("Do you want to save change", "Don't save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                   
                    if (string.IsNullOrEmpty(path))
                    {
                        using (SaveFileDialog sf = new SaveFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true })
                        {
                            if (sf.ShowDialog() == DialogResult.OK)
                                using (StreamWriter sw = new StreamWriter(sf.FileName))
                                {
                                    await sw.WriteLineAsync(richTextBox1.Text);
                                }

                        }
                    }
                    else
                    {
                        using (StreamWriter sw = new StreamWriter(path))
                        {
                            await sw.WriteLineAsync(richTextBox1.Text);
                        }
                    }
                    richTextBox1.Modified = false;
                    Application.Exit();
                }

                else

                {
                    richTextBox1.Modified = false;
                    Application.Exit();

                }

            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo)
                richTextBox1.Undo();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut(); 
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();   
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                System.Diagnostics.Process.Start("https://www.bing.com/search?q=get+help+with+notepad+in+windows+10&filters=guid:%224466414-en-dia%22%20lang:%22en%22&form=T00032&ocid=HelpPane-BingIA");

            }   
            catch
            {
                MessageBox.Show("Unable to open link that was clicked.");
            }
            
        }

        

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.StatusBarChange();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }
        static class Functions
        {
            public static int GoToLineNumber { get; set; }

            public static int MaxNumberOfLines { get; set; }

            public static string TextToFind { get; set; }

            public static string ReplacementText { get; set; }
        }
        private void Find (string textToFind, ref Find findForm)
        {
           
            if (richTextBox1.Text.IndexOf(textToFind) == -1)
            {
                
                MessageBox.Show("Cannot find '" + textToFind + " '");
               
                findForm.ShowDialog();
            }
            else
            {
               
                richTextBox1.SelectionStart = richTextBox1.Text.IndexOf(Functions.TextToFind);
               
                richTextBox1.SelectionLength = textToFind.Length;
            }
        }
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Find findForm = new Find();
            //Show the find dialog
            findForm.ShowDialog();
            Find(Functions.TextToFind, ref findForm);
        }

        private void aboutNotepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (About abt = new About())
                abt.ShowDialog();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

            PrintDialog Prt = new PrintDialog();
          //  Prt.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            Prt.ShowDialog();

            
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            PageSetupDialog pgSetup = new PageSetupDialog();
            pgSetup.PageSettings = new System.Drawing.Printing.PageSettings();
            pgSetup.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            pgSetup.ShowNetwork = false;
            pgSetup.ShowDialog();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.StatusBarUpdate();
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void TextBoxModeChange()
        {

            //Set the textbox wordwrap poperty in accordance to the word wrap menu item
            richTextBox1.WordWrap = wordWrapToolStripMenuItem.Checked;
            //Set the scroll bars in accordance to the textbox mode
           // richTextBox1.ScrollBars = wordWrapToolStripMenuItem.Checked ? ScrollBars.Vertical : ScrollBars.Both;
        }
        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
           



        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length > 0)

            {
                //  cutToolStripMenuItem.Enabled = true;
                pasteToolStripMenuItem.Enabled = true;
                undoToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
               // findNextToolStripMenuItem.Enabled = true;
               // findToolStripMenuItem.Enabled = true;
              //  goToToolStripMenuItem.Enabled = true;
            }
            else
            {
                //  cutToolStripMenuItem.Enabled = false;
                pasteToolStripMenuItem.Enabled = false;// tuong ung voi copy
                undoToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
               // findNextToolStripMenuItem.Enabled = false;
               // findToolStripMenuItem.Enabled = false;
               // goToToolStripMenuItem.Enabled = false;
            }
            if (richTextBox1.Text.Length > 0)
            
            {

                findNextToolStripMenuItem.Enabled = true;
                findToolStripMenuItem.Enabled = true;

            }
            else
            {

                findNextToolStripMenuItem.Enabled = false;
                findToolStripMenuItem.Enabled = false;

            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
          FontDialog fontdig = new FontDialog();
            if(fontdig.ShowDialog()==DialogResult.OK)
            {
                if (richTextBox1.SelectedText != "")
                {
                    richTextBox1.Font = fontdig.Font;
                }
            }

        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            richTextBox1.AppendText(DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToShortTimeString().ToString());
        }
        private void ReplaceAll(string textToReplace, string replacementText)
        {
            
            Regex regex = new Regex(textToReplace);
            
            string finishText = regex.Replace(richTextBox1.Text, replacementText);
           
            if (richTextBox1.Text == finishText)
                MessageBox.Show("Nothing was replaced, because there were no words to replace!");
            else
            {
               
                richTextBox1.Text = finishText;
                
                MessageBox.Show("'" + textToReplace + "' was replaced by '" + replacementText + "'");
            }
        }
        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Replace replaceForm = new Replace();
            
            replaceForm.ShowDialog();
            if (replaceForm.FindNextClicked)
            {
                
                Find findForm = new Find();
                Find(Functions.TextToFind, ref findForm);
            }
            else if (replaceForm.ReplaceAllClicked)
                ReplaceAll(Functions.TextToFind, Functions.ReplacementText);
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Functions.MaxNumberOfLines = richTextBox1.Lines.Count();
            GoTo goToForm = new GoTo();
            goToForm.ShowDialog();
            if (goToForm.GoToClicked)
            {
                richTextBox1.SelectionStart = richTextBox1.GetFirstCharIndexFromLine(Functions.GoToLineNumber - 1);
                richTextBox1.SelectionLength = 0;
            }
            
        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Notepad_Load(object sender, EventArgs e)
        {
            this.StatusBarUpdate();
        }
    }
 
}

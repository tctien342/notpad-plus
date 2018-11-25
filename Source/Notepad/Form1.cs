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

namespace Notepad
{
    public partial class Form1 : Form
    {
        // Init variable
        public static Form1 instance;
        String filePath = "";
        Find findForm;
        Replace replaceForm;

        // Const variable
        String appName = "NotepadPlus";
        String defaultFileName = "undified_text";
        String aboutMeTitle = "About us";
        String aboutMeSub = "An notepad clone made by Tran Cong Tien, MSSV: 15520889";
        int searchStarPos = 0;

        // Constuctor
        public Form1()
        {
            InitializeComponent();
            findNextToolStripMenuItem.Enabled = false;
            toolStripStatusLabel1.Text = "LN " + 1;
            toolStripStatusLabel2.Text = "COL " + 0;

            this.Text = appName;
            instance = this;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // Get the line.
            int index = richTextBox1.SelectionStart;
            int line = richTextBox1.GetLineFromCharIndex(index);

            // Get the column.
            int firstChar = richTextBox1.GetFirstCharIndexFromLine(line);
            int column = index - firstChar;

            toolStripStatusLabel1.Text = "LN " + (line + 1);
            toolStripStatusLabel2.Text = "COL " + column;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                String text = File.ReadAllText(filePath);
                this.Text = openFileDialog1.SafeFileName + " - " + appName;
                richTextBox1.Text = text;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            this.Close();
        }

        
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filePath != "")
            {
                TextWriter textWriter = new StreamWriter(filePath);
                textWriter.Write(richTextBox1.Text);
                textWriter.Close();
            }
            else
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = defaultFileName + ".txt";
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.ShowDialog();
            filePath = saveFileDialog1.FileName;
            String fName = saveFileDialog1.FileName;
            StreamWriter streamWriter = new StreamWriter(fName);
            streamWriter.Write(richTextBox1.Text);
            streamWriter.Flush();
            streamWriter.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Find.instance == null)
                findForm = new Find();
            if (Find.instance != null)
                findForm.Show();
            findForm.Focus();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsText()) {
                richTextBox1.Cut();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += DateTime.Now.ToString();
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.Checked == false)
            {
                wordWrapToolStripMenuItem.Checked = true;
                richTextBox1.WordWrap = true;
            }
            else
            {
                wordWrapToolStripMenuItem.Checked = false;
                richTextBox1.WordWrap = false;
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            richTextBox1.Font = fontDialog1.Font;
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip1.Visible = !statusStrip1.Visible;
        }

        private void aboutMeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(aboutMeSub, aboutMeTitle);
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Replace.instance == null)
                replaceForm = new Replace();
            if (Replace.instance != null)
                replaceForm.Show();
            replaceForm.Focus();
        }
        
        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String text = Find.instance.textBox1.Text;
            try
            {
                richTextBox1.Find(text, searchStarPos, RichTextBoxFinds.None);
                richTextBox1.Select(searchStarPos, text.Length);
            }
            catch
            {
                MessageBox.Show("No Matches Found");
                searchStarPos = 0;
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}

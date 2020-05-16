using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            trackBar1.Scroll += trackBar1_Scroll;

            trackBar1.Value = Convert.ToInt32(richTextBox1.Font.Size);            

            var installedFontCollection = new InstalledFontCollection();
            foreach (var fontFamily in installedFontCollection.Families)
            {
                comboBox2.Items.Add(fontFamily.Name);
            }

            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            FontStyle style = GetFontStyle();
            textBox1.Text = trackBar1.Value.ToString();
            label1.Text = String.Format("Текущий размер шрифта: {0}", trackBar1.Value);

            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, trackBar1.Value, style);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            FontStyle style = GetFontStyle();
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, trackBar1.Value, style);          
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            FontStyle style = GetFontStyle();
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, trackBar1.Value, style);
        }

        private FontStyle GetFontStyle()
        {
            FontStyle style;


            if(checkBox1.Checked && checkBox2.Checked)
            {
                style = FontStyle.Bold | FontStyle.Italic;
            } else if (checkBox1.Checked && !checkBox2.Checked)
            {
                style = FontStyle.Bold;
            }
            else if (!checkBox1.Checked && checkBox2.Checked)
            {
                style = FontStyle.Italic;
            } else
            {
                style = FontStyle.Regular;
            }

            return style;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FontStyle style = GetFontStyle();

            string selectedState = comboBox2.SelectedItem.ToString();
            richTextBox1.Font = new Font(selectedState, trackBar1.Value, style);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FontStyle style = GetFontStyle();

            int size = MinMaxValue();

            trackBar1.Value = size;
            label1.Text = String.Format("Текущий размер шрифта: {0}", textBox1.Text);

            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, size, style);
        }

        private int textBox1_Validating()
        {
            int size = 1 ;
            if (1== 1)
            {
                size = trackBar1.Value; 
            }
            
            if (textBox1.Text.Length < 42)
            {
                return 42;
            }
            else if(size < 1)
            {
                return 1;
            }
            return size;
        }

        private int MinMaxValue()
        {
            int size;
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                return trackBar1.Value;
            } else {
                size = Convert.ToInt32(textBox1.Text );
                if (size > 42)
                {
                    return 42;
                }
                else if (size < 1)
                {
                    return 1;
                }
            }

            return size;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "D:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            FontStyle style = GetFontStyle();
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, trackBar1.Value, style);

            richTextBox1.Text = richTextBox1.Text + fileContent;        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                { 
                    myStream.Close();
                    File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
                }
            }
        }
    }
}

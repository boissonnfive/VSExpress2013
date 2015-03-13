using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinNameChanger
{
    public partial class FormOptions : Form
    {
        private FormNameChanger form;
        public FormOptions(FormNameChanger frm)
        {
            InitializeComponent();
            form = frm;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            form.selectAll();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            form.deselectAll();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            form.reverseSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form.checkAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form.uncheckAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            form.reverseCheck();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            form.hideExtension(checkBox1.Checked);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            form.ignoreCase(checkBox2.Checked);
        }

        public void setIgnoreCase(Boolean ignore)
        {
            // We need to unsuscribe to checked event to not go in listView1_ItemChecked
            checkBox2.CheckedChanged -= checkBox2_CheckedChanged;
            checkBox2.Checked = ignore;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            
        }

        public void setHideExtension(Boolean hide)
        {
            // We need to unsuscribe to checked event to not go in listView1_ItemChecked
            checkBox1.CheckedChanged -= checkBox1_CheckedChanged;
            checkBox1.Checked = hide;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
        }
    }
}

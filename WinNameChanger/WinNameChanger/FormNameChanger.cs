using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinNameChanger
{
    public partial class FormNameChanger : Form
    {
        private Controller ctrl;
        public FormNameChanger()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ctrl = new Controller(this);

            // Set the view to show details.
            listView1.View = View.Details;
            // Allow the user to edit item text.
            listView1.LabelEdit = true;
            // Select the item and subitems when selection is made.
            listView1.FullRowSelect = true;
            // Display grid lines.
            listView1.GridLines = true;
            // By default, the first element of the combo box is selected
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            comboBox1.SelectedItem = comboBox1.Items[0];
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        /**
         * openToolStripMenuItem_Click  (called by menu "Ouvrir...")
         * Click on the menu item "Ouvrir".
         * @param object sender: not used
         * @param EventArgs e: not used
         */
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addFile();
        }

        /**
         * addFileFromButton  (called by button "Ajouter")
         * Click on the button "Ajouter".
         * @param object sender: not used
         * @param EventArgs e: not used
         */
        private void addFileFromButton(object sender, EventArgs e)
        {
            addFile();
        }

        /**
         * addFile
         * Open multiple files and add their name to the list view.
         */
        private void addFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = false;
            ofd.CheckPathExists = false;
            ofd.DefaultExt = "*.*";
            ofd.Filter = "Tous les fichiers(*.*)|*.*";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (String completeFileName in ofd.FileNames)
                {
                    ctrl.addFile(completeFileName);
                }
            }
            // Now that we have files, we can rename. So we enable all relative components
            EnableComponents(true);
        }

        /**
         * addName (called by Controller)
         * Add a name in the list view.
         * @param String originalName: the name in column "original name"
         * @param String modifiedName: the name in column "modified name"
         */
        public void addName(String originalName, String modifiedName)
        {
            // The original name is new line in the first column
            ListViewItem item1 = new ListViewItem(originalName);
            
            // Each line must have a check box
            // But we need to unsuscribe to checked event to not go in listView1_ItemChecked
            listView1.ItemChecked -= listView1_ItemChecked;
            item1.Checked = true;
            listView1.ItemChecked += listView1_ItemChecked;
            // To create columns, you need to add a SubItems collection
            // where index 0 is the first column, index 1 the second column, etc ...
            item1.SubItems.Add(modifiedName);

            // We need to add the line to the list view
            listView1.Items.Add(item1);
            //listView1.Items.AddRange(new ListViewItem[] { item1, item2, item3 });           
        }

        /**
        * quitToolStripMenuItem_Click (called by menu "Quitter")
        * Terminate the application.
        * @param object sender: not used
        * @param EventArgs e: not used
        */
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /**
        * removeFile (called by button "Enlever")
        * Remove item from list view.
        * @param object sender: not used
        * @param EventArgs e: not used
        */
        private void removeFile(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Selected)
                {
                    ctrl.removeFile(item.Index);
                }
            }
        }

        /**
        * removeName (called by Controller)
        * Remove item from list view.
        * @param object sender: not used
        * @param EventArgs e: not used
        */
        public void removeName(int index)
        {
            listView1.Items.RemoveAt(index);
        }

        /**
        * clear (called by button "Vider")
        * Remove all files in the program
        * @param object sender: not used
        * @param EventArgs e: not used
        */
        private void clear(object sender, EventArgs e)
        {
            ctrl.removeAllFiles();
        }

        /**
        * clear (called by Controller)
        * Remove all items from list view.
        */
        public void clear()
        {
            listView1.Items.Clear();
            EnableComponents(false);
        }

        /**
        * rename (called by button "Renomer")
        * Rename all checked items.
        * @param object sender: not used
        * @param EventArgs e: not used
        */
        private void rename(object sender, EventArgs e)
        {
            ctrl.renameFiles();
        }

        /**
        * textBox1_TextChanged (called by textBox1)
        * Listen to change in textBox1 to update modified file names column.
        * @param object sender: not used
        * @param EventArgs e: not used
        */
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;
            Object selectedItem = comboBox1.SelectedItem;

            if (selectedIndex == -1 || selectedItem == null)
            {
                return;
            }

            //MessageBox.Show("Selected Item Text: " + selectedItem.ToString() + "\n" + "Index: " + selectedIndex.ToString());

            updateModifiedName();
        }

        /**
        * comboBox1_SelectedIndexChanged (called by comboBox1)
        * Listen to change in comboBox1 to update modified file names column.
        * @param object sender: not used
        * @param EventArgs e: not used
        */
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Ajoute au début" || comboBox1.SelectedItem.ToString() == "Ajoute à la fin")
            {
                textBox1.Visible = false;
                textBox2.Location = new System.Drawing.Point(300, 88);
                //comboBox1.Location = new System.Drawing.Point(0,0) // Top left of Form
                comboBox1.Location = new System.Drawing.Point(13, 88);
            }
            else
            {
                textBox1.Visible = true;
                textBox2.Location = new System.Drawing.Point(454, 88);
                //comboBox1.Location = new System.Drawing.Point(0,0) // Top left of Form
                comboBox1.Location = new System.Drawing.Point(196, 88);
                if (textBox1.Text.Length > 0)
                {
                    updateModifiedName();
                }
            }
        }

        /**
        * textBox2_TextChanged (called by textBox2)
        * Listen to change in textBox2 to update modified file names column.
        * @param object sender: not used
        * @param EventArgs e: not used
        */
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;
            Object selectedItem = comboBox1.SelectedItem;

            if (selectedIndex == -1 || selectedItem == null)
            {
                return;
            }

            //MessageBox.Show("Selected Item Text: " + selectedItem.ToString() + "\n" + "Index: " + selectedIndex.ToString());

            updateModifiedName();
        }

        /**
        * listView1_ItemChecked (called by listView1)
        * Listen to checked/unchecked items to update modified file names column.
        * (unchecked item must not be renamed!)
        * @param object sender: not used
        * @param EventArgs e: not used
        */
        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!e.Item.Checked)
            {
                e.Item.SubItems[1].Text = e.Item.Text; // Reset newName to oldName
            }
            else
            {
                updateModifiedName(); // apply renaming
            }
        }

        /**
        * EnableComponents (called by listView1)
        * Enable or disable components relative to renaming
        * (These components are useless when list view is empty)
        * @param Boolean enable: true if we want to enable them, else false
        */
        private void EnableComponents(Boolean enable)
        {
            renameToolStripMenuItem.Enabled = enable;
            button2.Enabled = enable;
            button3.Enabled = enable;
            button4.Enabled = enable;
            button5.Enabled = enable;
            textBox1.Enabled = enable;
            textBox2.Enabled = enable;
            comboBox1.Enabled = enable;

        }

        private void updateModifiedName()
        {
            foreach (ListViewItem oldNameItem in listView1.Items)
            {
                ctrl.rename(comboBox1.SelectedIndex, oldNameItem.Index);
            }            
        }

        public int isChecked(int index)
        {
            int retour = -1;
            if (index >= 0 && index < listView1.Items.Count)
            {
                if (listView1.Items[index].Checked)
                {
                    retour = 1;
                }
                else
                {
                    retour = 0;
                }
            }
            return retour;
        }

        public int setModifiedName(int index, String modifiedName)
        {
            int retour = -1;
            if (index >= 0 && index < listView1.Items.Count)
            {
                listView1.Items[index].SubItems[1].Text = modifiedName;
                retour = 1;
            }
            return retour;
        }

        public String getModifiedName(int index)
        {
            if (index >= 0 && index < listView1.Items.Count)
            {
                return listView1.Items[index].SubItems[1].Text;
            }
            else
                return "";
        }

        public int setOldName(int index, String oldName)
        {
            int retour = -1;
            if (index >= 0 && index < listView1.Items.Count)
            {
                listView1.Items[index].Text = oldName;
                retour = 1;
            }
            return retour;
        }

        public String getOldName(int index)
        {
            if (index >= 0 && index < listView1.Items.Count)
            {
                return listView1.Items[index].Text;
            }
            else
                return "";
        }

        public String getSearchText()
        {
            return textBox1.Text;
        }

        public String getReplaceText()
        {
            return textBox2.Text;
        }

        public void checkAll()
        {
            // We need to unsuscribe to checked event to not go in listView1_ItemChecked
            listView1.ItemChecked -= listView1_ItemChecked;
            foreach (ListViewItem item in listView1.Items)
            {                
                item.Checked = true;
            }
            // Don't forget to suscribe again !
            listView1.ItemChecked += listView1_ItemChecked;
        }

        public void uncheckAll()
        {
            // We need to unsuscribe to checked event to not go in listView1_ItemChecked
            listView1.ItemChecked -= listView1_ItemChecked;
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = false;
            }
            // Don't forget to suscribe again !
            listView1.ItemChecked += listView1_ItemChecked;
        }

        public void reverseCheck()
        {
            // We need to unsuscribe to checked event to not go in listView1_ItemChecked
            listView1.ItemChecked -= listView1_ItemChecked;
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = !item.Checked;
            }
            // Don't forget to suscribe again !
            listView1.ItemChecked += listView1_ItemChecked;
        }

        public void selectAll()
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Selected = true;
            }
            this.Activate();
            this.listView1.Focus();
        }

        public void deselectAll()
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Selected = false;
            }
            this.Activate();
            this.listView1.Focus();
        }

        public void reverseSelection()
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Selected = !item.Selected;
            }
            this.Activate();
            this.listView1.Focus();
        }

        public void hideExtension(Boolean hide)
        {
            ctrl.setHideExtension(hide);
        }

        public void ignoreCase(Boolean ignore)
        {
            ctrl.setIgnoreCase(ignore);
        }

        private void Options(object sender, EventArgs e)
        {
            FormOptions form2 = new FormOptions(this);
            form2.setIgnoreCase(ctrl.getIgnoreCase());
            form2.setHideExtension(ctrl.getHideExtension());
            form2.Show();
        }
    }
}
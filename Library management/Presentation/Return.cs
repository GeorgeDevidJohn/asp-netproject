using Library_management.Middleware;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Library_management
{
    public partial class Return : Form
    {
        LibraryManagementDB libraryManagementDB = new LibraryManagementDB();
        public int UserId;
        public Return()
        {
            InitializeComponent();
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void setUserId(int Id)
        {
            UserId = Id;
            GetLendedBooks(UserId);
        }


        private void login_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lendedBooks.SelectedItems.Any())
            {

                var moveBook = lendedBooks.SelectedItem;
                returnBooks.Items.Add(moveBook);
                lendedBooks.Items.RemoveAt(lendedBooks.SelectedIndex);

            }
            else
            {
                MessageBox.Show("No book selected", "Alert");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (returnBooks.SelectedItems.Any())
            {

                var moveBook = returnBooks.SelectedItem;
                lendedBooks.Items.Add(moveBook);
                returnBooks.Items.RemoveAt(returnBooks.SelectedIndex);

            }
            else
            {
                MessageBox.Show("No book selected", "Alert");
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (returnBooks.Items.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to return these books ?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    List<int> itemIds = new List<int>();
                    foreach (var item in returnBooks.Items)
                    {
                        string itemName = item.ToString();
                        string[] words = itemName.Split(',');
                        var id = int.Parse(words[0].Trim());
                        itemIds.Add(id);
                    }

                    libraryManagementDB.ReturnBook(UserId, itemIds.ToArray());
                    returnBooks.Items.Clear();

                }
               

            }
            else
            {
                MessageBox.Show("No book borrowed", "Alert");
            }
        }
        public void GetLendedBooks(int id)
        {
            lendedBooks.Items.Clear();
            var data = libraryManagementDB.GetAllLendedBooks(id);
            lendedBooks.Items.AddRange(data.ToArray());
        }
    }
}

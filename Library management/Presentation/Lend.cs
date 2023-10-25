using Castle.Core.Resource;
using LibGit2Sharp;
using Library_management.Middleware;
using Library_management.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace Library_management
{
    public partial class Lend : Form
    {
        LibraryManagementDB libraryManagementDB = new LibraryManagementDB();
        public int UserId;
        public int count;



        public Lend()
        {
            InitializeComponent();
            GetAllAvilableBooks();
            getCount();
             
        }

        private void login_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void Lend_Load(object sender, EventArgs e)
        {

        }

        public void setUserId(int Id)
        {
            UserId = Id;
        }

        public void GetAllAvilableBooks()
        {
            allbooks.Items.Clear();
            var data = libraryManagementDB.GetAllBooksAvailable();
            allbooks.Items.AddRange(data.ToArray());
        }

        private void availableBooks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reservedBooks.Focused)
            {
                var selectedItems = reservedBooks.SelectedItems[0].ToString();
                string[] words = selectedItems.Split(',');
                var bookId = int.Parse(words[0].Trim());
                getDetails(bookId);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (reservedBooks.Items.Count < 5 && count <5)
            {
                if (allbooks.SelectedItems.Any())
                {
                    
                    var moveBook = allbooks.SelectedItem;
                    reservedBooks.Items.Add(moveBook);
                    allbooks.Items.RemoveAt(allbooks.SelectedIndex);

                }
                else
                {
                    MessageBox.Show("No book selected", "Alert");
                }
            }
            else
            {
                MessageBox.Show("Reched maximum number of books", "Alert");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (reservedBooks.SelectedItems.Any()) { 
                var moveBook = reservedBooks.SelectedItem;
                allbooks.Items.Add(moveBook);
                reservedBooks.Items.RemoveAt(reservedBooks.SelectedIndex);
                
            }
            else
            {
                MessageBox.Show("No book selected", "Alert");
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void allbooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (allbooks.Focused) { 
                var selectedItems = allbooks.SelectedItems[0].ToString();
                string[] words = selectedItems.Split(',');
                var bookId = int.Parse(words[0].Trim());
                getDetails(bookId);
            }
            


        }

        public void getDetails(int bookId)
        {
            var bookDetail = libraryManagementDB.GetBookDetails(bookId);
            string data = "Title: " + bookDetail.BookName + "\nAuthor: " + bookDetail.AuthorName +
                 "\nCategory: " + bookDetail.Category.CategoryName +
                 "\nSummary: " + bookDetail.Description;              
            bookDetails.Text = data;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(reservedBooks.Items.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to borrow these books ?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    List<int> itemIds = new List<int>();
                    foreach (var item in reservedBooks.Items)
                    {
                        string itemName = item.ToString();
                        string[] words = itemName.Split(',');
                        var id = int.Parse(words[0].Trim());
                        itemIds.Add(id);
                    }

                    libraryManagementDB.LendBook(UserId, itemIds.ToArray());
                    reservedBooks.Items.Clear();
                    bookDetails.Text = "";

                }
               
            }
            else
            {
                MessageBox.Show("No book borrowed", "Alert");
            }
            
        }
        public void getCount()
        {
             count = libraryManagementDB.getLendCount(UserId);          
        }
    }
}

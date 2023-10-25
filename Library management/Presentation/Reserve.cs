using Library_management.Middleware;
using Library_management.Models;
using Library_management.ViewModel;
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
    public partial class Reserve : Form
    {
        public int UserId;
        LibraryManagementDB libraryManagementDB = new LibraryManagementDB();
        public Reserve()
        {
            InitializeComponent();
            
        }

        private void login_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reservedbooks.Focused)
            {
                var selectedItems = reservedbooks.SelectedItems[0].ToString();
                string[] words = selectedItems.Split(',');
                var bookId = int.Parse(words[0].Trim());
                if (getReserved() == null)
                {
                    getDetails(bookId);
                }
                
            }
        }
        public void setUserId(int Id)
        {
            UserId = Id;
            GetLendedBooks();
            displayReserved();

        }

        public void GetLendedBooks()
        {

            reservedbooks.Items.Clear();
            var data = libraryManagementDB.GetAllBookstoReserve(UserId);
            reservedbooks.Items.AddRange(data.ToArray());
        }

        public void getDetails(int id)
        {
            var bookDetail = libraryManagementDB.GetBookDetails(id);
            string data = "Title: " + bookDetail.BookName + "\nAuthor: " + bookDetail.AuthorName +
                 "\nCategory: " + bookDetail.Category.CategoryName +
                 "\nSummary: " + bookDetail.Description;
            bookdetails.Text = data;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (getReserved() == null)
            {
                if (reservedbooks.SelectedItems.Any())
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to reserve " + reservedbooks.SelectedItems[0].ToString() + " ?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {


                        string itemName = reservedbooks.SelectedItems[0].ToString();             
                        string[] words = itemName.Split(',');
                        var id = int.Parse(words[0].Trim());
           
                        libraryManagementDB.ReserveBook(UserId, id);
                        GetLendedBooks();
                        displayReserved();
                        /*bookdetails.Text = "";*/

                    }
                   

                }
                else
                {
                    MessageBox.Show("No book is selected", "Alert");
                }
            }
            else
            {
                MessageBox.Show("You have already reserved a book", "Alert");
            }
        }

        public BooksDTO getReserved()
        {
            return libraryManagementDB.ReservedBook(UserId);
        }

        public void displayReserved()
        {
            BooksDTO reservedBook = getReserved();
            if (reservedBook != null)
            {
                string data = "Reserved Book:\nTitle: " + reservedBook.BookName + "\nAuthor: " + reservedBook.AuthorName +
                "\nCategory: " + reservedBook.Category.CategoryName +
                "\nSummary: " + reservedBook.Description;
                bookdetails.Text = data;


            }
        }
    }
}

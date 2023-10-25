using Library_management.Middleware;
using Library_management.Models;
using Library_management.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Library_management
{
    public partial class Edit : Form
    {
        public int UserId;
        public int bookId;
        LibraryManagementDB libraryManagement = new LibraryManagementDB();  
        public Edit()
        {
            InitializeComponent();
            getALLCategory();
            category.SelectedIndex = -1;

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Validator.IsPresent(bookName))
            {
                allbooks.Items.Clear();
                description.Text = "";
                var book = bookName.Text;
                var allBooks = libraryManagement.GetAllBooks(book);
                if (allBooks.Count == 0) {
                    allbooks.Items.Add("No Books Found");
                    description.Text = "";
                }
                else
                {
                    allbooks.Items.AddRange(allBooks.ToArray());
                }
               
            }
        }

        private void allbooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (allbooks.Focused)
            {
                var selectedItems = allbooks.SelectedItems[0].ToString();
                string[] words = selectedItems.Split(',');
                var bookId = int.Parse(words[0].Trim());
                getDetails(bookId);
            }
        }

        public void getDetails(int id)
        {
            var bookDetail = libraryManagement.GetBookDetails(id);
            string data = "Title: " + bookDetail.BookName + "\nAuthor: " + bookDetail.AuthorName +
                 "\nCategory: " + bookDetail.Category.CategoryName +
                 "\nSummary: " + bookDetail.Description;
            description.Text = data;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Validator.IsPresent(book))
            {
                editbooklist.Items.Clear();
                bookDetails.Text = "";
                var bookDe = book.Text;
                var allBooks = libraryManagement.GetAllBooks(bookDe);
                if (allBooks.Count == 0)
                {
                    editbooklist.Items.Add("No Books Found");
                }
                else
                {
                    editbooklist.Items.AddRange(allBooks.ToArray());
                }

            }
        }

        private void editbooklist_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedBook = editbooklist.SelectedItems[0];
            if (selectedBook != null)
            {
                string[] words = selectedBook.ToString().Split(',');
                var bookId = int.Parse(words[0].Trim());
                var bookDetail = libraryManagement.GetBookDetails(bookId);
                if (bookDetail.Owner != UserId)
                {
                    MessageBox.Show("You are not the owner of this book. So you cannot edit the book", "Not the owner");
                    author.Text = "";
                    category.SelectedIndex = -1;
                    bookDetails.Text = "";
                    bookId = 0;
                }
                else
                {
                    getDetails(bookDetail);
                }
            }
        }

        public void getDetails(BooksDTO bookDetail)
        {
            author.Text = bookDetail.AuthorName;
            category.SelectedValue = bookDetail.CategoryId;
            bookDetails.Text = bookDetail.Description;
            bookId = bookDetail.BookId;
        }

        public void setUserId(int Id)
        {
            UserId = Id;
        }
        public void getALLCategory()
        {
            var allCategory = libraryManagement.GetCategory();
            category.DataSource = allCategory;
            category.DisplayMember = "CategoryName";
            category.ValueMember = "CategoryID";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (bookId > 0)
                {
                    if (Validator.IsPresent(author) && Validator.IsPresentRichText(bookDetails) && Validator.IsPresentComboBox(category))
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to proceed  ?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            libraryManagement.UpdateBook(bookId, author.Text, bookDetails.Text, (int)category.SelectedValue);
                            MessageBox.Show("The book has succesfully been updated in the library");
                            ClearAll();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Choose a book to proceed");
                }
              
            }
            catch (OverflowException ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.GetType().ToString() + "\n" + ex.StackTrace, "Exception");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.GetType().ToString() + "\n" + ex.StackTrace, "Exception");
            }

            
        }
        public void ClearAll()
        {
            author.Text = "";
            book.Text = "";
            category.SelectedIndex = -1;
            bookDetails.Text = "";
            bookId = 0;
            editbooklist.Items.Clear();
        }
    }
}

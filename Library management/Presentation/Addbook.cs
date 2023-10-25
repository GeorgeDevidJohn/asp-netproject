
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Library_management
{
    public partial class Addbook : Form
    {
        LibraryManagementDB libraryManagementDB = new LibraryManagementDB();
        public int UserId;
        public Addbook()
        {
            InitializeComponent();
            getALLCategory();
            category.SelectedIndex = -1;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public void getALLCategory()
        {
            var allCategory = libraryManagementDB.GetCategory();
            category.DataSource = allCategory;
            category.DisplayMember = "CategoryName";
            category.ValueMember = "CategoryID";
        }

        private void login_Click(object sender, EventArgs e)
        {
            homePage newHome = new homePage();

            try
            {
                if (Validator.IsPresent(bookName) && Validator.IsPresent(author) && Validator.IsPresentRichText(description) && Validator.IsPresentComboBox(category))
                {
                    BooksDTO book = new BooksDTO()
                    {
                        BookName = bookName.Text,
                        AuthorName = author.Text,
                        Description = description.Text,
                        CategoryId = (int)category.SelectedValue,
                        Owner = UserId
                       
                    };
                    DialogResult result = MessageBox.Show("Are you sure you want to proceed  ?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        AddBook(book);
                    }
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

        public void AddBook(BooksDTO book)
        {

            DialogResult result = MessageBox.Show(book.BookName + " has succesfully been added to the library");

            string errorMessage = "";
            var response = libraryManagementDB.AddBook(book, out errorMessage);

            if (response == 1)
            {
                this.ClearRegister();

            }
            else
                MessageBox.Show(errorMessage);

        }

        public void ClearRegister()
        {
            bookName.Clear();
            author.Clear();
            description.Clear();
            Owner = null;
            category.SelectedIndex = -1;

        }

        public void setUserId(int Id)
        {
            UserId = Id;
        }


    }
}

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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Library_management
{
    public partial class Signup : Form
    {
        LibraryManagementDB libraryManagementDB = new LibraryManagementDB();
        public UsersDTO usersDetail;
        public Signup()
        {
            InitializeComponent();
           
        }

        private void login_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validator.IsPresent(userName) && Validator.IsPresent(password) && Validator.IsPresent(email) && Validator.IsEmail(email))
            {
                UsersDTO users = new UsersDTO()
                {
                    Name = userName.Text,
                    Email = email.Text.ToLower(),
                    Password = password.Text

                };
                AddUser(users);            
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            userName.Clear();
            email.Clear();
            password.Clear();
            Form1 login = new Form1();
            this.Visible = false;
            login.ShowDialog();
            
        }
        public void enterHome()
        {
            homePage home = new homePage();
            this.Visible = false;
            home.ShowDialog();
        }

        public void AddUser(UsersDTO users)
        {
            DialogResult result = MessageBox.Show(users.Name + " is succesfully registered");

            string errorMessage = "";
            var response = libraryManagementDB.RegisterUser(users, out errorMessage);

            if (response == 1) 
            { 
                this.ClearRegister();
                this.Visible = false;
                homePage home = new homePage();
                //GET CURRENT USER
                var currentUser = libraryManagementDB.ChekingUser(users);
                home.setTitle(currentUser);
                home.ShowDialog();
                
            }
                else
                    MessageBox.Show(errorMessage);
            
        }

        private void Signup_Load(object sender, EventArgs e)
        {

        }

        public void ClearRegister()
        {
            userName.Clear();
            email.Clear();
            password.Clear();
        }
    }
}

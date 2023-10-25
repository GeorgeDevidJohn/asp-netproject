using Library_management.Middleware;
using Library_management.Models;
using Library_management.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Library_management
{
    public partial class Form1 : Form
    {
       
        LibraryManagementDB libraryManagementDB = new LibraryManagementDB();
        public UsersDTO usersDetail;
        public Form1()
        {
            InitializeComponent();
             


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void login_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validator.IsPresent(email) && Validator.IsPresent(password) && Validator.IsPresent(email) && Validator.IsEmail(email))
                {
                    UsersDTO users = new UsersDTO()
                    {
                        Email = email.Text.ToLower(),
                        Password = password.Text
                    };
                    CheckUp(users);
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
            Signup sig = new Signup();
            this.Visible = false;
            DialogResult result = sig.ShowDialog();
            this.Close();
        }

        public void enterHome()
        {
            homePage home = new homePage();
            this.Visible = false;
            DialogResult result = home.ShowDialog(email);
        }

        public Boolean isValidData()
        {
             if (password.Text == "")
            {
                MessageBox.Show("Please enter Last Name", "Entry Error");
                password.Focus();
                return false;
            }
            else if ((email.Text == "") || !Regex.IsMatch(email.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Please enter a valid Email", "Entry Error");
                email.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        public void CheckUp(UsersDTO users)
        {
            usersDetail = libraryManagementDB.ChekingUser(users);
            if(usersDetail == null)
            {
                MessageBox.Show("Incorrect Username/Password", "User not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                homePage home = new homePage();
                this.Visible = false;
                home.setTitle(usersDetail);
                home.ShowDialog();
            }
        }
        public void checkValidUser()
        {
            
          //  var userprent = Signup.userlist.Where(x => x.Email == email.Text && x.Password == password.Text).Any();
         /*   if(userprent)
            {
                isValid = true; ;
            }
            else
            {
                isValid = false;
            }
*/

        }
    }
}

using Library_management.Middleware;
using Library_management.Models;
using Library_management.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Library_management
{
    public partial class homePage : Form
    {
        LibraryManagementDB libraryManagement = new LibraryManagementDB();
        public int UserId = 0;
        public homePage()
        {
            InitializeComponent();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void homePage_Load(object sender, EventArgs e)
        {

        }

        private void login_Click(object sender, EventArgs e)
        {
            Lend lend = new Lend();
            lend.setUserId(UserId);
            var result = lend.ShowDialog();        
            if (result == DialogResult.OK)
            {
                getLendedCount(UserId);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Return retur = new Return();
            retur.setUserId(UserId);
            var result = retur.ShowDialog();
            if (result == DialogResult.OK)
            {
                getLendedCount(UserId);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Addbook addbook1 = new Addbook();
            addbook1.setUserId(UserId);
            addbook1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Edit edit = new Edit();
            edit.setUserId(UserId);
            edit.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Reserve reserve = new Reserve();
            reserve.setUserId(UserId);
            reserve.ShowDialog();            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Visible = false;
            form1.ShowDialog();
        }
        public void setTitle( UsersDTO userDetails)
        {
           
                UserName.Text = userDetails.Name;
                UserId = userDetails.UserId;
                getLendedCount(UserId);


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public void getLendedCount(int userId)
        {
            count.Text =  libraryManagement.getLendCount(userId).ToString();
        }

        private void label4_Click(object sender, EventArgs e)
        {
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var fees = libraryManagement.CalcFees(UserId);
            var text = "";
            if (fees.Count > 0)
            {
                text += "$5 is charged per extra day for each book borrowed for more than 7 days.\n";
                fees.ForEach(fe =>
                {
                    TimeSpan datediff = (TimeSpan)(DateTime.Now - fe.ReturnDate);
                    text += "Name: " + fe.BookName + " Days: " + datediff.Days + " Fees: $" + fe.fees + "\n";
                });
            }
            else
            {
                text += "No dues";
            }
            MessageBox.Show(text, "Dues");
        }
    }
}

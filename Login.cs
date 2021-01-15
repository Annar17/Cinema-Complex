using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ergasia_2020
{
    public partial class Login : Form
    {
        public string side;
        public Login()
        {
            InitializeComponent();
       
        }

    
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are You Sure To Exit Cinema-Complex ?\n Unsaved work will be deleted.", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Environment.Exit(0);
            }

        }

        private void login_bt_Click(object sender, EventArgs e)
        {
            if(richTextBox1.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("Please fill in both Username and Password to continue.");
            }
            else
            {
                if(side == "employee")
                {
                    Cinema_Complex_Employee temp = new Cinema_Complex_Employee();
                    temp.set_employee_name(richTextBox1.Text);
                    temp.Show();
                }
                else
                {
                    Cinema_Complex_Customer temp = new Cinema_Complex_Customer();
                    temp.set_customer_name(richTextBox1.Text);
                    temp.Show();
                }
                this.Hide();
            }
        }

        private void cancel_bt1_Click(object sender, EventArgs e)
        {
            Employee_Main main_obj = new Employee_Main();
            main_obj.Show();
            this.Hide();
        }
    }
}

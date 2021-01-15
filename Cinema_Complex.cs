using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ergasia_2020
{
    public partial class Employee_Main : Form
    {
        
        public Employee_Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login_obj = new Login();
            login_obj.Show();
            login_obj.side = "employee";
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login login_obj = new Login();
            login_obj.Show();
            login_obj.side = "customer";
            this.Hide();
        }

        private void Employee_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

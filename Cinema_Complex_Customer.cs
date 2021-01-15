using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Ergasia_2020
{
    
    public partial class Cinema_Complex_Customer : Form
    {
        List<Movie> Movies = new List<Movie>();
        List<Button> checked_buttons = new List<Button>();
        private string customer_name;


        private void TextBox_Binding()
        {
            BindingSource bsource = new BindingSource();
            bsource.DataSource = Movies;
            comboBox_Search.DataSource = bsource.DataSource;

            comboBox_Search.DisplayMember = "Title";
            comboBox_Search.Text = null;
        }
        private void search_movie()
        {
            string search_title = comboBox_Search.DisplayMember;
            Movie movie = new Movie();

            movie = (Movie)comboBox_Search.SelectedItem;

            if (movie != null)
            {
                //Movie Panel
                label17.Text = movie.Title;
                label18.Text = movie.Genre;
                label19.Text = movie.Starting;
                label21.Text = movie.Ending;
                label22.Text = movie.Room;
                //Seats Panel
                seats_title.Text = movie.Title;
                seats_starting.Text = movie.Starting;
                seats_room.Text = movie.Room;
            }
            else
            {
                comboBox_Search.Text = null;
                MessageBox.Show("This movie doesn't exists.\nPlease select one from our list.\n", "Atteintion");
            }

        }

        public void set_customer_name(string name)
        {
            this.customer_name = name;
        }

        public string get_customer_name()
        {
            return this.customer_name;
        }

        private bool checkProducts()
        {
            string message = "Please choose both item and quantity for:\n"; 
            bool food = false, foodq = false, sodas = false, sodasq = false, drinks = false, drinksq = false;
            if(comboBox_Food.Text != "") { food = true; }
            if(comboBox_QuantityF.Text != "") { foodq = true; }
            if(comboBox_Sodas.Text != "") { sodas = true; }
            if(comboBox_QuantityS.Text != "") { sodasq = true; }
            if(comboBox_Drinks.Text != "") { drinks = true; }
            if (comboBox_QuantityD.Text != "") { drinksq = true; }

            if(food != foodq) { message += "Food Selection.\n"; }
            if(sodas != sodasq) { message += "Sodas.\n"; }
            if(drinks != drinksq) { message += "Drinks.\n"; }

            if(message == "Please choose both item and quantity for:\n")
            {
                return true;
            }
            else
            {
                MessageBox.Show(message);
                return false;
            }
        }

        void combo_box_clear()
        {
            comboBox_Food.Items.Clear();
            comboBox_Drinks.Items.Clear();
            comboBox_Sodas.Items.Clear();
        }

        void credit_clear()
        {
            name_card_tb.Text = "";
            number_card_tb.Text = "";
            cvv_tb.Text = "";
        }

        void movies_clear()
        {
            
            label17.Text = "";
            label18.Text = "";
            label19.Text = "";
            label21.Text = "";
            label22.Text = "";
        }
        void seats_clear()
        {
            seats_title.Text = "";
            seats_starting.Text = "";
            seats_room.Text = "";
        }
        void clear_products()
        {
            comboBox_Food.Text = null;
            comboBox_QuantityD.Text = null;
            comboBox_Sodas.Text = null;
            comboBox_QuantityS.Text = null;
            comboBox_Drinks.Text = null;
            comboBox_QuantityD.Text = null;
        }
        void clear_buttons()
        {
            foreach (Button bt in checked_buttons)
            {
                bt.BackColor = Color.Yellow;
            }
            checked_buttons.Clear();
        }
        private bool check_credit()
        {
            bool alldone = true;
            if(number_card_tb.Text.Trim().Length != 16) { alldone = false;toolTip1.Show("Card number must be 16digits long.",number_card_tb,3000); }
            if(cvv_tb.Text.Trim().Length != 3) { alldone = false; toolTip2.Show("Cvv number must be 3digits long.", cvv_tb,3000); }
            if (name_card_tb.Text == "") { alldone = false; toolTip3.Show("Card name can't be empty.", name_card_tb, 3000); }

            return alldone;
        }
        void clean_everything()
        {
            combo_box_clear();
            credit_clear();
            movies_clear();
            seats_clear();
            clear_buttons();
        }
        
        public Cinema_Complex_Customer()
        {
            InitializeComponent();
            string[] lines = File.ReadAllLines(@".\Mock_Data.csv", Encoding.UTF8);
            foreach (string line in lines)
            {

                string Title, Genre, Starting, Ending, Room;
                string[] movie_data = line.Split(',');
                Title = movie_data[0]; Genre = movie_data[1]; Starting = movie_data[2]; Ending = movie_data[3]; Room = movie_data[4];
                Movies.Add(new Movie(Title, Genre, Starting, Ending, Room));

            }
            TextBox_Binding();
            //Panels
            seats_panel.Hide();
            drink_panel.Hide();
            credit_panel.Hide();

            //Set-Up Combo Boxes
            comboBox_Food.Items.Add("Pizza"); comboBox_Food.Items.Add("Pop Corn"); comboBox_Food.Items.Add("Nachos");
            comboBox_Sodas.Items.Add("Coca-Cola"); comboBox_Sodas.Items.Add("Sprite"); comboBox_Sodas.Items.Add("Water");
            comboBox_Drinks.Items.Add("Whsikey"); comboBox_Drinks.Items.Add("Vodka"); comboBox_Drinks.Items.Add("Rum");

        }

        private void Cinema_Complex_Customer_Load(object sender, EventArgs e)
        {
            lbl_customer.Text = get_customer_name();
        }

       private void seat_availability(Button bt)
        {
            if(bt.BackColor == Color.Yellow)
            {
                bt.BackColor = Color.Green;
                checked_buttons.Add(bt);
            }
            else
            {
                bt.BackColor = Color.Yellow;
                checked_buttons.Remove(bt);
            }
            label_seats_sl.Text = checked_buttons.Count.ToString();
            
        }

        private void search_bt_Click(object sender, EventArgs e)
        {

            movies_clear();
            search_movie();
            
        }

        private void Cinema_Complex_Customer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are You Sure To Exit Cinema-Complex ?\n Unsaved work will be deleted.", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Environment.Exit(0);
            }
        }

       

        private void movie_panel_next_Click(object sender, EventArgs e)
        {
            if(label17.Text == "")
            {
                MessageBox.Show("Please select a movie.\n", "Information Missing");
            }
            else
            {
                movie_panel.Hide();
                seats_panel.Show();
                search_bt.Enabled = false;
                comboBox_Search.Text = null;
            }
            
        }

        private void seats_panel_back_Click(object sender, EventArgs e)
        {
            seats_panel.Hide();
            movie_panel.Show();
            search_bt.Enabled = true;
        }

        private void seats_panel_next_Click(object sender, EventArgs e)
        {
            if(checked_buttons.Count == 0)
            {
                MessageBox.Show("Please select at least one seat to continue.\n", "No seat selected.");
            }
            else
            {
                seats_panel.Hide();
                clear_products();
                drink_panel.Show();
            }
           
        }

        private void drink_panel_back_Click(object sender, EventArgs e)
        {
            drink_panel.Hide();
            seats_panel.Show();
          
        }

        private void drink_panel_next_Click(object sender, EventArgs e)
        {
            if (checkProducts())
            {
                drink_panel.Hide();
                credit_panel.Show();
            }
        }

        private void credit_panel_back_Click(object sender, EventArgs e)
        {
            credit_panel.Hide();
            drink_panel.Show();
            
        }

        private void button51_Click(object sender, EventArgs e)
        {
            if (check_credit())
            {
                MessageBox.Show("Congratulations!\nYour payment was processed succesfully.\nThank you for choosing our Cinema!", "Succes");
                clean_everything();
                credit_panel.Hide();
                movie_panel.Show();
                search_bt.Enabled = true;
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button40_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            seat_availability(sender as Button);
        }

        private void logout_bt_Click(object sender, EventArgs e)
        {
            Employee_Main main_obj = new Employee_Main();
            main_obj.Show();
            this.Hide();
        }

        private void seats_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void number_card_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cvv_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void name_card_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void comboBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void comboBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void logout_bt_Click_1(object sender, EventArgs e)
        {
            Employee_Main main_obj = new Employee_Main();
            main_obj.Show();
            this.Hide();
        }

        private void comboBox_Search_SelectionChangeCommitted(object sender, EventArgs e)
        {
            search_movie();
        }
    }
}

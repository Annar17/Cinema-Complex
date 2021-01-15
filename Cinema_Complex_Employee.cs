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
    public partial class Cinema_Complex_Employee : Form
    {
        List<Cleaning> cleaning_data = new List<Cleaning>();
        Cleaning selected_to_clean = new Cleaning();
        private string employee_name;

        private void fill_combo()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Room 1");
            comboBox1.Items.Add("Room 2");
            comboBox1.Items.Add("Room 3");
            comboBox1.Items.Add("Room 4");
            comboBox1.Items.Add("Room 5");
            comboBox1.Items.Add("Hall");
            comboBox1.Items.Add("Loby");
            comboBox1.Items.Add("Espresso Machine");
            
        }

        
        public void set_employee_name(string name)
        {
            this.employee_name = name;
        }
        public string get_employee_name()
        {
            return this.employee_name;
        }
        private void clean()
        {
            selected_to_clean = null;
            foreach (Cleaning room in cleaning_data)
            {
                if (comboBox_RoomtoClean.Text == room.Room)
                {
                    foreach (Cleaning i in cleaning_data)
                    {
                        if (comboBox_afterMovie.Text == room.Title)
                        {
                            foreach (Cleaning j in cleaning_data)
                            {
                                if (comboBox_Time.Text == room.Time)
                                {
                                    selected_to_clean = room;
                                }
                            }
                        }

                    }
                }
            }     
                
            if(selected_to_clean != null)
            {
                item_lb.Text = selected_to_clean.Item;
                seat_lb.Text = selected_to_clean.Dirty;
                looking_lb.Text = selected_to_clean.Looking;
            }
            else
            {
                MessageBox.Show("Please complete all the boxes and try again.", "Attention");
            }
                
            
        
        }
        private void load_cleaning_data()
        {
            string[] lines = File.ReadAllLines(@".\Cleaning_Data.csv", Encoding.UTF8);
            foreach (string line in lines)
            {

                string Title, Item, Dirty, Looking, Room,Time;
                string[] movie_data = line.Split(',');
                Room = movie_data[0]; Title = movie_data[1]; Item = movie_data[2]; Dirty = movie_data[3]; Looking = movie_data[4];Time = movie_data[5];
                cleaning_data.Add(new Cleaning(Room, Title, Item, Dirty, Looking,Time));

            }
        }
        private void clear_cleaning_panel()
        {
            cleaning_lb.Text = "";
            movie_lb.Text = "";
            item_lb.Text = "";
            seat_lb.Text = "";
            looking_lb.Text = "";                            
        }

        public void fill_cleaning_data()
        {
            if (cleaning_data.Count != 0)
            {
                clear_cleaning_panel();
                foreach(Cleaning room in cleaning_data)
                {
                    if (!comboBox_RoomtoClean.Items.Contains(room.Room))
                    {
                        comboBox_RoomtoClean.Items.Add(room.Room);

                    }

                }               
            }
           

        }
        public Cinema_Complex_Employee()
        {
            InitializeComponent();
            fill_combo();
            load_cleaning_data();
            clean_panel.Hide();
            lights_panel.Hide();
            
            
        }

        private void Cinema_Complex_Employee_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are You Sure To Exit Cinema-Complex ?\n Unsaved work will be deleted.", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Environment.Exit(0);
            }
        }

        private void contact_btn_Click(object sender, EventArgs e)
        {
            if(looking_lb.Text == "Yes")
            {
                MessageBox.Show("The client was informed successfully!", "Infos");
            }
            else
            {
                MessageBox.Show("No client is not looking for this item.", "Infos");
            }
            
        }

        private void vacuum_bt_Click(object sender, EventArgs e)
        {
            lights_panel.Hide();
            clean_panel.Show();
            if (cleaning_data.Count != 0)
            {
                next_btn.Enabled = true;
                contact_btn.Enabled = true;
                clear_cleaning_panel();
                fill_cleaning_data();
            }
            else
            {
                clear_cleaning_panel();
                next_btn.Enabled = false;
                contact_btn.Enabled = false;
                MessageBox.Show("All rooms are cleaned!", "Infos");
            }
           

        }

        private void next_btn_Click(object sender, EventArgs e)
        {
            fill_cleaning_data();
            clean();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = null;
            electricity_lb.Text = "";
            clean_panel.Hide();
            lights_panel.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please choose a room or a facility.", "Infos");
            }
            else
            {
                electricity_lb.Text = "ON";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please choose a room or a facility.", "Infos");
            }
            else
            {
                electricity_lb.Text = "OFF";
            }
        }

        private void logout_bt_Click(object sender, EventArgs e)
        {
            Employee_Main main_obj = new Employee_Main();
            main_obj.Show();
            this.Hide();
        }

    

        private void comboBox_RoomtoClean_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_RoomtoClean != null)
            {
                comboBox_afterMovie.Items.Clear();
                comboBox_Time.Items.Clear();
                foreach (Cleaning room in cleaning_data)
                {
                    if (room.Room == comboBox_RoomtoClean.Text)
                    {
                        if (!comboBox_afterMovie.Items.Contains(room.Title))
                        {
                            comboBox_afterMovie.Items.Add(room.Title);
                        }
                        if (!comboBox_Time.Items.Contains(room.Time))
                        {
                            comboBox_Time.Items.Add(room.Time);
                        }

                    }
                }
            }
        }

        private void comboBox_afterMovie_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Time.Items.Clear();
            foreach(Cleaning room in cleaning_data)
            {
                if (room.Room == comboBox_RoomtoClean.Text && room.Title == comboBox_afterMovie.Text)
                {
                    if (!comboBox_Time.Items.Contains(room.Time))
                    {
                        comboBox_Time.Items.Add(room.Time);
                    }
                }   
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            electricity_lb.Text = "";
        }

        private void Cinema_Complex_Employee_Load(object sender, EventArgs e)
        {
            lbl_employee.Text = get_employee_name();
        }
    }
}

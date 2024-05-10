using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMS_Rushdha_New
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
        //    textBox1.KeyDown += enter_funtion;
        //    textBox2.KeyDown += enter_funtion;
        //    this.AcceptButton = button1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Text;
            //this.AcceptButton = button1;
            //if (username != null || password != null)
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Enter Correct Username and Password.");
                }
                else if (AuthenticateUser(username, password))
                {
                    Form2 form2 = new Form2();
                    form2.Show();
                }
                else
                {
                    MessageBox.Show("Wrong UserName or Password!");
                }
            }
        }
        // Form2 form2 = new Form2();
        // form2.Show();



        private bool AuthenticateUser(string username, string password)
        {
            bool isAuthenticated = false;

            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-L1CE92B\\SQLEXPRESS01;Initial Catalog=StudentManagementSystem;Integrated Security=True;");
            //if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            //{
            //    MessageBox.Show("Enter Correct Username and Password.");
            //}
        
            //else
            {
                string query = "SELECT COUNT(*) FROM [user] WHERE username = @username AND password = @password";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@username", username);//avoid injection attacks
                command.Parameters.AddWithValue("@password", password);

                conn.Open();
                int count = (int)command.ExecuteScalar();
                if (count > 0)
                {
                    isAuthenticated = true;
                }
                conn.Close();
            }
            return isAuthenticated;
        }
            
            //private void enter_funtion(object sender, KeyEventArgs e)
            //{
           
            //if (e.KeyCode == Keys.Enter)
            //{
            //    btnLogin.PerformClick();
            //}
            //}

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

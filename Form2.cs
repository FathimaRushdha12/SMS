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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Configuration;


namespace SMS_Rushdha_New
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
           // textBox5.KeyDown += enter_function();
           // this.AcceptButton = button1;
        }
        string connectionstring;
        SqlConnection conn;
        private void Form2_Load(object sender, EventArgs e)
        {
            //txtSearch.KeyDown += functionenter;
            //this.AcceptButton = btnSearch;
            string connectionstring = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionstring);
        }
        //("Data Source=DESKTOP-L1CE92B\\SQLEXPRESS01;Initial Catalog=StudentManagementSystem;Integrated Security=True;");
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open(); //Connection is opened
                SqlCommand sqlCommand = conn.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;

                //Query is addded to the command
                sqlCommand.CommandText = "INSERT INTO Student_Details (Student_ID,FullName,Contact,Email) VALUES (@Student_ID,@FullName, @Contact,@email)";

                //Only the person's name and number is added because the Id value is auto incremented anyways
                sqlCommand.Parameters.AddWithValue("@Student_ID", txtStudentId.Text);
                sqlCommand.Parameters.AddWithValue("@FullName", txtFullName.Text);
                sqlCommand.Parameters.AddWithValue("@Contact", txtContactNo.Text);
                sqlCommand.Parameters.AddWithValue("@Email", txtEmailAddress.Text);

                //If rows are affected we will get the successful message, else error is shown
                int rowsAffected = sqlCommand.ExecuteNonQuery();//return the count of row affected
                if (rowsAffected > 3)
                {
                    MessageBox.Show("Student Added successfully.");

                }
                else
                {
                    MessageBox.Show("Unsuccessful.");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
            finally
            {
                //Connection is closed
                conn.Close();

            }

        }
           
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();//Connection is opened
                SqlCommand sqlCommand = conn.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;

                //When the user entrs the employee id, if the use is present in the database, the user gets deleted
                sqlCommand.CommandText = "DELETE FROM Student_Details WHERE Student_ID = @Student_ID";
                sqlCommand.Parameters.AddWithValue("@Student_ID", txtStudentId.Text);
                int rowsAffected = sqlCommand.ExecuteNonQuery();

                //If rows are affected we will get the successful message, else error is shown

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Student Details deleted successfully.");

                }
                else
                {
                    MessageBox.Show("Unsuccessful!EnterDetails.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
            finally
            {
                //Connection is closed
                conn.Close();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();//Connection is opened
                SqlCommand sqlCommand = conn.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;

                //All employees present in the database is displayed
                sqlCommand.CommandText = "SELECT * FROM Student_Details";
                sqlCommand.ExecuteNonQuery();
                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                dgvDispaly.DataSource = dataTable;
                //dgvStudent_Details.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
            finally
            {
                conn.Close();

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open(); //Connection Opened
                SqlCommand sqlCommand = conn.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;

                //Values of the employee is updated if the employee is present in the database
                sqlCommand.CommandText = "UPDATE Student_Details SET FullName = @FullName, Contact = @Contact , Email=@Email WHERE Student_ID = @Student_ID";
                sqlCommand.Parameters.AddWithValue("@FullName", txtFullName.Text);
                sqlCommand.Parameters.AddWithValue("@Email", txtEmailAddress.Text);
                sqlCommand.Parameters.AddWithValue("@Contact", txtContactNo.Text);
                sqlCommand.Parameters.AddWithValue("@Student_Id", txtStudentId.Text);
                int rowsAffected = sqlCommand.ExecuteNonQuery();
                //If rows are affected we will get the successful message, else error is shown

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record updated successfully.");
                }
                else
                {
                    MessageBox.Show("Unsuccessful.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
            finally
            {
                //Connection is closed
                conn.Close();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open(); // Connection is opened
                SqlCommand sqlCommand = conn.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;

                // Search for students whose ID or FullName matches the input text
                sqlCommand.CommandText = "SELECT * FROM Student_Details WHERE Student_ID LIKE @SearchText OR FullName LIKE @SearchText";
                sqlCommand.Parameters.AddWithValue("@SearchText", "%" + txtSearch.Text + "%"); // Use '%' to match any characters before and after the search text
                //sqlCommand.ExecuteNonQuery();

                DataTable dataTable = new DataTable();//store the query results
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);//bridge between datatable and database
                sqlDataAdapter.Fill(dataTable);//populate the results

                if (dataTable.Rows.Count > 0)
                {
                    // Display the search results in the DataGridView
                    dgvDispaly.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("No matching records found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close(); // Connection is closed
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please enter search criteria.");
                return; // Exit the method without executing the search
            }
            try
            {
                conn.Open(); // Connection is opened
                SqlCommand sqlCommand = conn.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;

                // Search for students whose ID or FullName matches the input text
                sqlCommand.CommandText = "SELECT * FROM Student_Details WHERE Student_ID LIKE @Search OR FullName LIKE @Search";
                sqlCommand.Parameters.AddWithValue("@Search", "%" + txtSearch.Text + "%"); 

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    // Display the search results in the DataGridView
                    dgvDispaly.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("No matching records found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close(); // Connection is closed
            }
        }
        private void functionenter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
            //String search = textBox5.Text;
        }


        //Only allow numerics to be entered as contact details
        //private void txtEmpContact_KeyPress_1(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }

        //}


        //Only allow numeric digits to be entered as an Employee Id
        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }

        }


        //Only allow letters to be entered at Employee Name
        private void txtFullName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
            }
}

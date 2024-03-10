using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace midf1
{
    public partial class Student : Form
    {
        private StudentView studentView;
        public Student()
        {
            InitializeComponent();
            studentView = new StudentView(); 
        }

        private void Student_Load(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Student] (FirstName, LastName, Contact, Email, RegistrationNumber, Status) VALUES (@FirstName, @LastName, @Contact, @Email, @RegistrationNumber, @Status)", con);

            cmd.Parameters.AddWithValue("@FirstName", textBox1.Text);
            cmd.Parameters.AddWithValue("@LastName", textBox2.Text);
            cmd.Parameters.AddWithValue("@Contact", textBox3.Text);
            cmd.Parameters.AddWithValue("@Email", textBox4.Text);
            cmd.Parameters.AddWithValue("@RegistrationNumber", textBox5.Text);

            if (int.TryParse(textBox6.Text, out int Status))
            {
                cmd.Parameters.AddWithValue("@Status", Status);
            }
            else
            {

                MessageBox.Show("Invalid  datatype in Status.Kindly enter integers");
                return;
            }

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Inserted a Student");
                studentView.LoadStudentData();
            }
            else
            {
                MessageBox.Show("Insertion failed.");
            }
            studentView.LoadStudentData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Student] SET FirstName = @FirstName, LastName = @LastName, Contact = @Contact, Email = @Email WHERE [RegistrationNumber] = @RegistrationNumber", con);

            cmd.Parameters.AddWithValue("@FirstName", textBox1.Text);
            cmd.Parameters.AddWithValue("@LastName", textBox2.Text);
            cmd.Parameters.AddWithValue("@Contact", textBox3.Text);
            cmd.Parameters.AddWithValue("@Email", textBox4.Text);
            cmd.Parameters.AddWithValue("@RegistrationNumber", textBox5.Text);




            if (int.TryParse(textBox6.Text, out int Status))
            {
                cmd.Parameters.AddWithValue("@Status", Status);
            }
            else
            {

                MessageBox.Show("Invalid  datatype in Status.Kindly enter integers");
                return;
            }


            int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Successfully Updated a student");
                    studentView.LoadStudentData();

            }
                else
                {
                    MessageBox.Show("Student not found or update failed.");
                     
            }
            
        }

          
     

        private void button3_Click(object sender, EventArgs e)
        {
            studentView.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Student] WHERE [RegistrationNumber] = @RegistrationNumber", con);

            cmd.Parameters.AddWithValue("@RegistrationNumber", textBox5.Text);


            int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Successfully Deleted a student");
                    
                }
                else
                {
                    MessageBox.Show("Student not found or deletion failed.");
                }
            }

        private void button5_Click(object sender, EventArgs e)
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Student] WHERE [RegistrationNumber] = @RegistrationNumber", con);
            cmd.Parameters.AddWithValue("@RegistrationNumber", textBox5.Text);


            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                if (reader.HasRows)
                {

                    reader.Read();


                    textBox1.Text = reader["FirstName"].ToString();
                    textBox2.Text = reader["LastName"].ToString();
                    textBox3.Text = reader["Contact"].ToString();
                    textBox4.Text = reader["Email"].ToString();
                    textBox6.Text = reader["Status"].ToString();

                    MessageBox.Show("Student found.");
                }
                else
                {
                    MessageBox.Show("Student not found.");
                }
            }
        }
    }
}
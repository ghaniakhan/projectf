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

namespace midf1
{
    public partial class ClassAttendence : Form
    {
        private ClassAttendenceView classattendenceView;
        public ClassAttendence()
        {
            InitializeComponent();
            classattendenceView = new ClassAttendenceView();
        }

        private void ClassAttendencecs_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();


            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[ClassAttendance] ([AttendanceDate]) VALUES (@AttendanceDate)", con);





            cmd.Parameters.AddWithValue("@AttendanceDate", dateTimePicker1.Value);




            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Inserted");
            classattendenceView.LoadClassAttendenceData();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("UPDATE  [dbo].[ClassAttendance]  SET  AttendanceDate = @AttendanceDate WHERE Id = @Id", con);

            if (!int.TryParse(textBox7.Text, out int assessmentId))
            {
                MessageBox.Show("Invalid Assessment ID. Please enter a valid integer.");
                return;
            }

            cmd.Parameters.AddWithValue("@Id", assessmentId);
            cmd.Parameters.AddWithValue("@AttendanceDate", dateTimePicker1.Value);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Updated ");
                classattendenceView.LoadClassAttendenceData();
            }
            else
            {
                MessageBox.Show("Attendence not found or Updation failed.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[ClassAttendance] WHERE Id = @Id", con);
            if (!int.TryParse(textBox7.Text, out int Id))
            {
                MessageBox.Show("Invalid ID. Please enter a valid integer.");
                return;
            }

            cmd.Parameters.AddWithValue("@Id", Id);



            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Deleted Class Attendance");
                classattendenceView.LoadClassAttendenceData();
            }
            else
            {
                MessageBox.Show("Class Attendance not found or deletion failed.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            classattendenceView.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Please enter a valid Id for search.");
                return;
            }

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[ClassAttendance] WHERE Id = @Id ", con);
            cmd.Parameters.AddWithValue("@Id", int.Parse(textBox7.Text));


            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                   
                    dateTimePicker1.Value = Convert.ToDateTime(reader["AttendanceDate"]);
                    
                    MessageBox.Show("ClassAttendance found.");
                }
                else
                {
                    MessageBox.Show("ClassAttendance not found.");
                }
            }
        }
    }
}
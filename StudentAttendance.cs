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
    public partial class StudentAttendance : Form
    {
        private StudentAttendanceView studentattendanceView;
        public StudentAttendance()
        {
            InitializeComponent();
            studentattendanceView = new StudentAttendanceView();
        }

        private void StudentAttendance_Load(object sender, EventArgs e)
        {

        }

        private bool IsAttendanceIdExists(int attendanceId)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[ClassAttendance] WHERE Id = @AttendanceId", con);
            cmd.Parameters.AddWithValue("@AttendanceId", attendanceId);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        private bool IsStudentIdExists(int studentId)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Student] WHERE Id = @StudentId", con);
            cmd.Parameters.AddWithValue("@StudentId", studentId);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        private bool IsAttendanceStatusIdExists(int attendanceStatusId)
        {
            var con = Configuration.getInstance().getConnection();

            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Lookup] WHERE LookupId = @AttendanceStatusId AND Category = 'ATTENDANCE_STATUS'", con);
            cmd.Parameters.AddWithValue("@AttendanceStatusId", attendanceStatusId);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var con = Configuration.getInstance().getConnection();

            if (string.IsNullOrWhiteSpace(textBox1.Text) || !int.TryParse(textBox1.Text, out int attendanceId))
            {
                MessageBox.Show("Please enter a valid AttendanceId as an integer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text) || !int.TryParse(textBox2.Text, out int studentId))
            {
                MessageBox.Show("Please enter a valid StudentId as an integer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text) || !int.TryParse(textBox3.Text, out int attendanceStatusId))
            {
                MessageBox.Show("Please enter a valid AttendanceStatusId as an integer.");
                return;
            }

            if (!IsAttendanceIdExists(attendanceId))
            {
                MessageBox.Show("Invalid AttendanceId. Please enter a valid AttendanceId from the ClassAttendance table.");
                return;
            }

            if (!IsStudentIdExists(studentId))
            {
                MessageBox.Show("Invalid StudentId. Please enter a valid StudentId from the Student table.");
                return;
            }

            if (!IsAttendanceStatusIdExists(attendanceStatusId))
            {
                MessageBox.Show("Invalid AttendanceStatusId. Please enter a valid AttendanceStatusId from the Lookup table.");
                return;
            }

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[StudentAttendance] ([AttendanceId], [StudentId], [AttendanceStatus]) VALUES (@AttendanceId, @StudentId, @AttendanceStatus)", con);

            cmd.Parameters.AddWithValue("@AttendanceId", attendanceId);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@AttendanceStatus", attendanceStatusId);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Inserted into StudentAttendance");
            studentattendanceView.LoadStudentAttendanceData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (string.IsNullOrWhiteSpace(textBox1.Text) || !int.TryParse(textBox1.Text, out int attendanceId))
            {
                MessageBox.Show("Please enter a valid AttendanceId as an integer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text) || !int.TryParse(textBox2.Text, out int studentId))
            {
                MessageBox.Show("Please enter a valid StudentId as an integer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text) || !int.TryParse(textBox3.Text, out int attendanceStatusId))
            {
                MessageBox.Show("Please enter a valid AttendanceStatusId as an integer.");
                return;
            }

            if (!IsAttendanceIdExists(attendanceId))
            {
                MessageBox.Show("Invalid AttendanceId. Please enter a valid AttendanceId from the ClassAttendance table.");
                return;
            }

            if (!IsStudentIdExists(studentId))
            {
                MessageBox.Show("Invalid StudentId. Please enter a valid StudentId from the Student table.");
                return;
            }

            if (!IsAttendanceStatusIdExists(attendanceStatusId))
            {
                MessageBox.Show("Invalid AttendanceStatusId. Please enter a valid AttendanceStatusId from the Lookup table.");
                return;
            }


            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[StudentAttendance] SET AttendanceStatus = @AttendanceStatus WHERE AttendanceId = @AttendanceId AND StudentId = @StudentId", con);

            cmd.Parameters.AddWithValue("@AttendanceId", attendanceId);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@AttendanceStatus", attendanceStatusId);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Updated a StudentAttendance");
                studentattendanceView.LoadStudentAttendanceData();
            }
            else
            {
                MessageBox.Show("StudentAttendance not found or Updation failed.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || !int.TryParse(textBox1.Text, out int attendanceId))
            {
                MessageBox.Show("Please enter a valid AttendanceId as an integer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text) || !int.TryParse(textBox2.Text, out int studentId))
            {
                MessageBox.Show("Please enter a valid StudentId as an integer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text) || !int.TryParse(textBox3.Text, out int attendanceStatusId))
            {
                MessageBox.Show("Please enter a valid AttendanceStatusId as an integer.");
                return;
            }

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[StudentAttendance] WHERE AttendanceId = @AttendanceId AND StudentId = @StudentId", con);
            cmd.Parameters.AddWithValue("@AttendanceId", attendanceId);
            cmd.Parameters.AddWithValue("@StudentId", studentId);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Deleted a StudentAttendance");
                studentattendanceView.LoadStudentAttendanceData();
            }
            else
            {
                MessageBox.Show("StudentAttendance not found or deletion failed.");
            }

            con.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || !int.TryParse(textBox1.Text, out int attendanceId))
            {
                MessageBox.Show("Please enter a valid AttendanceId as an integer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text) || !int.TryParse(textBox2.Text, out int studentId))
            {
                MessageBox.Show("Please enter a valid StudentId as an integer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text) || !int.TryParse(textBox3.Text, out int attendanceStatusId))
            {
                MessageBox.Show("Please enter a valid AttendanceStatusId as an integer.");
                return;
            }


            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[StudentAttendance]  WHERE AttendanceId = @AttendanceId AND StudentId = @StudentId", con);
            cmd.Parameters.AddWithValue("@AttendanceId", attendanceId);
            cmd.Parameters.AddWithValue("@StudentId", studentId);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    
                    textBox3.Text = reader["AttendanceStatus"].ToString();

                    MessageBox.Show("StudentAttendance found.");

                }
                else
                {
                    MessageBox.Show("StudentAttendance not found.");
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            studentattendanceView.ShowDialog();
        }
    }
}
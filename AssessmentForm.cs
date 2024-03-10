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
    public partial class AssessmentForm : Form
    {
        private AssessmentView assessmentView;
        public AssessmentForm()
        {
            InitializeComponent();
            assessmentView = new AssessmentView();
        }

        private void AssessmentForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();


            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Assessment] ([Title], [DateCreated], [TotalMarks],[TotalWeightage]) VALUES (@Title, @DateCreated, @TotalMarks,@TotalWeightage)", con);



            cmd.Parameters.AddWithValue("@Title", textBox1.Text);

            cmd.Parameters.AddWithValue("@DateCreated", dateTimePicker1.Value);


            if (int.TryParse(textBox3.Text, out int totalMarks))
            {
                cmd.Parameters.AddWithValue("@TotalMarks", totalMarks);
            }
            else
            {

                MessageBox.Show("Invalid date format in TotalMarks");
                return;
            }

            if (int.TryParse(textBox4.Text, out int TotalWeightage))
            {
                cmd.Parameters.AddWithValue("@TotalWeightage", TotalWeightage);
            }
            else
            {

                MessageBox.Show("Invalid date format in TotalWeightage");
                return;
            }

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Inserted an Assessment");
                assessmentView.LoadAssessmentData();
            }
            else
            {
                MessageBox.Show("Insertion failed.");
            }
            assessmentView.LoadAssessmentData();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("UPDATE  [dbo].[Assessment]  SET  Title = @Title, DateCreated = @DateCreated,  TotalMarks= @TotalMarks,TotalWeightage = @TotalWeightage WHERE Id = @Id", con);

            if (!int.TryParse(textBox7.Text, out int assessmentId))
            {
                MessageBox.Show("Invalid Assessment ID. Please enter a valid integer.");
                return;
            }

            cmd.Parameters.AddWithValue("@Id", assessmentId);

            cmd.Parameters.AddWithValue("@Title", textBox1.Text);

            cmd.Parameters.AddWithValue("@DateCreated", dateTimePicker1.Value);



            if (int.TryParse(textBox3.Text, out int totalMarks))
            {
                cmd.Parameters.AddWithValue("@TotalMarks", totalMarks);
            }
            else
            {

                MessageBox.Show("Invalid date format in TotalMarks");
                return;
            }

            if (int.TryParse(textBox4.Text, out int TotalWeightage))
            {
                cmd.Parameters.AddWithValue("@TotalWeightage", TotalWeightage);
            }
            else
            {

                MessageBox.Show("Invalid date format in TotalWeightage");
                return;
            }





            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Updated an Assessment");
                assessmentView.LoadAssessmentData();
            }
            else
            {
                MessageBox.Show("Assessment not found or Updation failed.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Assessment] WHERE Id = @Id", con);
            if (!int.TryParse(textBox7.Text, out int assessmentId))
            {
                MessageBox.Show("Invalid Assessment ID. Please enter a valid integer.");
                return;
            }

            cmd.Parameters.AddWithValue("@Id", assessmentId);



            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Deleted an Assessment");
                assessmentView.LoadAssessmentData();
            }
            else
            {
                MessageBox.Show("Assessment not found or deletion failed.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Please enter a valid Id for search.");
                return;
            }

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Assessment] WHERE Id = @Id ", con);
            cmd.Parameters.AddWithValue("@Id", int.Parse(textBox7.Text));


            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    textBox1.Text = reader["Title"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(reader["DateCreated"]);
                    textBox3.Text = reader["TotalMarks"].ToString();
                    textBox4.Text = reader["TotalWeightage"].ToString();
                    MessageBox.Show("Assessment found.");
                }
                else
                {
                    MessageBox.Show("Assessment not found.");
                }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            assessmentView.ShowDialog();
        }
    }
}

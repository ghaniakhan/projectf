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
    public partial class StudentResult : Form
    {
        private StudentResultView studentresultView;
        public StudentResult()
        {
            InitializeComponent();
            studentresultView = new StudentResultView();
        }

        private void StudentResult_Load(object sender, EventArgs e)
        {

        }


        private bool IsRubricMeasurementIdExists(int rubricmeasurementId)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[RubricLevel] WHERE Id = @RubricMeasurementId", con);
            cmd.Parameters.AddWithValue("@RubricMeasurementId", rubricmeasurementId);

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

        private bool IsAssessmentComponentIdExists(int assessmentcomponentId)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[AssessmentComponent] WHERE Id = @AssessmentComponentId", con);
            cmd.Parameters.AddWithValue("@AssessmentComponentId", assessmentcomponentId);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            if (string.IsNullOrWhiteSpace(textBox1.Text) || !int.TryParse(textBox1.Text, out int studentId))
            {
                MessageBox.Show("Please enter a valid StudentId as an integer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text) || !int.TryParse(textBox2.Text, out int assessmentcomponentId))
            {
                MessageBox.Show("Please enter a valid AssessmentComponentId as an integer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text) || !int.TryParse(textBox3.Text, out int rubricmeasurementId))
            {
                MessageBox.Show("Please enter a valid RubricMeasurementId as an integer.");
                return;
            }

            if (!IsAssessmentComponentIdExists(assessmentcomponentId))
            {
                MessageBox.Show("Invalid AssessmentComponentId. Please enter a valid AssessmentComponentId from the Assessment table.");
                return;
            }

            if (!IsStudentIdExists(studentId))
            {
                MessageBox.Show("Invalid StudentId. Please enter a valid StudentId from the Student table.");
                return;
            }

            if (!IsRubricMeasurementIdExists(rubricmeasurementId))
            {
                MessageBox.Show("Invalid RubricMeasurementId. Please enter a valid RubricMeasurementId from the RubricLevel table.");
                return;
            }

            // Check if the record already exists
            if (IsStudentResultExists(studentId, assessmentcomponentId))
            {
                MessageBox.Show("The record with the given StudentId and AssessmentComponentId already exists in StudentResult table.");
                return;
            }

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[StudentResult] ( [StudentId],[AssessmentComponentId], [RubricMeasurementId],[EvaluationDate]) VALUES ( @StudentId, @AssessmentComponentId,@RubricMeasurementId,@EvaluationDate)", con);


            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@AssessmentComponentId", assessmentcomponentId);
            cmd.Parameters.AddWithValue("@RubricMeasurementId", rubricmeasurementId);
            cmd.Parameters.AddWithValue("@EvaluationDate", dateTimePicker1.Value);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Inserted into Student Result");
            studentresultView.LoadStudentResultData();
        }



        private bool IsStudentResultExists(int studentId, int assessmentcomponentId)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[StudentResult] WHERE StudentId = @StudentId AND AssessmentComponentId = @AssessmentComponentId", con);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@AssessmentComponentId", assessmentcomponentId);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            studentresultView.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            if (string.IsNullOrWhiteSpace(textBox1.Text) || !int.TryParse(textBox1.Text, out int studentId))
            {
                MessageBox.Show("Please enter a valid StudentId as an integer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text) || !int.TryParse(textBox2.Text, out int assessmentcomponentId))
            {
                MessageBox.Show("Please enter a valid AssessmentComponentId as an integer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text) || !int.TryParse(textBox3.Text, out int rubricmeasurementId))
            {
                MessageBox.Show("Please enter a valid RubricMeasurementId as an integer.");
                return;
            }

            if (!IsAssessmentComponentIdExists(assessmentcomponentId))
            {
                MessageBox.Show("Invalid AssessmentComponentId. Please enter a valid AssessmentComponentId from the Assessment table.");
                return;
            }

            if (!IsStudentIdExists(studentId))
            {
                MessageBox.Show("Invalid StudentId. Please enter a valid StudentId from the Student table.");
                return;
            }

            if (!IsRubricMeasurementIdExists(rubricmeasurementId))
            {
                MessageBox.Show("Invalid RubricMeasurementId. Please enter a valid RubricMeasurementId from the RubricLevel table.");
                return;
            }

            // Check if the record already exists
            if (IsStudentResultExists(studentId, assessmentcomponentId))
            {
                MessageBox.Show("The record with the given StudentId and AssessmentComponentId already exists in StudentResult table.");
                return;
            }


            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[StudentResult] SET [RubricMeasurementId] = @RubricMeasurementId, [EvaluationDate] = @EvaluationDate WHERE [StudentId] = @StudentId AND [AssessmentComponentId] = @AssessmentComponentId", con);


            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@AssessmentComponentId", assessmentcomponentId);
            cmd.Parameters.AddWithValue("@RubricMeasurementId", int.Parse(textBox3.Text));
            cmd.Parameters.AddWithValue("@EvaluationDate", dateTimePicker1.Value);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Updated Student Result");
            studentresultView.LoadStudentResultData();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();


            if (string.IsNullOrWhiteSpace(textBox1.Text) || !int.TryParse(textBox1.Text, out int studentId))
            {
                MessageBox.Show("Please enter a valid StudentId as an integer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text) || !int.TryParse(textBox2.Text, out int assessmentcomponentId))
            {
                MessageBox.Show("Please enter a valid AssessmentComponentId as an integer.");
                return;
            }


            if (!IsStudentResultExists(studentId, assessmentcomponentId))
            {
                MessageBox.Show("The record with the given StudentId and AssessmentComponentId does not exist in StudentResult table. Deletion not allowed.");
                return;
            }


            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[StudentResult] WHERE [StudentId] = @StudentId AND [AssessmentComponentId] = @AssessmentComponentId", con);


            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@AssessmentComponentId", assessmentcomponentId);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Deleted Student Result");
            studentresultView.LoadStudentResultData();

        }

        private void button5_Click(object sender, EventArgs e)
        {

            var con = Configuration.getInstance().getConnection();

            // Validate input values
            if (string.IsNullOrWhiteSpace(textBox1.Text) || !int.TryParse(textBox1.Text, out int studentId))
            {
                MessageBox.Show("Please enter a valid StudentId as an integer for search.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text) || !int.TryParse(textBox2.Text, out int assessmentcomponentId))
            {
                MessageBox.Show("Please enter a valid AssessmentComponentId as an integer for search.");
                return;
            }


            if (!IsStudentResultExists(studentId, assessmentcomponentId))
            {
                MessageBox.Show("The record with the given StudentId and AssessmentComponentId does not exist in StudentResult table.");
                return;
            }

            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[StudentResult] WHERE [StudentId] = @StudentId AND [AssessmentComponentId] = @AssessmentComponentId", con);


            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@AssessmentComponentId", assessmentcomponentId);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();


                    textBox1.Text = reader["StudentId"].ToString();
                    textBox2.Text = reader["AssessmentComponentId"].ToString();
                    textBox3.Text = reader["RubricMeasurementId"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(reader["EvaluationDate"]);


                    MessageBox.Show("Student Result found.");
                }
                else
                {
                    MessageBox.Show("Student Result not found.");
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
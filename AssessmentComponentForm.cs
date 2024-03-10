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
    public partial class AssessmentComponentForm : Form
    {
        private AssessmentComponentView assessmentComponentView;
        public AssessmentComponentForm()
        {
            InitializeComponent();
            assessmentComponentView = new AssessmentComponentView();
        }

        private void AssessmentComponentForm_Load(object sender, EventArgs e)
        {

        }



        private bool RubricIdExists(int rubricId, SqlConnection connection)
        {
            using (SqlCommand cmdCheck = new SqlCommand("SELECT 1 FROM [dbo].[Rubric] WHERE Id = @RubricId", connection))
            {
                cmdCheck.Parameters.AddWithValue("@RubricId", rubricId);
                return cmdCheck.ExecuteScalar() != null;
            }
        }


        private bool AssessmentIdExists(int assessmentId, SqlConnection connection)
        {
            using (SqlCommand cmdCheck = new SqlCommand("SELECT 1 FROM [dbo].[Assessment] WHERE Id = @AssessmentId", connection))
            {
                cmdCheck.Parameters.AddWithValue("@AssessmentId", assessmentId);
                return cmdCheck.ExecuteScalar() != null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();


            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[AssessmentComponent] ([Name], [RubricId], [TotalMarks],[DateCreated],[DateUpdated],[AssessmentId]) VALUES (@Name, @RubricId, @TotalMarks,@DateCreated,@DateUpdated,@AssessmentId)", con);




            // Validate RubricId
            if (!int.TryParse(textBox2.Text, out int RubricId) || RubricId < 0 || !RubricIdExists(RubricId, con))
            {
                MessageBox.Show("Please enter a valid positive integer for 'RubricId' that exists in the Rubric table.");
                return;
            }

            // Validate TotalMarks
            if (!int.TryParse(textBox3.Text, out int totalMarks) || totalMarks < 0)
            {
                MessageBox.Show("Please enter a valid positive integer for 'TotalMarks'.");
                return;
            }

            // Validate AssessmentId
            if (!int.TryParse(textBox8.Text, out int AssessmentId) || AssessmentId < 0 || !AssessmentIdExists(AssessmentId, con))
            {
                MessageBox.Show("Please enter a valid positive integer for 'AssessmentId' that exists in the Assessment table.");
                return;
            }

           
            cmd.Parameters.AddWithValue("@Name", textBox1.Text);
            cmd.Parameters.AddWithValue("@RubricId", RubricId);
            cmd.Parameters.AddWithValue("@TotalMarks", totalMarks);
            cmd.Parameters.AddWithValue("@DateCreated", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@DateUpdated", dateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@AssessmentId", AssessmentId);





            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Inserted an AssessmentComponent");
                assessmentComponentView.LoadAssessmentComponentData();
            }
            else
            {
                MessageBox.Show("Insertion failed.");
            }
            assessmentComponentView.LoadAssessmentComponentData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("UPDATE  [dbo].[AssessmentComponent]  SET  Name = @Name, RubricId = @RubricId,  TotalMarks= @TotalMarks,DateCreated = @DateCreated, DateUpdated = @DateUpdated,AssessmentId= @AssessmentId WHERE Id = @Id", con);

            // Validate Id
            if (!int.TryParse(textBox7.Text, out int Id) || Id < 0)
            {
                MessageBox.Show("Please enter a valid positive integer for 'Id'.");
                return;
            }



            // Validate RubricId
            if (!int.TryParse(textBox2.Text, out int RubricId) || RubricId < 0 || !RubricIdExists(RubricId, con))
            {
                MessageBox.Show("Please enter a valid positive integer for 'RubricId' that exists in the Rubric table.");
                return;
            }

            // Validate TotalMarks
            if (!int.TryParse(textBox3.Text, out int totalMarks) || totalMarks < 0)
            {
                MessageBox.Show("Please enter a valid positive integer for 'TotalMarks'.");
                return;
            }

            // Validate AssessmentId
            if (!int.TryParse(textBox8.Text, out int AssessmentId) || AssessmentId < 0 || !AssessmentIdExists(AssessmentId, con))
            {
                MessageBox.Show("Please enter a valid positive integer for 'AssessmentId' that exists in the Assessment table.");
                return;
            }

            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@Name", textBox1.Text);
            cmd.Parameters.AddWithValue("@RubricId", RubricId);
            cmd.Parameters.AddWithValue("@TotalMarks", totalMarks);
            cmd.Parameters.AddWithValue("@DateCreated", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@DateUpdated", dateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@AssessmentId", AssessmentId);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Updated an AssessmentComponent");
                assessmentComponentView.LoadAssessmentComponentData();
            }
            else
            {
                MessageBox.Show("Updation failed.");
            }
            assessmentComponentView.LoadAssessmentComponentData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[AssessmentComponent] WHERE Id = @Id", con);
            if (!int.TryParse(textBox7.Text, out int Id))
            {
                MessageBox.Show("Invalid  ID. Please enter a valid integer.");
                return;
            }

            cmd.Parameters.AddWithValue("@Id", Id);



            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Deleted an AssessmentComponent");
                assessmentComponentView.LoadAssessmentComponentData();
            }
            else
            {
                MessageBox.Show("AssessmentComponent not found or deletion failed.");
            }
        }




        private void button5_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Please enter a valid Assessment Component Id for search.");
                return;
            }

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[AssessmentComponent] WHERE Id = @Id ", con);
            cmd.Parameters.AddWithValue("@Id", int.Parse(textBox7.Text));


            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    textBox1.Text = reader["Name"].ToString();
                    textBox2.Text = reader["RubricId"].ToString();
                    textBox3.Text = reader["TotalMarks"].ToString();

                    dateTimePicker1.Value = Convert.ToDateTime(reader["DateCreated"]);
                    dateTimePicker2.Value = Convert.ToDateTime(reader["DateUpdated"]);
                    textBox8.Text = reader["AssessmentId"].ToString();




                    MessageBox.Show("AssessmentComponent found.");
                }
                else
                {
                    MessageBox.Show("AssessmentComponent not found.");
                }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {

            assessmentComponentView.ShowDialog();
        }
    }
}

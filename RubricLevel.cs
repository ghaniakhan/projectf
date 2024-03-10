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
    public partial class RubricLevel : Form
    {
        private RubricLevelView rubricLevelView;
        public RubricLevel()
        {
            InitializeComponent();
            rubricLevelView = new RubricLevelView();

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (string.IsNullOrWhiteSpace(textBox1.Text) || !int.TryParse(textBox1.Text, out int rubricId))
            {
                MessageBox.Show("Please enter a valid RubricId as an integer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text) || !int.TryParse(textBox3.Text, out int measurementLevel))
            {
                MessageBox.Show("Please enter a valid MeasurementLevel as an integer.");
                return;
            }

            if (!IsRubriIdExists(rubricId))
            {
                MessageBox.Show("Invalid RubriId. Please enter a valid RubriId from the Rubric table.");
                return;
            }

            SqlCommand cmd = new SqlCommand("UPDATE  [dbo].[RubricLevel]  SET  RubricId=@RubricId, Details = @Details, MeasurementLevel = @MeasurementLevel  WHERE Id = @Id", con);

            cmd.Parameters.AddWithValue("@Id", int.Parse(textBox7.Text));
            cmd.Parameters.AddWithValue("@RubricId", rubricId);
            cmd.Parameters.AddWithValue("@Details", textBox2.Text);
            cmd.Parameters.AddWithValue("@MeasurementLevel", measurementLevel);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Updated a RubricLevel");
                rubricLevelView.LoadRubricLevelData();
            }
            else
            {
                MessageBox.Show("RubricLevel not found or Updation failed.");
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (string.IsNullOrWhiteSpace(textBox1.Text) || !int.TryParse(textBox1.Text, out int rubricId))
            {
                MessageBox.Show("Please enter a valid RubricId as an integer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text) || !int.TryParse(textBox3.Text, out int measurementLevel))
            {
                MessageBox.Show("Please enter a valid MeasurementLevel as an integer.");
                return;
            }

            if (!IsRubriIdExists(rubricId))
            {
                MessageBox.Show("Invalid RubriId. Please enter a valid RubriId from the Rubric table.");
                return;
            }

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[RubricLevel] ([RubricId],[Details], [MeasurementLevel]) VALUES (@RubricId,@Details, @MeasurementLevel)", con);

            cmd.Parameters.AddWithValue("@RubricId", rubricId);
            cmd.Parameters.AddWithValue("@Details", textBox2.Text);
            cmd.Parameters.AddWithValue("@MeasurementLevel", measurementLevel);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Inserted");
            rubricLevelView.LoadRubricLevelData();
        }

        private bool IsRubriIdExists(int rubricId)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Rubric] WHERE Id = @RubricId", con); 
            cmd.Parameters.AddWithValue("@RubricId", rubricId);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }





        private void RubricLevel_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Please enter a valid Id for deletion.");
                return;
            }

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[RubricLevel] WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", int.Parse(textBox7.Text));

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Deleted a RubricLevel");
                rubricLevelView.LoadRubricLevelData();
            }
            else
            {
                MessageBox.Show("RubricLevel not found or deletion failed.");
            }

            con.Close();
        
    }

        private void button5_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox7.Text) || !int.TryParse(textBox7.Text, out int id))
            {
                MessageBox.Show("Please enter a valid ID as an integer for search.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text) || !int.TryParse(textBox3.Text, out int measurementLevel))
            {
                MessageBox.Show("Please enter a valid MeasurementLevel as an integer for search.");
                return;
            }

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[RubricLevel] WHERE Id = @Id ", con);
            cmd.Parameters.AddWithValue("@Id", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    textBox2.Text = reader["Details"].ToString();
                    textBox3.Text = reader["MeasurementLevel"].ToString();

                    if (int.TryParse(reader["RubricId"].ToString(), out int rubricId))
                    {
                        textBox1.Text = rubricId.ToString();
                        MessageBox.Show("Rubric found.");
                    }
                    else
                    {
                        MessageBox.Show("Invalid data for RubricId in the database.");
                    }
                }
                else
                {
                    MessageBox.Show("RubricLevel not found.");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rubricLevelView.ShowDialog();
        }
    }
}
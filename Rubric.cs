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
    public partial class Rubric : Form
    {
        private RubricView rubricView;
        public Rubric()
        {
            InitializeComponent();
            rubricView = new RubricView();

        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

           
            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Please enter a valid ID.");
                return;
            }

           
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter a valid CloID.");
                return;
            }

           
            if (IsIdExists(int.Parse(textBox7.Text)))
            {
                MessageBox.Show("ID already exists. Please enter a unique ID.");
                return;
            }

            
            if (!IsCloIdExists(int.Parse(textBox2.Text)))
            {
                MessageBox.Show("Invalid CloID. Please enter a valid CloID from the Clo table.");
                return;
            }

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Rubric] ([Id],[Details], [CloId]) VALUES (@Id,@Details, @CloId)", con);
            cmd.Parameters.AddWithValue("@Id", int.Parse(textBox7.Text));
            cmd.Parameters.AddWithValue("@Details", textBox1.Text);
            cmd.Parameters.AddWithValue("@CloId", int.Parse(textBox2.Text)); 

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Inserted");
            rubricView.LoadRubricData();
        }

        private bool IsCloIdExists(int cloId)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Clo] WHERE Id = @CloId", con); 
            cmd.Parameters.AddWithValue("@CloId", cloId);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter a valid CloID.");
                return;
            }

           
            if (!IsCloIdExists(int.Parse(textBox2.Text)))
            {
                MessageBox.Show("Invalid CloID. Please enter a valid CloID from the Clo table.");
                return;
            }

            SqlCommand cmd = new SqlCommand("UPDATE  [dbo].[Rubric]  SET  Details = @Details, CloId = @CloId  WHERE Id = @Id", con);

            cmd.Parameters.AddWithValue("@Id", int.Parse(textBox7.Text));
            cmd.Parameters.AddWithValue("@Details", textBox1.Text);
            cmd.Parameters.AddWithValue("@CloId", int.Parse(textBox2.Text)); 

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Updated a Rubric");
                rubricView.LoadRubricData();
            }
            else
            {
                MessageBox.Show("Rubric not found or Updation failed.");
            }
        }


        private bool IsIdExists(int id)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Rubric] WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }




        private void button3_Click(object sender, EventArgs e)
        {
            rubricView.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Please enter a valid Id for deletion.");
                return;
            }

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Rubric] WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", int.Parse(textBox7.Text));

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Deleted a Rubric");
                rubricView.LoadRubricData();
            }
            else
            {
                MessageBox.Show("Rubric not found or deletion failed.");
            }

            con.Close();
        }



        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Please enter a valid ID for search.");
                return;
            }

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Rubric] WHERE Id = @Id ", con);
            cmd.Parameters.AddWithValue("@Id", int.Parse(textBox7.Text));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    textBox1.Text = reader["Details"].ToString();


                    if (int.TryParse(reader["CloId"].ToString(), out int cloId))
                    {
                        textBox2.Text = cloId.ToString();
                        MessageBox.Show("Rubric found.");
                    }
                    else
                    {
                        MessageBox.Show("Invalid data for CloId in the database.");
                    }
                }
                else
                {
                    MessageBox.Show("Rubric not found.");
                }
            }
        }
    }
}
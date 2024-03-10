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
    public partial class CLOForm : Form
    {
        private CLOViewForm cloViewForm;
        public CLOForm()
        {
            InitializeComponent();
            cloViewForm = new CLOViewForm();
        }

        private void CLOForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();


            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Clo] ([Name], [DateCreated], [DateUpdated]) VALUES (@Name, @DateCreated, @DateUpdated)", con);

            cmd.Parameters.AddWithValue("@Name", textBox1.Text);
            cmd.Parameters.AddWithValue("@DateCreated", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@DateUpdated", dateTimePicker2.Value);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully added a new CLO");
            cloViewForm.LoadCLOData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("UPDATE  [dbo].[Clo]  SET  Name = @Name, DateCreated = @DateCreated,  DateUpdated= @DateUpdated WHERE Id = @Id", con);

            cmd.Parameters.AddWithValue("@Id", int.Parse(textBox7.Text));

            cmd.Parameters.AddWithValue("@Name", textBox1.Text);
            cmd.Parameters.AddWithValue("@DateCreated", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@DateUpdated", dateTimePicker2.Value);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Updated a CLO");
                cloViewForm.LoadCLOData();
            }
            else
            {
                MessageBox.Show("CLO not found or Updation failed.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Please enter a valid Id for deletion.");
                return;
            }

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Clo] WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", int.Parse(textBox7.Text));


            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Deleted a CLO");
                cloViewForm.LoadCLOData();
            }
            else
            {
                MessageBox.Show("CLO not found or deletion failed.");
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
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Clo] WHERE Id = @Id ", con);
            cmd.Parameters.AddWithValue("@Id", int.Parse(textBox7.Text));


            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    textBox1.Text = reader["Name"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(reader["DateCreated"]); 
                    dateTimePicker2.Value = Convert.ToDateTime(reader["DateUpdated"]); 
                    MessageBox.Show("CLO found.");
                }

                else
                {
                    MessageBox.Show("CLO not found.");
                }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            cloViewForm.ShowDialog();
        }
    }
}
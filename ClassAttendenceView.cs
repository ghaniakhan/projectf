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
    public partial class ClassAttendenceView : Form
    {

        private readonly SqlConnection con;
        public ClassAttendenceView()
        {
            InitializeComponent();
        }

        private void ClassAttendenceView_Load(object sender, EventArgs e)
        {
            LoadClassAttendenceData();
        }

        public void LoadClassAttendenceData()
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[ClassAttendance]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }
            private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

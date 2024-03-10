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
    public partial class StudentAttendanceView : Form
    {
        private readonly SqlConnection con;
        public StudentAttendanceView()
        {
            InitializeComponent();
            con = Configuration.getInstance().getConnection();

        }

        private void StudentAttendanceView_Load(object sender, EventArgs e)
        {
            LoadStudentAttendanceData();
        }


        public void LoadStudentAttendanceData()
        {


            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[StudentAttendance]", con);
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

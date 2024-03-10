using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace midf1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Student());
            //Application.Run(new CLOForm());
            //Application.Run(new AssessmentForm());

            //Application.Run(new Rubric());
            // Application.Run(new RubricLevel());

            //Application.Run(new  StudentAttendance());
            // Application.Run(new ClassAttendence());
            Application.Run(new AssessmentComponentForm());
          // Application.Run(new StudentResult());




        }
    }
}

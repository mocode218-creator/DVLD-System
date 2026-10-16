using DVLD_PrsentationLayer.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DVLD_PrsentationLayer
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
           Application.Run(new frmLogin());
            // Application.Run(new DVLD_PrsentationLayer.people.frmFindPerson());
            //Application.Run(new DVLD_PrsentationLayer.people.frmListPeople());
            //Application.Run(new DVLD_PrsentationLayer.people.frmAddUpdatePerson());
            //Application.Run(new Drivers.frmListDrivers());


        }
    }
}

using System;
using System.Windows.Forms;

namespace Siyakhula.Admin.Desktop
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // Swapping out standard Form1 instantiation for the security gate execution loop
            Application.Run(new LoginForm());
        }
    }
}

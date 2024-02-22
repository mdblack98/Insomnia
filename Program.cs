using System;
using System.Diagnostics;
using System.Security.Principal;

namespace Insomnia
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            bool autoOff = false;
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].Equals("-autooff"))
                    {
                        autoOff = true;
                    }
                }
            }
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            if (IsUserAdministrator())
            {
                ApplicationConfiguration.Initialize();
                try
                {
                    Application.Run(new Form1(autoOff));
                }
                catch { }
            }
            else
            {
                try
                {
                    // Setting up start info of the new process to run as admin
                    var startInfo = new ProcessStartInfo
                    {
                        UseShellExecute = true,
                        WorkingDirectory = Environment.CurrentDirectory,
                        FileName = System.Reflection.Assembly.GetExecutingAssembly().Location,
                        Verb = "runas"
                    };
                    startInfo.FileName = startInfo.FileName.Replace(".dll", ".exe");
                    // Start the new process
                    Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }

                // Exit the current process
                Environment.Exit(0);
            }
        }
        private static bool IsUserAdministrator()
        {
            bool isAdmin;
            try
            {
                // Get the current Windows user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (Exception ex)
            {
                isAdmin = false;
                Console.WriteLine("Exception: " + ex.Message);
            }
            return isAdmin;
        }
    }
}
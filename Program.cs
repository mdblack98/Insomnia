using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            if (IsUserAdministrator())
            {
                ApplicationConfiguration.Initialize();
                try
                {
                    Application.Run(new Form1());
                }
                catch { }
            }
            else
            {
                try
                {
                    var form1 = new Form1();
                    form1.Show();
                    Application.DoEvents();
                    //for(int i=0; i < 10; i++) { Thread.Sleep(300);Application.DoEvents(); }
                    //form1.List();
                    if (form1.count > 0) // then we need to change power off and need Admin
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
                    else if (form1.autoExit)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        //form1.List();
                        Application.Run(form1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }

                // Exit the current process
                Environment.Exit(0);
            }
        }
        public static bool IsUserAdministrator()
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
using System;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using Microsoft.VisualBasic.Devices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Security;
using Microsoft.Win32;
using System.Management;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace Insomnia
{
    public partial class Form1 : Form
    {
        public int count = 0;
        public bool autoOff = false;
        public bool autoExit = false;
        private static ManagementObjectSearcher? searcher = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkBoxAutoExit.Checked = Properties.Settings.Default.autoExit;
            checkBoxAutoOff.Checked = Properties.Settings.Default.autoOff;
            checkBoxAutoExit.Enabled = checkBoxAutoOff.Checked;
            Width = Properties.Settings.Default.width;
            Height = Properties.Settings.Default.height;
            Location = new Point(Properties.Settings.Default.X,Properties.Settings.Default.Y);
            buttonKeepAwake.Enabled = true;
            EnsureWithinScreenBounds();
            if (!Program.IsUserAdministrator())
            {
                richTextBox1.AppendText("Must run as Administrator for Keep Awake to be enabled\r\n");
                buttonKeepAwake.Enabled = false;
            }
        }
        private void EnsureWithinScreenBounds()
        {
            // Get the working area of the primary display
            if (Screen.PrimaryScreen == null)
            {
                MessageBox.Show("Hmmm....could not find PrimaryScreen...please contact mdblack98@yahoo.com");
                return;
            }
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            // Check and adjust the Form's position to ensure it's completely within the working area
            if (this.Left < workingArea.Left) this.Left = workingArea.Left;
            if (this.Top < workingArea.Top) this.Top = workingArea.Top;
            //if (this.Right > workingArea.Right) this.Left = workingArea.Right - this.Width;
            //if (this.Bottom > workingArea.Bottom) this.Top = workingArea.Bottom - this.Height;
        }
        private void keepAwake()
        {
            if (!Program.IsUserAdministrator())
            {
                MessageBox.Show("Must run as Administrator to keep awake");
                return;
            }
            richTextBox1.Clear();
            count = 0;
            richTextBox1.AppendText("Reset all to keep awake\n");
            var key = "SYSTEM\\CurrentControlSet\\Enum\\USB";
            SearchRegistryForKey(key, true);
            String items = count > 1 ? " items" : " item";
            richTextBox1.AppendText("Changed " + count + items + "\n");
        }

        private void buttonKeepAwake_Click(object sender, EventArgs e)
        {
            keepAwake();
        }


        public static string? GetUsbDeviceCommonName(string vid, string pid)
        {
            string? commonName = "No device name";
            string queryString = "SELECT * FROM Win32_PnPEntity WHERE DeviceID LIKE '%USB%'";
            if (searcher == null) searcher = new ManagementObjectSearcher(queryString);
            try
            {
                // Query string to search for USB devices

                // Create a new ManagementObjectSearcher
                using (searcher)
                {
                    // Execute the query
                    foreach (var device in searcher.Get())
                    {
                        string? deviceId = device["DeviceID"].ToString();
                        // Check if the device ID contains the VID and PID
                        if (deviceId != null && deviceId.Contains($"VID_{vid}&PID_{pid}"))
                        {
                            // Get the common name of the device
                            commonName = device["Name"].ToString();
                            break; // Break the loop if device is found
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., ManagementException)
                commonName = $"Error: {ex.Message}";
            }

            return commonName;
        }
        public bool List()
        {
            richTextBox1.Clear();
            if (!Program.IsUserAdministrator())
            {
                richTextBox1.AppendText("Must run as Administrator to keep awake\n");
            }
            count = 0;
            var key = "SYSTEM\\CurrentControlSet\\Enum\\USB";
            SearchRegistryForKey(key);
            if (count > 0)
            {
                richTextBox1.AppendText("Need to change " + count + " item" + (count > 1?"s":"") + "\n");
                Application.DoEvents();
                Thread.Sleep(3000);
                if (!Program.IsUserAdministrator())
                {
                    richTextBox1.AppendText("Must run as Administrator to keep awake");
                    buttonKeepAwake.Enabled = false;
                }
                else
                {
                    buttonKeepAwake.Enabled = true;
                    if (checkBoxAutoExit.Checked) richTextBox1.AppendText("Will restart in admin mode...");
                    else richTextBox1.AppendText("Click Keep Awake button to change\n");
                }
            }
            else
            {
                richTextBox1.AppendText("All USB items have sleep disabled\n");
            }
            return count > 0;
        }
        private void buttonList_Click(object sender, EventArgs e)
        {
            List();
        }
        String? ParseVIDPID(string vidpid)
        {
            // Regular expression pattern to match VID and PID
            string pattern = @"VID_([0-9A-Fa-f]+)&PID_([0-9A-Fa-f]+)";

            // Try to find a match in the input string
            Match match = Regex.Match(vidpid, pattern);
            string vidString = "";
            string pidString = "";
            if (match.Success)
            {
                // Extract VID and PID as strings
                vidString = match.Groups[1].Value;
                pidString = match.Groups[2].Value;

                // Convert VID and PID to integers (assuming they are in hexadecimal format)
                //int vid = Convert.ToInt32(vidString, 16);
                //int pid = Convert.ToInt32(pidString, 16);
                //Console.WriteLine($"VID: {vid} (0x{vidString})");
                //Console.WriteLine($"PID: {pid} (0x{pidString})");

            }
            else
            {
                return "";
            }
            return GetUsbDeviceCommonName(vidString, pidString);
        }
        public void SearchRegistryForKey(string rootPath, bool turnOff = false)
        {
            try
            {
                //            using (RegistryKey rootKey = Registry.LocalMachine.OpenSubKey(rootPath))
                string user = Environment.UserDomainName + "\\" + Environment.UserName;
            var rule = new RegistryAccessRule(user,
                 RegistryRights.ChangePermissions,
                 InheritanceFlags.ContainerInherit,
                 PropagationFlags.InheritOnly | PropagationFlags.NoPropagateInherit,
                 AccessControlType.Allow);
            var mSec = new RegistrySecurity();
            mSec.AddAccessRule(rule);
            using (RegistryKey? rootKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(rootPath))
            {
                if (rootKey != null)
                {
                    //richTextBox1.AppendText("Here#1 " + rootPath+"\n"); Application.DoEvents();
                    foreach (string subKeyName in rootKey.GetSubKeyNames())
                    {
                            //richTextBox1.AppendText("Examine: " + subKeyName+"\n");
                            if (subKeyName.Contains("Properties")) continue;
                            if (subKeyName.Contains("uxd")) continue;
                            using (RegistryKey? subKey = rootKey.OpenSubKey(subKeyName))
                            {
                                //if (subKey.Name.Contains("RegistryKey")) 
                                //    continue;
                                if (subKey != null)
                                {
                                    //    return;
                                    //if (subKey.Name.Contains("uxd")) 
                                    //    return;
                                    // Recursively search in subkeys
                                    //richTextBox1.AppendText(subKey.Name+"***");
                                    SearchRegistryForKey($"{rootPath}\\{subKeyName}", turnOff);
                                    //richTextBox1.AppendText("Here#3 " + subKeyName + "\n"); Application.DoEvents();

                                    // Example: Check for a specific value, adjust the value name as needed
                                    object? value = null;
                                    if (subKey.Name.Contains("WDF")) value = subKey.GetValue("IdleInWorkingState");
                                    if (value != null && value.ToString() != null)
                                    {
                                        var changed = "";
                                        var s = value.ToString();
                                        if (s != null && s.Equals("1") && turnOff == true)
                                        {
                                            try
                                            {
                                                //rootKey.SetValue(subKeyName, 0, RegistryValueKind.DWord);
                                                var subKey2 = rootKey.OpenSubKey(subKeyName, true);
                                                if (subKey2 != null) subKey2.SetValue("IdleInWorkingState", 0);
                                                s = "0";
                                            }
                                            catch (Exception ex)
                                            {
                                                richTextBox1.AppendText(ex.Message);
                                            }
                                            changed = "Changed";
                                            count++;
                                        }
                                        if (value != null && value.ToString() != null)
                                        {
                                            if (value.ToString().Equals("1", StringComparison.Ordinal))
                                            {
                                                changed = ":Change needed";
                                                count++;
                                            }
                                        }
                                        try
                                        {
                                            int i1 = subKey.ToString().IndexOf("USB");
                                            if (i1 != -1)
                                            {
                                                var vidpid = ParseVIDPID(subKey.ToString());
                                                var s1 = subKey.ToString()[i1..];
                                                if (s1 != null && value != null)
                                                {
                                                    if (vidpid != null && vidpid == "") vidpid = "Unknown Description";
                                                    richTextBox1.AppendText(value.ToString() + ":" + vidpid + ":" + s1 + changed + "\n");
                                                    richTextBox1.ScrollToCaret();
                                                    Application.DoEvents();
                                                }
                                            }
                                        }
                                        catch (Exception e) { richTextBox1.AppendText(e.Message+"\n"); }
                                    }
                                }
                            }
                        }
                    }
                    Application.DoEvents();
                }
            }
            catch (Exception e)
            {
                richTextBox1.AppendText(e.Message+"\n"+e.StackTrace+"\n");
                Application.DoEvents();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo("https://www.qrz.com/db/W9MDB") { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabelHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                var url = Application.StartupPath + "/Insomnia.htm";
                url = url.Replace("\\", "/");
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            bool count = false;
            if (autoOff)
            {
                if (Program.IsUserAdministrator())
                {
                    // then we are in admin which means we are changing things
                    // so we don't need to list
                    keepAwake();
                }
                else if (count = List())  // First call when 
                {
                    if (autoOff)
                    {
                        keepAwake();
                    }
                }
            }
            if (autoExit && !count)
            {
                richTextBox1.AppendText("Autoexit selected so will exit");
                Application.DoEvents();
                for (int i = 0; i < 11; ++i)
                {
                    Application.DoEvents();
                    richTextBox1.AppendText("..." + (10 - i));
                    Thread.Sleep(1000);
                }
                this.Close();
            }
        }

        private void checkBoxAutoOff_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.autoOff = checkBoxAutoOff.Checked;
            Properties.Settings.Default.Save();
            autoOff = checkBoxAutoOff.Checked;
            if (!autoOff) checkBoxAutoExit.Checked = autoOff;
            checkBoxAutoExit.Enabled = autoOff;
        }

        private void checkBoxAutoExit_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.autoExit = checkBoxAutoExit.Checked;
            Properties.Settings.Default.Save();
            autoExit = checkBoxAutoExit.Checked;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            Properties.Settings.Default.width = (int)Bounds.Width;
            Properties.Settings.Default.height = (int)Bounds.Height;
            Properties.Settings.Default.X = (int)Bounds.X;
            Properties.Settings.Default.Y = (int)Bounds.Y;
            Properties.Settings.Default.Save();
        }
    }
}

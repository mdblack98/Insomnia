using System;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using Microsoft.VisualBasic.Devices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Security;
using Microsoft.Win32;

namespace Insomnia
{
    public partial class Form1 : Form
    {
        public int count = 0;
        public bool autoOff = false;
        public bool autoExit = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkBoxAutoExit.Checked = Properties.Settings.Default.autoExit;
            checkBoxAutoOff.Checked = Properties.Settings.Default.autoOff;
            checkBoxAutoExit.Enabled = checkBoxAutoOff.Checked;
        }

        private void keepAwake()
        {
            richTextBox1.Clear();
            count = 0;
            richTextBox1.AppendText("Reset all to keep awake\n");
            var key = "SYSTEM\\CurrentControlSet\\Enum\\USB";
            SearchRegistryForKey(key, true);
            richTextBox1.AppendText("Changed " + count + " items");
        }

        private void buttonKeepAwake_Click(object sender, EventArgs e)
        {
            keepAwake();
        }

        public bool List()
        {
            richTextBox1.Clear();
            count = 0;
            var key = "SYSTEM\\CurrentControlSet\\Enum\\USB";
            SearchRegistryForKey(key);
            richTextBox1.AppendText("Need to change " + count + " items"+"\n");
            //if (count == 0 && autoExit) this.Close();
            return count > 0;
        }
        private void buttonList_Click(object sender, EventArgs e)
        {
            List();
        }

        public void SearchRegistryForKey(string rootPath, bool turnOff = false)
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
                    foreach (string subKeyName in rootKey.GetSubKeyNames())
                    {
                        try
                        {
                            using (RegistryKey? subKey = rootKey.OpenSubKey(subKeyName))
                            {
                                if (subKey != null)
                                {
                                    // Recursively search in subkeys
                                    SearchRegistryForKey($"{rootPath}\\{subKeyName}", turnOff);

                                    // Example: Check for a specific value, adjust the value name as needed
                                    var value = subKey.GetValue("IdleInWorkingState");
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
                                                subKey2.SetValue("IdleInWorkingState", 0);
                                                s = "0";
                                            }
                                            catch (Exception ex)
                                            {
                                                richTextBox1.AppendText(ex.Message);
                                            }
                                            changed = "Changed";
                                            count++;
                                        }
                                        else if (value.ToString().Equals("1"))
                                        {
                                            changed = "Change needed";
                                            count++;
                                        }
                                        int i1 = subKey.ToString().IndexOf("USB");
                                        if (i1 != -1)
                                        {
                                            var s1 = subKey.ToString()[i1..];
                                            if (s1 != null)
                                            {
                                                richTextBox1.AppendText(value.ToString() + ":" + s1 + ":" + changed + "\n");
                                                Application.DoEvents();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    Application.DoEvents();
                }
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
            if (autoOff)
            {
                if (List())
                {
                    if (autoOff)
                    {
                        keepAwake();
                    }
                }
            }
            if (autoExit)
            {
                richTextBox1.AppendText("Autoff selected so will exit");
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
    }
}

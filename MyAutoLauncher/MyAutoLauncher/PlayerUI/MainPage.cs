using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Configuration;

namespace PlayerUI
{
    public partial class MainPage : Form
    {
        //To identify caps lock is on or not
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        //To identify caps lock is on or not
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        string con2 = ConfigurationManager.ConnectionStrings["APDB"].ConnectionString;

        public MainPage()
        {
            InitializeComponent();

        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainPage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Turns off caps lock if on
                TurnOffCaps();
                string shortcut = "";
                ProcessItem item = new ProcessItem();
                AutomatedDesktopApps desktopapp = new AutomatedDesktopApps();

                if (e.Shift) { shortcut += "Shift+"; }
                if (e.Control) { shortcut += "Control+"; }
                if (e.Alt) { shortcut += "Alt+"; }
                if (e.KeyCode.ToString().Length != 0) { shortcut += e.KeyCode.ToString(); }

                using (SqlConnection con = new SqlConnection(con2))
                {
                    SqlCommand cmd = new SqlCommand("GetProcessByShortcut", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@KeyboardShortcut", shortcut);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        item.ProcessID = (Convert.ToInt32(rdr["ProcessID"]));
                        item.ProcessName = (rdr["ProcessName"].ToString());
                        item.ProcessPath = (rdr["ProcessPath"].ToString());
                        item.KeyboardShortcut = (rdr["KeyboardShortcut"].ToString());
                        item.UserName = (rdr["UserName"].ToString());
                        item.PSWRD = (rdr["PSWRD"].ToString());

                    }
                    con.Close();
                }

               

                if (item.ProcessName == "LeagueOfLegends" || item.ProcessName == "Valorant")
                {
                    if (Process.GetProcessesByName("RiotClientUx").Length > 0 || Process.GetProcessesByName("LeagueClientUx").Length > 0)
                    {
                        MessageBox.Show("Riot Client Is Currently Running");
                        return;
                    }
                    var task = Task.Run(() => Process.Start(item.ProcessPath));//Start application
                    desktopapp.LaunchLolOrVal(item.UserName, item.PSWRD);//Start login process
                }
                else if (item.ProcessName == "Steam")
                {
                    if (Process.GetProcessesByName("Steam").Length > 0)  //Check if process is running
                    {
                        MessageBox.Show("Process " + item.ProcessName + " Is Currently Running");
                        return;
                    }
                    var task = Task.Run(() => Process.Start(item.ProcessPath));//Start application
                    desktopapp.LaunchSteam(item.UserName, item.PSWRD);//Start login process
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }

        }

       
        

        private void btnNewProcess_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form2 = new AutomatedProcessesForm();
            form2.Show();
        }

        private void MainPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }


        //Method to check if caps lock is turned on and turn it off for send keys not to error .
        public void TurnOffCaps()
        {
            //Check CapsLock
            bool CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
            //TURN OFF CAPSLOCK
            if (CapsLock == true)
            {
                const int KEYEVENTF_EXTENDEDKEY = 0x1;
                const int KEYEVENTF_KEYUP = 0x2;
                keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP,
                (UIntPtr)0);

            }
        }
    }
}

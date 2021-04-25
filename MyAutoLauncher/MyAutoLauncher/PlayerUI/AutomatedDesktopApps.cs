using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayerUI
{
    public class AutomatedDesktopApps
    {
        //Configured for league of legends app
        public async Task LaunchLolOrVal(string username, string password )
        {
            await Task.Delay(TimeSpan.FromSeconds(6));
            for (int i = 0; i < username.Length; i++) {
                SendKeys.Send("{" + username[i] + "}");
            }
            
            await Task.Delay(TimeSpan.FromSeconds(1));
            SendKeys.Send("{TAB}");
            await Task.Delay(TimeSpan.FromSeconds(1));

            for (int i = 0; i < password.Length; i++)
            {
                SendKeys.Send("{" + password[i] + "}");
            }

            SendKeys.Send("{ENTER}");
        }

        //Configured for steam
        public async Task LaunchSteam(string username, string password)
        {
            
            await Task.Delay(TimeSpan.FromSeconds(5));
            SendKeys.Send ("+{TAB}");
            await Task.Delay(TimeSpan.FromSeconds(1));
            SendKeys.Send("^{BACKSPACE}");
            for (int i = 0; i < username.Length; i++)
            {
                SendKeys.Send("{" + username[i] + "}");
            }

            await Task.Delay(TimeSpan.FromSeconds(1));
            SendKeys.Send("{TAB}");
            await Task.Delay(TimeSpan.FromSeconds(1));

            for (int i = 0; i < password.Length; i++)
            {
                SendKeys.Send("{" + password[i] + "}");
            }

            SendKeys.Send("{ENTER}");
        }
    }
}

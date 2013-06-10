using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace osuBeatmapsBlocker_dotNet2
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (MessageBox.Show("因使用这个软件造成的一切后果由使用者承担，与软件作者没有任何关系。您使用这个软件则视为您同意这个条件。\nUse this software as your OWN RISK!!!", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                Application.Exit();
                return;
            }
            Application.Run(new FormMain());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            handleError(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            handleError((Exception)e.ExceptionObject);
        }

        private static bool handleError(Exception exceptionObject)
        {
            OsuHelper.OsuIsSuspend = false;
            MessageBox.Show("程序运行期间出了点问题，程序将把详细的错误信息文件存放在程序目录下，您可以将这个错误信息文件发送给作者，让他为你解决错误！\n程序即将退出,为您带来的不便还请多多包涵~\n\n下面是简化的错误信息: " + exceptionObject.Message, "程序居然出错了...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            File.WriteAllText(Application.StartupPath + "\\osuBeatmapsBlocker_errcode_" + DateTime.Now.ToString("yyyy_MM_dd_HH_ff_ss_ffff") + ".txt", exceptionObject.ToString(), System.Text.Encoding.UTF8);
            Application.Exit();
            return false;
        }
    }
}

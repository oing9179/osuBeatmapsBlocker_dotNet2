using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace osuBeatmapsBlocker_dotNet2
{
    //屏蔽时暂停osu!.exe进程可有效避免osu!高频率摔刷新beatmap信息
    public class ProcessOperator
    {
        private const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        private const int PROCESS_VM_READ = 0x0010;
        private const int PROCESS_VM_WRITE = 0x0020;

        [DllImport("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern int OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);//访问权限,是否继承,PID
        [DllImport("ntdll.dll", EntryPoint = "NtSuspendProcess")]
        public static extern int NtSuspendProcess(int pAddress);//进程地址
        [DllImport("ntdll.dll", EntryPoint = "NtResumeProcess")]
        public static extern int NtResumeProcess(int pAddress);//进程地址

        public static int ProcessSuspend(int pid)
        {
            int _pAddress = OpenProcess(PROCESS_ALL_ACCESS, false, pid);
            return NtSuspendProcess(_pAddress);
        }

        public static int ProcessResume(int pid)
        {
            int _pAddress = OpenProcess(PROCESS_ALL_ACCESS, false, pid);
            return NtResumeProcess(_pAddress);
        }

    }
}

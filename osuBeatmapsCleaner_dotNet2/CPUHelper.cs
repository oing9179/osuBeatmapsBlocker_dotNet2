using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace osuBeatmapsBlocker_dotNet2
{
    public class CPUHelper
    {
        [DllImport("kernel32.dll",EntryPoint="GetSystemInfo",SetLastError=true)]
        private static extern void GetSystemInfo(ref SYSTEM_INFO lpSystemInfo);

        private struct SYSTEM_INFO
        {
            public int dwOemId;
            public int dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public IntPtr dwActiveProcessorMask;
            public int dwNumberOfProcessors;
            public int dwProcessorType;
            public int dwAllowcationGranularity;
            public int wProcessorLevel;
            public int wProcessorRevision;
        }

        public static int GetCPUCoreCount()
        {
            SYSTEM_INFO sys_info = new SYSTEM_INFO();
            GetSystemInfo(ref sys_info);
            return sys_info.dwNumberOfProcessors;
        }
    }
}

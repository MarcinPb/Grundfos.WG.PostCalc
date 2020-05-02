using System;
using System.Diagnostics;
using Grundfos.WG.PostCalc.Persistence.Model;

namespace Grundfos.WG.PostCalc
{
    public class ProcessInfoService
    {
        public static ProcessInfo GetCurrentProcessInfo()
        {
            var currentProcess = Process.GetCurrentProcess();
            int processID = currentProcess.Id;
            DateTime processTime = currentProcess.StartTime;
            return new ProcessInfo
            {
                ProcessID = processID,
                ProcessStartTime = processTime,
            };
        }
    }
}

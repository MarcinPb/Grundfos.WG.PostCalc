using System;
using NLog;

namespace Grundfos.WG2SVG.ConsoleApp
{
    public class ProgressReporter
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        private readonly double threshold;
        private double previousReported;

        public ProgressReporter(double threshold)
        {
            this.threshold = threshold;
            this.previousReported = 0;
        }

        public void HandleProgress(double ratio, string message)
        {
            if (ratio > this.previousReported + this.threshold)
            {
                this.previousReported = ratio;
                log.Info("Processed {0:P0}.", ratio);
            }
        }
    }
}

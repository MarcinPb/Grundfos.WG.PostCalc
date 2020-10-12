﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WbEasyCalc.Model
{
    public class SystemInputSheet
    {
        public const double Factor = 1.96;
        public SystemInputSheet()
        {
            this.SystemInputVolumeM3_D6_D70 = new List<double>();
            this.SystemInputVolumeError_F6_F70 = new List<double>();
        }
        public List<double> SystemInputVolumeM3_D6_D70 { get; set; }
        public List<double> SystemInputVolumeError_F6_F70 { get; set; }
        public List<double> SystemInputFactorized_K6_K70 { get => this.GetSystemInputVolumeFactorized(); }
        public double SystemInputVolume_D79 { get => this.SystemInputVolumeM3_D6_D70.Sum(); }
        public double SumFactorizedSqrt_J72 { get => Math.Sqrt(this.SystemInputFactorized_K6_K70.Sum()); }
        public double ErrorMargin_F72 { get => this.SystemInputVolume_D79 == 0 ? 0 :  this.SumFactorizedSqrt_J72 * Factor / this.SystemInputVolume_D79; }
        private List<double> GetSystemInputVolumeFactorized()
        {
            var result = new List<double>();
            for (int i = 0; i < this.SystemInputVolumeM3_D6_D70.Count; i++)
            {
                double item = Math.Pow(this.SystemInputVolumeM3_D6_D70[i] * this.SystemInputVolumeError_F6_F70[i] / Factor, 2);
                result.Add(item);
            }

            return result;
        }
    }
}
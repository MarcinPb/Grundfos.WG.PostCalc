using System;
using System.Collections.Generic;
using System.Drawing;
using Grundfos.GeometryModel;
using NLog;

/// <summary>
/// Credits to: https://stackoverflow.com/questions/17821828/calculating-heat-map-colours.
/// </summary>
public class GradientColorHeatMap
{
    private static readonly ILogger log = LogManager.GetCurrentClassLogger();

    public GradientColorHeatMap()
    {
        this.ColorsOfMap = this.BuildColorBlocks();
    }

    public List<Color> ColorsOfMap { get; private set; }

    public Color GetColorForValue(double min, double max, double value)
    {
        double valPerc = (value - min) / (max - min);// value%
        double colorPerc = 1d / (this.ColorsOfMap.Count - 1);// % of each block of color. the last is the "100% Color"
        double blockOfColor = valPerc / colorPerc;// the integer part repersents how many block to skip
        int blockIdx = (int)Math.Truncate(blockOfColor);// Idx of 
        double valPercResidual = valPerc - (blockIdx * colorPerc);//remove the part represented of block 
        double percOfColor = valPercResidual / colorPerc;// % of color of this block that will be filled

        Color cTarget = this.ColorsOfMap[blockIdx];
        Color cNext = cNext = blockIdx >= this.ColorsOfMap.Count - 1 ? cTarget : this.ColorsOfMap[blockIdx + 1];

        var deltaR = cNext.R - cTarget.R;
        var deltaG = cNext.G - cTarget.G;
        var deltaB = cNext.B - cTarget.B;

        var R = cTarget.R + (deltaR * percOfColor);
        var G = cTarget.G + (deltaG * percOfColor);
        var B = cTarget.B + (deltaB * percOfColor);

        Color c = this.ColorsOfMap[0];
        try
        {
            c = Color.FromArgb((byte)R, (byte)G, (byte)B);
        }
        catch (Exception ex)
        {
            log.Trace(ex, "Could not build color due to the following error: " + ex.Message);
        }

        return c;
    }

    private List<Color> BuildColorBlocks()
    {
        return new List<Color>
        {
            Color.FromArgb(0, 0, 0xFF) ,//Blue
            Color.FromArgb(0, 0xFF, 0) ,//Green
            Color.FromArgb(0xFF, 0xFF, 0) ,//Yellow
            Color.FromArgb(0xFF, 0, 0) ,//Red
        };
    }
}
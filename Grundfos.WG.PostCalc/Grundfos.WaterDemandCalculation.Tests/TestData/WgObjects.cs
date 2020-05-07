using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grundfos.WaterDemandCalculation.Tests.TestData
{
    public class WgObjects
    {
        public static Dictionary<int, string> ZoneDict => new Dictionary<int, string>()
        {
            {6773, "1 - Przybków"},
            {6774, "2 - Stare Miasto"},
            {6775, "3 - Kopernik"},
            {6776, "4 - Piekary"},
            {6777, "5 - Północ"},
            {6778, "6 - ZPW"},
            {6779, "7 - Tranzyt"},
            {6780, "8 - Zbiorniki"},
            {6781, "9 - Huta"},
            {6782, "10 - LSSE"},
            {6783, "11 - Legnickie Pole"},
            {6784, "12 - Krotoszyce"},
            {6785, "13 - Kunice Iwaszk."},
            {6786, "14 - Kunice Wrocł."},
            {6787, "15 - Miłkowice"},
            {6788, "16 - Pompownia"}
        };

        public static Dictionary<string, int> DemandPatternDict => new Dictionary<string, int>()
        {
            {"Urz", 6839},
            {"Mw", 6842},
            {"Mj", 6857},
            {"Ut", 6861},
            {"In", 6864},
            {"Uwp", 6933},
            {"Usz", 7016},
            {"CP1", 19092},
            {"CP2", 19093},
            {"CP3", 19094},
            {"CP4", 19095},
            {"Huta", 19096},
            {"KR", 19097},
            {"LP", 19098},
            {"Mił", 19099},
            {"P5", 19100},
            {"PC", 19101},
            {"ZPW", 19102},
            {"ZT1", 19103},
            {"Straty", 19126},
            {"nieaktywni", 19129},
            {"uwaga", 19131},
            {"C14", 19140},
            {"PW", 19154},
            {"Oczyszczalnia1", 19155},
            {"Oczyszczalnia2", 19156},
            {"20201", 19161},
            {"20816", 19162},
            {"22329", 19163},
            {"24180", 19164},
            {"24944", 19165},
            {"32131", 19166},
            {"21135", 19167},
            {"23054", 19168},
            {"24892", 19169},
            {"25433", 19170},
            {"25461", 19171},
            {"25927", 19172},
            {"25929", 19173},
            {"26552", 19174},
            {"29073", 19175},
            {"29765", 19176},
            {"30098", 19177},
            {"30943", 19178},
            {"31297", 19179},
            {"31466", 19180},
            {"32113", 19181},
            {"32904", 19182},
            {"34183", 19183},
            {"Jedynka", 147709},
            {"Fixed", -1}
        };
    }
}

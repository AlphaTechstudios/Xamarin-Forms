using System;
using System.Collections.Generic;
using System.Text;

namespace Micro_Charts.Tools
{
    public static class NumbersTools
    {
        public static string GetNember(float number)
        {
            if (number >= 1000000)
            {
                return $"{Math.Round(number / 1000000, MidpointRounding.ToEven)} M €";
            }

            if (number >= 1000)
            {
                return $"{Math.Round(number / 1000, MidpointRounding.ToEven)} K €";
            }
            return $"{Math.Round(number, MidpointRounding.ToEven)} €";
        }
    }
}

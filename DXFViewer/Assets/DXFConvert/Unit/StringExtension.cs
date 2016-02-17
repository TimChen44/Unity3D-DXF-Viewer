using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TimCommon.DXFConvert
{
   public static  class StringExtension
    {
       public static int ToInt(this string value)
       {
           int ret = 0;
           int.TryParse(value, out ret);
           return ret;
       }

       public static float ToFloat(this string value)
       {
           float ret = 0;
           float.TryParse(value, out ret);
           return ret;
       }

       public static double ToDouble(this string value)
       {
           double ret = 0;
           double.TryParse(value, out ret);
           return ret;
       }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;


namespace TimCommon.DXFConvert
{
    //当前文件印象
    public class DXFImage
    {
        protected StreamReader FStream;

    

        public DXFImage(string fileName)
        {
            if (FStream == null)
            {
                FStream = new StreamReader(fileName);
            }
        }

        protected int ReadLine=0;

        public Property Next()
        {
            if (FStream.Peek() >= 0)
            {
                var prop = new Property()
                 {
                     Code = Convert.ToInt32(FStream.ReadLine()),
                     Value = FStream.ReadLine(),
                 };
                ReadLine += 2;
                //Debug.Print(ReadLine.ToString() + "\t" + prop.Code.ToString() + "\t" + prop.Value);
                return prop;
            }
            else
            {
                return null;
            }
        }
    }

    public class Property
    {
        public int Code { get; set; }//组码
        public string Value { get; set; }//值
    }
}


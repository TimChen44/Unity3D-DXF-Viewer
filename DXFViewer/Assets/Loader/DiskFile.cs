using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.IO;

namespace Loader
{
    public class DiskFile : ILoader,IDisposable
    {
        public DiskFile(string fileName)
        {
            if (FStream == null)
            {
                FStream = new StreamReader(fileName);
            }
        }

        protected StreamReader FStream;

        protected int ReadLine = 0;

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

        public void Dispose()
        {
            if (FStream != null)
                FStream.Close();
        }

       
    }
}
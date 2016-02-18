using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Loader;

namespace DXFConvert
{
    //段基类
    public abstract class SECTION : Entity
    {
        public SECTION() { }

        public SECTION(ILoader dxfData, Property prop)
            : base(dxfData, prop)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        protected override bool ReadProperty(Property prop)
        {
            if (prop.Code == 0 && prop.Value == "ENDSEC")
            {
                return true;
            }
            else
            {
                SaveProperty(prop);
                return false;
            }
        }

    }
}

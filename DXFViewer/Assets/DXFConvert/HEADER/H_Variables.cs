using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loader;

namespace DXFConvert
{
    /// <summary>
    /// 头部变量读取
    /// </summary>
    public class H_Variables : Entity
    {
        public H_Variables() { }

        public H_Variables(ILoader dxfData, Property prop)
            : base(dxfData, prop) { }

        protected override bool  ReadProperty(Property prop)
        {
            switch (prop.Code)
            {
                case 9:
                    return true ;
                case 0:
                    return true ;
                default:
                    return base.ReadProperty(prop);
            }
        }
    }
}

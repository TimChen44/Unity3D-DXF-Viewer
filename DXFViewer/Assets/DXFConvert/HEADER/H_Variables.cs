using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TimCommon.DXFConvert
{
    /// <summary>
    /// 头部变量读取
    /// </summary>
    public class H_Variables : Entity
    {
        public H_Variables() { }

        public H_Variables(DXFImage dxfImage, Property prop)
            : base(dxfImage, prop) { }

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

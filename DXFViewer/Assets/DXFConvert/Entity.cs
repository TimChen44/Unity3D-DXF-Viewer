using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Loader;

namespace DXFConvert
{
    //所有实体
    public  class Entity
    {
        protected ILoader DXFData;

        public Entity() { }

        public Entity(ILoader dxfData, Property prop)
        {
            DXFData = dxfData;
            C = prop.Code;
            V = prop.Value.Trim();
            Sons = new List<Entity>();
            P = new List<string>();
        }



        public int C { get; set; }//组码
        public string V { get; set; }//值

        public List<Entity> Sons { get; set; }//子类对象

        public List<string> P { get; set; }
        //Dictionary<string, string> _P = new Dictionary<string, string>();

        //public Dictionary<string, string> P
        //{
        //    get { return _P; }
        //    set { _P = value; }
        //}

        /// <summary>
        /// 读取属性
        /// </summary>
        /// <returns>返回最后一个读取的属性</returns>
        public Property ReadProperties()
        {
            Property prop = DXFData.Next();
            bool isEnd = false;
            while (isEnd == false   ) 
            {
                var sonProp = ReadSonClass(prop);//尝试处理子类
                if (sonProp == null)
                {//不是子类，直接读取属性
                    isEnd = ReadProperty(prop);
                    if (isEnd == false)//如果没有退出就读取下一条记录，如果是退出，那么直接返回当前退出记录
                        prop = DXFData.Next();
                }
                else
                {//如果有子类，那么对下一个再次进行子类判断
                    prop = sonProp;//赋值，然后进行下一次判断
                    isEnd = false;
                }

            } 
            return prop;
            //var prop = DXFImage.Next();
            //var lastProp = ReadProperty(prop);
            //while (lastProp == null)
            //{
            //    prop = DXFImage.Next();
            //    lastProp = ReadProperty(prop);
            //}
            //return lastProp;
        }

        /// <summary>
        /// 读取子类的函数
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        protected virtual Property ReadSonClass(Property prop)
        {
            return null  ;
        }

        /// <summary>
        /// 创建子类并进行属性读取
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected Property CreateSonClass(Entity entity)
        {
            Sons.Add(entity);
            return entity.ReadProperties();
        }

        /// <summary>
        /// 读取属性
        /// </summary>
        /// <param name="prop"></param>
        /// <returns>T为退出代码，F为正常代码</returns>
        protected virtual bool ReadProperty(Property prop)
        {
            switch (prop.Code)
            {
                case 0:
                    return true ;
                default:
                    SaveProperty(prop);
                    return false ;
            }
        }

        //保存为定义的属性
        protected void SaveProperty(Property prop)
        {
           // P.Add(prop.Code.ToString() + "@" + prop.Value.Trim());
        }

    }
}

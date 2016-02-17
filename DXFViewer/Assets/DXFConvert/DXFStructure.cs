using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace TimCommon.DXFConvert
{
    public class DXFStructure
    {
        private DXFImage DXFImage;

        public DXFStructure() { }

        public DXFStructure(DXFImage dxfImage)
        {
            DXFImage = dxfImage;
        }

        public void Load()
        {
            Property prop = DXFImage.Next();
            while (prop != null)
            {
                switch (prop.Value)
                {
                    case "HEADER":
                        HEADER = new HEADER(DXFImage, prop);
                        prop = HEADER.ReadProperties();
                        break;
                    case "CLASSES":
                        CLASSES = new CLASSES(DXFImage, prop);
                        prop = CLASSES.ReadProperties();
                        break;
                    case "TABLES":
                        TABLES = new TABLES(DXFImage, prop);
                        prop = TABLES.ReadProperties();
                        break;
                    case "BLOCKS":
                        BLOCKS = new BLOCKS(DXFImage, prop);
                        prop = BLOCKS.ReadProperties();
                        break;
                    case "ENTITIES":
                        ENTITIES = new ENTITIES(DXFImage, prop);
                        prop = ENTITIES.ReadProperties();
                        break;
                    case "OBJECTS":
                        OBJECTS = new OBJECTS(DXFImage, prop);
                        prop = OBJECTS.ReadProperties();
                        break;
                    case "THUMBNAILIMAGE":
                        THUMBNAILIMAGE = new THUMBNAILIMAGE(DXFImage, prop);
                        prop = THUMBNAILIMAGE.ReadProperties();
                        break;
                    default:
                        prop = DXFImage.Next();
                        break;
                }
                //
            }

            //foreach (var item in ENTITIES.INSERTList)
            //{
            //    if (BLOCKS.BLOCKList.Any(x => x.C2 == item.C2 ) == false)
            //    {
            //        Debug.Print(item.C2);
            //    }
            //}

            foreach (var item in ENTITIES.Sons.GroupBy(x => x.V).Select(x => new { v = x.Key, c = x.Count() }).ToList())
            {
                if (item.v == "INSERT")
                {
                    var a = item;
                }

               

            }
        }

        /// <summary>
        /// 有损压缩，将一些很小的对象合并成大的对象提高运行效率
        /// </summary>
        public void LossCompression(LossCompressionConfig config)
        {
            int compression = 0;
            //对多段线进行压缩
            foreach (var LWPOLYLINE in ENTITIES.LWPOLYLINEList)
            {
                for (int i = 0; i < LWPOLYLINE.P2D.Count - 2; i++)
                {
                    P2D p1 = LWPOLYLINE.P2D[i];
                    P2D p2 = LWPOLYLINE.P2D[i + 1];
                    P2D p3 = LWPOLYLINE.P2D[i + 2];

                    //计算出两条线的夹角
                    var A = Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
                    var B = Math.Atan2(p3.Y - p2.Y, p3.X - p2.X);
                    var C = (B - A) / Math.PI * 180;
                    var abcC = Math.Abs(C);

                    //计算出两条线最远端的距离
                    var d2 = ((p1.X - p3.X) * (p1.X - p3.X) + (p1.Y - p3.Y) * (p1.Y - p3.Y)) / 2;

                    if (d2 <= config.Length2)
                    {
                        //计算当前长度所在的比例
                        var bl = (d2 - config.Length1) / (config.Length2 - config.Length1);
                        //计算当前角度
                        var bd = (config.Deg2 - config.Deg1) * (1 - bl) + config.Deg1;

                        if ((abcC >= 0 && abcC <= bd) || (abcC >= 180 - bd && abcC <= 180 + bd) || (abcC >= 360 - bd && abcC <= 360))
                        {
                            LWPOLYLINE.P2D.RemoveAt(i + 1);
                            compression++;

                            if (config.IsDoubleCompression == true) i--;
                        }
                    }
                }
            }

         
        }


        public HEADER HEADER { get; set; }
        public CLASSES CLASSES { get; set; }
        public TABLES TABLES { get; set; }
        public BLOCKS BLOCKS { get; set; }
        public ENTITIES ENTITIES { get; set; }
        public OBJECTS OBJECTS { get; set; }
        public THUMBNAILIMAGE THUMBNAILIMAGE { get; set; }
    }

    /// <summary>
    /// 有损压缩配置
    /// </summary>
    public class LossCompressionConfig
    {
        /// <summary>
        /// 压缩最小角度
        /// </summary>
        public double Deg1 = 0.5d;
        /// <summary>
        /// 压缩最大角度
        /// </summary>
        public double Deg2 = 20d;

        /// <summary>
        /// 压缩掉的最小长度
        /// </summary>
        public double Length1 = 0d;
        /// <summary>
        /// 压缩掉的最大长度
        /// </summary>
        public double Length2 = 800d;

        /// <summary>
        /// 双倍压缩，压缩掉的内容更多，但是图形更加失真
        /// </summary>
        public bool IsDoubleCompression = false;

    }
}

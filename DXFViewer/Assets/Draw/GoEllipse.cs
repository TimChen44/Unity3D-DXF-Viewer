using UnityEngine;
using System.Collections;
using TimCommon.DXFConvert;

[RequireComponent(typeof(LineRenderer))]
public class GoEllipse : MonoBehaviour, IResizeObject
{

    public LineRenderer lr;
    public float ZoomAdjust = 1;
    // Use this for initialization

    private float LongAxis = 0;//长轴，用来来优化椭圆的显示，太小的椭圆就不要显示了
    void Awake()
    {
        GoView.Content.ResizeObjects.Add(this);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (GoView.Content.Zoom > LongAxis)
    //    {
    //        lr.enabled = false;
    //        return;
    //    }
    //    lr.enabled = true;
    //   // lr.SetWidth(GoView.Content.Zoom * ZoomAdjust, GoView.Content.Zoom * ZoomAdjust);
    //}

    public int MaxResolution = 180;//最大线段数量
    public int MinResolution = 5;//最小线段数里
    public int OptimizingLevel = 20;//优化等级，数字越大优化级别越高，越容易失真

    public void Set(DXFStructure dxf, ELLIPSE item, float ScaleX = 1, float ScaleY = 1)
    {
        var goLayer = GoView.Content.GetLayer(item.C8);
        if (goLayer != null)
        {
            lr.material = goLayer.LayerMaterial;
            ZoomAdjust = goLayer.ZoomAdjust;
        }

        Vector3 center = new Vector3((float)item.C10, (float)item.C20, (float)item.C30);//中心点

        var theta = Mathf.Atan2((float)item.C21, (float)item.C11) * (180 / Mathf.PI);//椭圆的旋转角度
        Quaternion q = Quaternion.AngleAxis(theta, Vector3.forward);//创建一个旋转

        //长轴长度
        var a = Mathf.Abs(Vector3.Distance(new Vector3(), new Vector3((float)item.C11, (float)item.C21, (float)item.C31)));
        LongAxis = a;
        //短轴长度
        var b = a * (float)item.C40;

        //计算组层椭圆的元素数量，用于优化
        int resolution = (int)((a + b) / 2.0 / OptimizingLevel);
        if (resolution > MaxResolution) resolution = MaxResolution;
        else if (resolution < MinResolution) resolution = MinResolution;

        lr.SetVertexCount(resolution + 1);

        for (int i = 0; i < resolution; i++)
        {
            var ii = (float)(i * (item.C42 - item.C41) / resolution) + (float)item.C41;
            //计算出的坐标点
            var v = new Vector3(a * Mathf.Cos(ii), b * Mathf.Sin(ii), 0.0f);
            //加入旋转和中心点位置
            v = q * v + center;
            //增加缩放
            lr.SetPosition(i, new Vector3(v.x * ScaleX, v.y * ScaleY, v.z));
        }


        //最后增加一点用于处理最后一段的连线
        //计算出的坐标点
        var v1 = new Vector3(a * Mathf.Cos((float)item.C42), b * Mathf.Sin((float)item.C42), 0.0f);
        //加入旋转和中心点位置
        v1 = q * v1 + center;
        //增加缩放
        lr.SetPosition(resolution, new Vector3(v1.x * ScaleX, v1.y * ScaleY, v1.z));

        this.gameObject.isStatic = true;
    }

    #region IResizeObject 成员

    public void SetSetWidth()
    {
        lr.SetWidth(GoView.Content.Zoom * ZoomAdjust, GoView.Content.Zoom * ZoomAdjust);
    }

    //只会隐藏
    public void ToMin()
    {
        if (GoView.Content.Zoom > LongAxis) lr.gameObject.SetActive(false);
    }

    //只会显示
    public void ToMax()
    {
        if (GoView.Content.Zoom < LongAxis) lr.gameObject.SetActive(true);
    }

    public void HideOrShow()
    {
        if (GoView.Content.Zoom > LongAxis) lr.gameObject.SetActive(false);
        else lr.gameObject.SetActive(true);
    }

    #endregion

}

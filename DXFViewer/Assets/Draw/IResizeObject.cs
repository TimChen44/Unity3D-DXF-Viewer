using UnityEngine;
using System.Collections;

public interface IResizeObject
{  
    /// <summary>
    /// 游戏的对象
    /// </summary>
    GameObject gameObject { get; }

    /// <summary>
    /// 设置线条宽度
    /// </summary>
    void SetSetWidth();

    /// <summary>
    /// 地图缩小时，设置符合条件时影藏
    /// </summary>
    void ToMin();

    /// <summary>
    /// 地图放大时，设置符合条件时显示
    /// </summary>
    void ToMax();

    /// <summary>
    /// 更具显示的大小调整内容是否显示
    /// </summary>
    void HideOrShow();
}

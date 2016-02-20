using UnityEngine;
using System.Collections;
using DXFConvert;

//文本
[RequireComponent(typeof(TextMesh))]
public class GoText : MonoBehaviour, IResizeObject
{

    public TextMesh tm;

    private MeshRenderer mr;
    private float HideSize;//大小，用于判断到什么级别是影藏，提高执行效率
    // Use this for initialization
    void Awake()
    {
        mr = this.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    //void Update () {
    //    if (GoView.Content.Zoom > HideSize)
    //    {
    //        mr.enabled = false;
    //        return;
    //    }
    //    mr.enabled = true;
    //}

    public void Set(DXFStructure dxf, TEXT item, float ScaleX = 1, float ScaleY = 1)
    {
        tm.color = GoView.Content.GetLayerMaterial(item.C8).color;
        transform.position = new Vector3((float)item.C10, (float)item.C20, (float)item.C30);
        tm.text = item.C1;
        tm.characterSize = (float)item.C40/5;

        HideSize = tm.characterSize * tm.text.Length;

        this.gameObject.isStatic = true;
    }


    #region IResizeObject 成员


    public void SetSetWidth()
    {

    }

    //只会隐藏
    public void ToMin()
    {
        if (GoView.Content.Zoom > HideSize) this.gameObject.SetActive(false);
    }

    //只会显示
    public void ToMax()
    {
        if (GoView.Content.Zoom < HideSize) this.gameObject.SetActive(true);
    }

    public void HideOrShow()
    {
        if (GoView.Content.Zoom > HideSize) this.gameObject.SetActive(false);
        else this.gameObject.SetActive(true);
    }

    #endregion
}

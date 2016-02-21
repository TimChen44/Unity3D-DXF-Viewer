using UnityEngine;
using System.Collections;
using Loader;

public class Manager : MonoBehaviour
{
    public GoView GoView;

    // Use this for initialization
    void Start()
    {
        // this.GetComponent<Loader.ILoader>().Loaded = Loaded;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadDXF(string path)
    {
        try
        {
            DiskFile iLoader = new DiskFile(path);
            DXFConvert.DXFStructure dxfStructure = new DXFConvert.DXFStructure(iLoader);
            dxfStructure.Load();
            iLoader.Dispose();
            GoView.Set(dxfStructure);
            Debug.Log("OK:" + path);
        }
        catch (System.Exception ex)
        {
            Debug.Log("Error:" + path);
            Debug.LogError(ex.ToString());
        }

    }

    string path = "";

    public void OnGUI()
    {
        GUI.Label(new Rect(10, 25, 100, 20), "DXF文件地址");
        path = GUI.TextField(new Rect(10, 50, 300, 20), path);

        if (GUI.Button(new Rect(10, 75, 75, 20), "载入文件"))
        {
            LoadDXF(path);
        }

        if (GUI.Button(new Rect(10, 100, 75, 20), "测试文件1"))
        {
            LoadDXF(Application.dataPath + "/TestData/Test1.dxf");
        }
        if (GUI.Button(new Rect(90, 100, 75, 20), "测试文件2"))
        {
            LoadDXF(Application.dataPath + "/TestData/Test2.dxf");
        }
        if (GUI.Button(new Rect(170, 100, 75, 20), "测试文件3"))
        {
            LoadDXF(Application.dataPath + "/TestData/Test3.dxf");
        }
        if (GUI.Button(new Rect(250, 100, 75, 20), "测试文件4"))
        {
            LoadDXF(Application.dataPath + "/TestData/Test4.dxf");
        }
    }
}

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

    string path = "";

    public void OnGUI()
    {
        GUI.Label(new Rect(25, 25, 100, 20), "DXF文件地址");
        path = GUI.TextField(new Rect(25, 50, 300, 20), path);

        if (GUI.Button(new Rect(25, 75, 50, 20), "载入"))
        {
#if DEBUG

            path = Application.dataPath + "/TestData/Test1.dxf";


#endif
            try
            {
                ILoader iLoader = new DiskFile(path);
                DXFConvert.DXFStructure dxfStructure = new DXFConvert.DXFStructure(iLoader);
                dxfStructure.Load();
                GoView.Set(dxfStructure);
                Debug.Log("OK");
            }
            catch (System.Exception ex)
            {

                Debug.LogError(ex.ToString());
            }

         


        }
    }
}

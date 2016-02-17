using UnityEngine;
using System.Collections;
using TimCommon.DXFConvert;
using System.Linq;
using System.Collections.Generic;

public class GoInsert : EntitiesBase
{

    public void Set(DXFStructure dxf, INSERT insert)
    {
        gameObject.name = "Insert_" + insert.C2;

        var block = dxf.BLOCKS.BLOCKList.FirstOrDefault(x => x.C2 == insert.C2);
        if (block == null)
        {
            Debug.Log(insert.C2);
            return;
        }

        DrawLINEList(dxf, block.LINEList, (float)insert.C41, (float)insert.C42);
        DrawLWPOLYLINEList(dxf, block.LWPOLYLINEList, (float)insert.C41, (float)insert.C42);
        DrawTEXTList(dxf, block.TEXTList, (float)insert.C41, (float)insert.C42);
        DrawCIRCLEList(dxf, block.CIRCLEList, (float)insert.C41, (float)insert.C42);
        DrawARCList(dxf, block.ARCList, (float)insert.C41, (float)insert.C42);
        //DrawINSERTList(dxf, block.INSERTList, (float)insert.C41, (float)insert.C42);
        DrawELLIPSEList(dxf, block.ELLIPSEList, (float)insert.C41, (float)insert.C42);

        this.transform.position = new Vector3((float)insert.C10, (float)insert.C20, (float)insert.C30);
        this.transform.localEulerAngles = new Vector3(0, 0, (float)insert.C50);

        this.gameObject.isStatic = true;
    }
}

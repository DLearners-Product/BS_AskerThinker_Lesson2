using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Html_values
{
    public List<Html_List> myLists;

    public Html_values(string[] slideName, string[] teacherInst, bool[] videoSlides, bool[] worksheetSlides)
    {
        myLists = new List<Html_List>();
        for (int i = 0; i < slideName.Length; i++)
        {
            myLists.Add(new Html_List(i + 1, slideName[i], teacherInst[i], videoSlides[i], worksheetSlides[i]));
        };

        //Debug.Log(myLists);
    }
}

public class Html_List
{
    public int _slideNo;
    public string _slideName;
    public string _teacherInst;
    public bool _HasVideo;
    public bool _HasWorksheet;

    public Html_List(int slideNo, string slideName, string teacherInst, bool hasVideo, bool hasWorksheet)
    {
        this._slideNo = slideNo;
        this._slideName = slideName;
        this._teacherInst = teacherInst;
        this._HasVideo = hasVideo;
        this._HasWorksheet = hasWorksheet;
    }
}
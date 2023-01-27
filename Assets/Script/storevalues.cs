using System.Collections.Generic;
using UnityEngine.UI;
using System;


[Serializable]
public class storevalues
{
    //date and time
    public string date_time;
    public string parent_name;
    public string parent_mobile;
    public string child_school;
    public string child_class;
    public string child_name;
     public string child_id;

    public storevalues()
    {
        //date and time
        date_time = Main_Blended.OBJ_main_blended.STR_date_with_time;
        child_name = GetValues.OBJ_getvalues.child_name;
        parent_name = GetValues.OBJ_getvalues.parent_name;
        parent_mobile = GetValues.OBJ_getvalues.parent_mobile;
        child_class = GetValues.OBJ_getvalues.child_class;
        child_school = GetValues.OBJ_getvalues.school_name;
        child_id = GetValues.OBJ_getvalues.child_id;
    }
}
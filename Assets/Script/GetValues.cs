using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Networking;

public class GetValues : MonoBehaviour
{
    public static GetValues OBJ_getvalues;
    public string STR_url;
    public string enabler_name, child_name, parent_name, parent_mobile, child_class, school_name, child_id;

    void Start()
    {
        OBJ_getvalues = this;
        StartCoroutine(GetChildInformation());
    }

    IEnumerator GetChildInformation()
    {
        UnityWebRequest www = UnityWebRequest.Get(STR_url);

        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error");
            yield break;
        }

        JSONNode jSONNode = JSON.Parse(www.downloadHandler.text);
        Debug.Log(jSONNode);

        enabler_name =  jSONNode["enabler_name"];
        child_name =  jSONNode["child_name"];
        parent_name =  jSONNode["parent_name"];
        parent_mobile =  jSONNode["parent_mobile"];
        child_class =  jSONNode["child_class"];
        school_name =  jSONNode["school_name"];
        child_id =  jSONNode["child_id"];

      //  StopCoroutine(GetChildInformation());

    }   
}

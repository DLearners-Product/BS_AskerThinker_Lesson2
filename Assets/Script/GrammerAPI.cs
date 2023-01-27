using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;

public class GrammerAPI : MonoBehaviour
{

}
/*{
    public static GrammerAPI OBJ_grammerAPI;
    public string STR_url;
    // Start is called before the first frame update
    void Start()
    {
        OBJ_grammerAPI = this;
    }

    public void PUB_Separate()
    {
        StartCoroutine(SeparateWords());
    }
    /*  IEnumerator SeparateWords()
      {
         /* UnityWebRequest www = UnityWebRequest.Get(STR_url);

          yield return www.SendWebRequest();

          if (www.isNetworkError || www.isHttpError)
          {
              Debug.Log("Error");
              yield break;
          }
          else
          {
              Debug.Log("Test 2 : " + www.downloadHandler.text);
          }
    JSONNode jSONNode = JSON.Parse(www.downloadHandler.text);

        // Debug.Log(jSONNode);
        // Debug.Log(jSONNode[0]);

        Main_Blended.OBJ_main_blended.LS_noun.Clear();
        Main_Blended.OBJ_main_blended.LS_adjective.Clear();
        Main_Blended.OBJ_main_blended.LS_sightwords.Clear();
        Main_Blended.OBJ_main_blended.LS_verb.Clear();
        Main_Blended.OBJ_main_blended.LS_adverb.Clear();

        for (int i = 0; i< jSONNode.Count; i++)
        {
            //Debug.Log(jSONNode[i][0].ToString().Replace('"',' ').Trim());
            for(int j=0;j< jSONNode[i].Count;j++)
            {
                switch (i)
                {
                    case 0: Main_Blended.OBJ_main_blended.LS_noun.Add(jSONNode[i][j].ToString().Replace('"', ' ').Trim());
                        break;
                    case 1:
                        Main_Blended.OBJ_main_blended.LS_adjective.Add(jSONNode[i][j].ToString().Replace('"', ' ').Trim());
                        break;
                    case 2:
                        Main_Blended.OBJ_main_blended.LS_sightwords.Add(jSONNode[i][j].ToString().Replace('"', ' ').Trim());
                        break;
                    case 3:
                        Main_Blended.OBJ_main_blended.LS_verb.Add(jSONNode[i][j].ToString().Replace('"', ' ').Trim());
                        break;
                    case 4:
                        Main_Blended.OBJ_main_blended.LS_adverb.Add(jSONNode[i][j].ToString().Replace('"', ' ').Trim());
                        break;
                }
            }
            
        }

    }
}*/

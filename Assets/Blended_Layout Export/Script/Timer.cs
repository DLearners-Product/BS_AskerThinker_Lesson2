using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    Text text;
    float theTime;
    public float speed = 1;
    public Color COL_1, COL_2;

    // Use this for initialization
    void Start()
    {
        text = this.gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
            theTime += Time.deltaTime * speed;
            string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
            string seconds = (theTime % 60).ToString("00");
            text.text = minutes + ":" + seconds;
        if(theTime>=1800 && theTime<=2699)
        {
            // 30 mins
            text.color = COL_1;
            text.gameObject.transform.parent.GetChild(1).GetComponent<Image>().color = COL_1;
        }
        if(theTime >=2700 && theTime<=3299)
        {
            // 45 mins
            text.color = COL_2;
            text.gameObject.transform.parent.GetChild(1).GetComponent<Image>().color = COL_2;
        }
        
    }


}

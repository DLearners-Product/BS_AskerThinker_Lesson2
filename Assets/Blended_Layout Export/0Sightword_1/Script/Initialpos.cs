using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialpos : MonoBehaviour
{
    public static Initialpos OBJ_initialpos;
    public GameObject[] GA_objects;
    public Vector2[] V2_Pos;
    public AudioSource AS_Wrg,AS_crt;

    // Start is called before the first frame update
    public void Awake()
    {
        OBJ_initialpos = this;
        V2_Pos = new Vector2[GA_objects.Length];
        for(int i=0;i<GA_objects.Length;i++)
        {
            V2_Pos[i] = GA_objects[i].transform.position;
        }
    }

    public void Reset()
    {
        for (int i = 0; i < GA_objects.Length; i++)
        {
             GA_objects[i].transform.position = V2_Pos[i] ;
             GA_objects[i].GetComponent<Act_drag>().enabled = true;
        }
    }
    public void wrgaudio()
    {
        AS_Wrg.Play();
    }
    public void crgaudio()
    {
        AS_crt.Play();
    }
}

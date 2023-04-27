using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeYouSoon : MonoBehaviour
{
    [SerializeField] private GameObject go_BlackScreen;


    private void Start()
    {
        Invoke("EnableBlackScreen", 6f);
    }


    public void EnableBlackScreen()
    {
        go_BlackScreen.SetActive(true);
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class spotthedifference : MonoBehaviour
{
    public int I_ansCount;
    public GameObject G_activityComplete;
    public Text TEX_ansCount;

    void Start()
    {
        I_ansCount = 0;
        TEX_ansCount.text = I_ansCount + "/10";
        G_activityComplete.SetActive(false);
    }
    public void BUT_ansClick()
    {
        I_ansCount++;
        TEX_ansCount.text = I_ansCount + "/10";
        EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<Button>().enabled = false;
        EventSystem.current.currentSelectedGameObject.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        if(I_ansCount==10)
        {
            Invoke("THI_Final", 2f);
        }
    }
    public void THI_Final()
    {
        G_activityComplete.SetActive(true);
    }
}

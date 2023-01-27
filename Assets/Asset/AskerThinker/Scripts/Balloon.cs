
using UnityEngine;
using TMPro;


public class Balloon : MonoBehaviour
{
    //anim
    [SerializeField] private Animator balloonAnim;

    [SerializeField] private TMP_InputField answerInputField;

    public void KeepFloating()
    {
        balloonAnim.SetTrigger("KeepFloating");
        answerInputField.gameObject.SetActive(true);
    }
}

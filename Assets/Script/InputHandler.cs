using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public static InputHandler instance;
    GameObject currentSlecetedGameObject;
    InputField selectedInput;
    TMP_InputField tmpSelectedInput;

    private void Start()
    {
        if(instance == null){
            instance = this;
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            currentSlecetedGameObject = EventSystem.current.currentSelectedGameObject;

            if(currentSlecetedGameObject?.GetComponent<InputField>()){
                selectedInput = currentSlecetedGameObject.GetComponent<InputField>();
            }else if(currentSlecetedGameObject?.GetComponent<TMP_InputField>()){
                tmpSelectedInput = currentSlecetedGameObject.GetComponent<TMP_InputField>();
            }

        }
    }

    public void InputField(string givenString){
        if(selectedInput != null){
            selectedInput.text = givenString;
        }else if(tmpSelectedInput != null){
            Debug.Log($"{tmpSelectedInput.text}");
            tmpSelectedInput.text = givenString;
        }
    }

    public void SetInputField(InputField inputField=null, TMP_InputField tMP_InputField=null){
        if(inputField){
            selectedInput = inputField;
        }else if(tMP_InputField){
            tmpSelectedInput = tMP_InputField;
        }
    }
}

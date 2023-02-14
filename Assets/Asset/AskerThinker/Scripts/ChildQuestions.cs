using TMPro;
using UnityEngine;

public class ChildQuestions : MonoBehaviour
{
    //ref
    [SerializeField] private ChildQuestionHandler cqhRef;

    //audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClick;

    //GO
    [SerializeField] private GameObject instructionOverlay;
    [SerializeField] private Transform qPanel;

    //prefab
    [SerializeField] private GameObject qPrefab;

    public static GameObject OBJ_ChildQuestions;
    private GameObject instantiatedQuestion;

    //Awake
    private void Awake()
    {

    }

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < cqhRef.q.Count; i++)
        {
            instantiatedQuestion = Instantiate(qPrefab, qPanel);
            instantiatedQuestion.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = cqhRef.q[i];
            instantiatedQuestion.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }


    public void OnClickSkipButton()
    {
        audioSource.clip = buttonClick;
        audioSource.Play();

        instructionOverlay.SetActive(false);
    }
}

using UnityEngine;
using TMPro;

public class QuestionBalloon : MonoBehaviour
{
    //ref
    [SerializeField] private ChildQuestionHandler cqhRef;

    //anim
    [SerializeField] private Animator balloonAnim;


    private void Start()
    {
        cqhRef = GetComponentInParent<ChildQuestionHandler>();
        this.GetComponentInChildren<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    public void KeepFloating()
    {
        balloonAnim.SetTrigger("KeepFloating");
    }

    public void BalloonPop()
    {
        cqhRef.OnClickBalloon();
    }
}

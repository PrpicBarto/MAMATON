using QTEPack;
using System.Collections;
using UnityEngine;

public class playerStop : MonoBehaviour
{
    public QuickTimeEvent QTE;
    public QuickTimeEvent QTE2;
    [SerializeField] RunCamera runCamera;
    [SerializeField] int enemyDifficulty;
    PlayerMove playerMove;
    bool isDead;

    private void Start()
    {
        QTE.Hide();
        if (QTE2 != null) QTE2.Hide();
        PlayerMove.instance.animator.SetBool("IsMoving", true);
    }

    public void Show()
    {
        runCamera.CombatCamera();
        QTE.OnSuccess.AddListener(OnFirstQTEWin);
        QTE.OnFail.AddListener(OnFirstQTELose);
        PlayerMove.instance.animator.SetBool("IsMoving", false);

        QTE.ShowQTE(new Vector2(Random.Range(0, 200f), Random.Range(0, 200f)), 1, enemyDifficulty);
    }

    private void ShowSecondQTE()
    {
        if (QTE2 == null) return;
        runCamera.SwitchCameras();
        PlayerMove.instance.animator.SetBool("IsAttacking", true);
        QTE2.OnSuccess.AddListener(OnSecondQTEWin);
        QTE2.OnFail.AddListener(OnSecondQTELose);


        QTE2.ShowQTE(new Vector2(Random.Range(0, 200f), Random.Range(0, 200f)), 1, enemyDifficulty);
    }

    public void Hide()
    {
        QTE.Hide();
        if (QTE2 != null) QTE2.Hide();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger!");
            playerMove = other.GetComponent<PlayerMove>();
            RunCamera runCamera = other.GetComponent<RunCamera>();
            if (playerMove != null)
            {
                Debug.Log("PlayerMove script found and disabled.");
                playerMove.canMove = false;
                Show();
            }
        }
    }

    
    private void OnFirstQTEWin()
    {
        
        QTE.OnSuccess.RemoveListener(OnFirstQTEWin);
        QTE.OnFail.RemoveListener(OnFirstQTELose);
        QTE.Hide();
        Time.timeScale = 1f;
        PlayerMove.instance.animator.SetBool("IsAttacking", true);
        StartCoroutine(ShowSecondQTEWithDelay(2.25f));
    }


    private void OnFirstQTELose()
    {
        QTE.OnSuccess.RemoveListener(OnFirstQTEWin);
        QTE.OnFail.RemoveListener(OnFirstQTELose);
        QTE.Hide();
        Debug.Log("Player failed the first QTE.");
        Time.timeScale = 1f;
        PlayerMove.instance.animator.SetBool("IsDead", true);
    }

    private IEnumerator ShowSecondQTEWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        ShowSecondQTE();
    }
    private void OnSecondQTEWin()
    {
        QTE2.OnSuccess.RemoveListener(OnSecondQTEWin);
        QTE2.OnFail.RemoveListener(OnSecondQTELose);
        QTE2.Hide();
        PlayerMove.instance.animator.SetBool("IsMoving", true);
        PlayerMove.instance.animator.SetBool("IsAttacking", false);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }

    private void OnSecondQTELose()
    {
        QTE2.OnSuccess.RemoveListener(OnSecondQTEWin);
        QTE2.OnFail.RemoveListener(OnSecondQTELose);
        QTE2.Hide();
        Time.timeScale = 1f;
        PlayerMove.instance.animator.SetBool("IsDead", true);
        Debug.Log("Player failed the second QTE.");
    }
}

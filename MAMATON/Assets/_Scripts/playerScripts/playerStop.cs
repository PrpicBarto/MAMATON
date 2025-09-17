using QTEPack;
using System.Collections;
using UnityEngine;

public class playerStop : MonoBehaviour
{
    public QuickTimeEvent QTE;
    public QuickTimeEvent QTE2; // Second QTE
    [SerializeField] RunCamera runCamera;
    [SerializeField] int enemyDifficulty;
    PlayerMove playerMove;

    private void Start()
    {
        QTE.Hide();
        if (QTE2 != null) QTE2.Hide();
        PlayerMove.instance.animator.SetBool("IsMoving", true);
    }

    public void Show()
    {
        QTE.OnSuccess.AddListener(OnFirstQTEWin);
        QTE.OnFail.AddListener(OnFirstQTELose);
        runCamera.CombatCamera();

        // Start the animation when the first QTE is shown
        PlayerMove.instance.animator.SetBool("IsMoving", false);
        PlayerMove.instance.animator.SetBool("IsAttacking", true);

        QTE.ShowQTE(new Vector2(Random.Range(0, 200f), Random.Range(0, 200f)), 1, enemyDifficulty);
    }

    private void ShowSecondQTE()
    {
        if (QTE2 == null) return;
        QTE2.OnSuccess.AddListener(OnSecondQTEWin);
        QTE2.OnFail.AddListener(OnSecondQTELose);

        // Start the animation when the second QTE is shown
        PlayerMove.instance.animator.SetBool("IsMoving", true);
        PlayerMove.instance.animator.SetBool("IsAttacking", false);

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

    // First QTE callbacks
    private void OnFirstQTEWin()
    {
        QTE.OnSuccess.RemoveListener(OnFirstQTEWin);
        QTE.OnFail.RemoveListener(OnFirstQTELose);
        QTE.Hide();
        Time.timeScale = 1f;
        StartCoroutine(ShowSecondQTEWithDelay(2.5f));
    }

    private IEnumerator ShowSecondQTEWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        ShowSecondQTE();
    }

    private void OnFirstQTELose()
    {
        QTE.OnSuccess.RemoveListener(OnFirstQTEWin);
        QTE.OnFail.RemoveListener(OnFirstQTELose);
        QTE.Hide();
        Debug.Log("Player failed the first QTE.");
        PlayerMove.instance.animator.SetBool("IsMoving", true);
        Time.timeScale = 1f;
    }

    // Second QTE callbacks
    private void OnSecondQTEWin()
    {
        QTE2.OnSuccess.RemoveListener(OnSecondQTEWin);
        QTE2.OnFail.RemoveListener(OnSecondQTELose);
        QTE2.Hide();
        Time.timeScale = 1f;
        runCamera.SwitchCameras();
        Destroy(gameObject);
    }

    private void OnSecondQTELose()
    {
        QTE2.OnSuccess.RemoveListener(OnSecondQTEWin);
        QTE2.OnFail.RemoveListener(OnSecondQTELose);
        QTE2.Hide();
        Time.timeScale = 1f;
        PlayerMove.instance.animator.SetBool("IsMoving", true);
        Debug.Log("Player failed the second QTE.");
    }
}

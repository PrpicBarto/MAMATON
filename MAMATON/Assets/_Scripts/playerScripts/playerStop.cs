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
    }

    public void Show()
    {
        QTE.OnSuccess.AddListener(OnFirstQTEWin);
        QTE.OnFail.AddListener(OnFirstQTELose);
        
        runCamera.CombatCamera();
        QTE.ShowQTE(new Vector2(Random.Range(0, 200f), Random.Range(0, 200f)), 1, enemyDifficulty);
    }

    private void ShowSecondQTE()
    {
        if (QTE2 == null) return;
        QTE2.OnSuccess.AddListener(OnSecondQTEWin);
        QTE2.OnFail.AddListener(OnSecondQTELose);
        runCamera.CombatCamera();
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
        StartCoroutine(ShowSecondQTEWithDelay(0.5f)); // 0.5 seconds delay
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
        // Optionally handle fail logic here
        Debug.Log("Player failed the first QTE.");
        Time.timeScale = 1f;
    }

    // Second QTE callbacks
    private void OnSecondQTEWin()
    {
        QTE2.OnSuccess.RemoveListener(OnSecondQTEWin);
        QTE2.OnFail.RemoveListener(OnSecondQTELose);
        QTE2.Hide();
        Time.timeScale = 1f;
        Destroy(gameObject);
        runCamera.SwitchCameras();
    }

    private void OnSecondQTELose()
    {
        QTE2.OnSuccess.RemoveListener(OnSecondQTEWin);
        QTE2.OnFail.RemoveListener(OnSecondQTELose);
        QTE2.Hide();
        Time.timeScale = 1f;
        Debug.Log("Player failed the second QTE.");
        // Optionally handle fail logic here
    }
}

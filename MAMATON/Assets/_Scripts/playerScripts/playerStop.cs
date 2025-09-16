using QTEPack;
using System.Collections;
using UnityEngine;

public class playerStop : MonoBehaviour
{

    public QuickTimeEvent QTE;
    [SerializeField] RunCamera runCamera;
    PlayerMove playerMove;

    private void Start()
    {
        QTE.Hide();
    }
    public void Show()
    {

        QTE.ShowQTE(new Vector2(200f, 200f), 1, 0);

    }

    public void Hide()
    {

        QTE.Hide();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger!");
            // You can stop the player or do other actions here
            // Example: stop movement script
            playerMove = other.GetComponent<PlayerMove>();
            RunCamera runCamera = other.GetComponent<RunCamera>();
            QTE.OnSuccess.AddListener(OnWin);
            QTE.OnFail.AddListener(OnLose);
            if (playerMove != null)
            {
                Debug.Log("PlayerMove script found and disabled.");
                playerMove.canMove = false; // disable movement
                Show();
            }
        }
    }

    public void OnLose()
    {
        Destroy(gameObject);
        QTE.OnSuccess.RemoveListener(OnWin);
        QTE.OnFail.RemoveListener(OnLose);
    }

    public void OnWin()
    {
        Hide();
        Destroy(gameObject);
        runCamera.SwitchCameras();
        QTE.OnSuccess.RemoveListener(OnWin);
        QTE.OnFail.RemoveListener(OnLose);
    }
}

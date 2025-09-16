using UnityEngine;

public class RunQTETest : MonoBehaviour
{
    [SerializeField] RunCamera runCamera;
    public void OnLose()
    {
        Debug.Log("Ye suck");
    }

    public void OnWin()
    {
        runCamera.SwitchCameras();
    }
}

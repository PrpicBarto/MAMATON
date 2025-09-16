using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class RunCamera : MonoBehaviour
{
    [SerializeField] CinemachineCamera[] cameras;
    [SerializeField] GameObject player;
    [SerializeField] int index;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            SwitchCameras();
        }
    }

    void SwitchCameras()
    {
        StartCoroutine(CameraSwitch());
    }

    IEnumerator CameraSwitch()
    {
        yield return new WaitForSeconds(1f);
        cameras[0].Priority = index;
        index++;
        yield return new WaitForSeconds(1f);
        cameras[1].Priority = index;
        index++;
        yield return new WaitForSeconds(1f);
        cameras[2].Priority = index;
        index++;
        yield return new WaitForSeconds(1f);
        cameras[0].Priority = index;
        yield return null;
    }
}

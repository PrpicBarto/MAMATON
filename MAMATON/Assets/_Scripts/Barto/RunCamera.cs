using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class RunCamera : MonoBehaviour
{
    [SerializeField] CinemachineCamera[] cameras;
    [SerializeField] GameObject player;
    [SerializeField] int index;
    [SerializeField] float sequenceTime;
    [SerializeField] QTETest quickTimeEvent;

    public void SwitchCameras()
    {
        StartCoroutine(CameraSwitch());
    }

    IEnumerator CameraSwitch()
    {
        quickTimeEvent.Hide();
        yield return new WaitForSeconds(sequenceTime);
        cameras[0].Priority = index;
        index++;
        yield return new WaitForSeconds(sequenceTime);
        cameras[1].Priority = index;
        index++;
        yield return new WaitForSeconds(sequenceTime);
        cameras[0].Priority = index;
        yield return null;
    }
}

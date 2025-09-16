using QTEPack;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class RunCamera : MonoBehaviour
{
    [SerializeField] CinemachineCamera[] cameras;
    [SerializeField] int index;
    [SerializeField] float sequenceTime;

    public void SwitchCameras()
    {
        StartCoroutine(CameraSwitch());
    }

    IEnumerator CameraSwitch()
    {
        yield return new WaitForSeconds(sequenceTime);
        cameras[0].Priority = index;
        index++;
        yield return new WaitForSeconds(sequenceTime);
        cameras[1].Priority = index;
        index++;
        yield return new WaitForSeconds(sequenceTime);
        cameras[0].Priority = index;
        yield return null;
        PlayerMove.instance.canMove = true;
    }

    
}

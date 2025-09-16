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
    [SerializeField] float animationTime;

    public void SwitchCameras()
    {
        StartCoroutine(CameraSwitch());
    }

    public void CombatCamera()
    {
        StartCoroutine(CombatSequence());
    }

    IEnumerator CameraSwitch()
    {
        yield return new WaitForSeconds(sequenceTime);
        cameras[0].Priority = index;
        index++;
        yield return new WaitForSeconds(sequenceTime);
        cameras[Random.Range(2, cameras.Length)].Priority = index;
        index++;
        yield return new WaitForSeconds(sequenceTime);
        PlayerMove.instance.animator.SetBool("IsMoving", true);
        cameras[0].Priority = index;
        yield return null;
        PlayerMove.instance.canMove = true;
    }
    IEnumerator CombatSequence()
    {
        Time.timeScale = 0.25f;
        PlayerMove.instance.animator.SetBool("IsMoving", false);
        cameras[1].Priority = index;
        index++;
        yield return new WaitForSeconds(animationTime);
        cameras[0].Priority = index;
        index++;
        yield return null;
    }


}

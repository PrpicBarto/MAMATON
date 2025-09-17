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
        Debug.Log("Combat camera activated");
    }

    IEnumerator CameraSwitch()
    {
        yield return new WaitForSeconds(sequenceTime);
        cameras[Random.Range(2, cameras.Length)].Priority = index;
        index++;
        yield return new WaitForSeconds(sequenceTime);
        PlayerMove.instance.animator.SetBool("IsMoving", true);
        PlayerMove.instance.animator.SetBool("IsAttacking", false);
        cameras[0].Priority = index;
        PlayerMove.instance.canMove = true;
        yield return null;
    }
    IEnumerator CombatSequence()
    {
        PlayerMove.instance.animator.SetBool("IsAttacking", true);
        Time.timeScale = 0.25f;
        cameras[Random.Range(1, cameras.Length)].Priority = index;
        index++;
        yield return new WaitForSeconds(animationTime);
        yield return null;
    }


}

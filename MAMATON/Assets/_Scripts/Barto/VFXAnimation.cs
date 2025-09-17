using UnityEngine;

public class VFXAnimation : MonoBehaviour
{
    [SerializeField] GameObject vfxPrefab;
    [SerializeField] GameObject vfxPrefabTwo;
    public void TriggerAttackVFX()
    {
        vfxPrefab.SetActive(true);
        var ps = vfxPrefab.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play(true);
        }
    }
    public void TriggerAttackVFXSecond()
    {
        vfxPrefabTwo.SetActive(true);
        var ps = vfxPrefabTwo.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play(true);
        }
    }
}

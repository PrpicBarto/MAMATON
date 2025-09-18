using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        audioSource.Play();
    }
}

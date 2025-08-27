using UnityEngine;

public class audioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public void playAudio()
    {
        audioSource.Play();
    }
}

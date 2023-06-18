using UnityEngine;

public class tocaMusica : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();  
        audioSource.Play();
    }
}
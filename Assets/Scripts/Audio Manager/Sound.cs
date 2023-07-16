using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)] // Sliders no Inspector do Unity.
    public float volume;
    [Range(.7f, 2.5f)]
    public float pitch;
    public bool loop;

    // Para não aparecer no Inspector, pois esses AudioSources devem ser adicionados 
    // no Awake() do AudioManager.cs
    [HideInInspector]
    public AudioSource source;
}

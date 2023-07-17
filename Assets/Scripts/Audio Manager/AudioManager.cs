using UnityEngine.Audio;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    //public Sound[] sounds;

    public static AudioManager instance{
        get;
        private set;
    }

    private void Awake(){
        if(instance != null){
            Debug.LogError("Mais de um AudioManager encontrado na Scene.");
        }
        instance = this;
    }


    public void PlayOneShot(EventReference sound, Vector3 worldPos){
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
}

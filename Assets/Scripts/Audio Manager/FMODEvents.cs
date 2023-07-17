using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience SFX")]
    [field: SerializeField] public EventReference waterFlowing{get; private set;}
    [field: Header("Game SFX")]
    [field: SerializeField] public EventReference weaponCollected{get; private set;}

    private const string FootstepEventPath = "event:/Footsteps";
    private EventInstance footstepEventInstance;
    private EventDescription footstepEventDescription;
    private int currentTerrainType;
    

    public static FMODEvents instance{
        get;
        private set;
    }

    private void Awake(){
        if(instance != null){
            Debug.LogError("Encontrou mais de uma instância de FMODEvents na Scene.");
        }
        instance = this;

        footstepEventDescription = RuntimeManager.GetEventDescription(FootstepEventPath);
        footstepEventDescription.createInstance(out footstepEventInstance);
    }
    private void OnDestroy(){
        footstepEventInstance.release();
    }

    // Método para atualizar o parâmetro 'Terreno' do evento de passos.
    public void SetFootstepTerrain(int terrainType){
        if(currentTerrainType != terrainType){
            currentTerrainType = terrainType;
            footstepEventInstance.setParameterByName("Terreno", terrainType);
        }
    }
    public void PlayFootstepSound(){
        footstepEventInstance.start();
    }
}

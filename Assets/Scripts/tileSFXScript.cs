using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class tileSFXScript : MonoBehaviour
{
    [SerializeField][Range(0f,2f)]
    private int terrainType;

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            playerController.Instance.UpdateTerrainType(terrainType);
        }
    }
}

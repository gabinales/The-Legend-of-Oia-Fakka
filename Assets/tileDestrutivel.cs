using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class tileDestrutivel : MonoBehaviour
{
    // Evento que será acionado quando um tile for destruído:
    public static event Action<GameObject> TileDestroyed;


    // Destrói o tile (grama, cruz, vaso, etc.) e dropa o item:
    public void DestroyTile(){ // Função chamada pela animação de destruição do tile
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        Destroy(gameObject);

        TileDestroyed?.Invoke(gameObject); // Sinaliza para todas as classes interessadas.
    }
}

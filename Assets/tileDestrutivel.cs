using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileDestrutivel : MonoBehaviour
{
    // Destrói o tile (grama, cruz, vaso, etc.) e dropa o item:
    public void DestroyTile(){ // Função chamada pela animação de destruição do tile
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        Destroy(gameObject);
    }
}

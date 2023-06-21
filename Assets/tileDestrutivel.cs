using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileDestrutivel : MonoBehaviour
{
    public void Kill(){
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileDestrutivel : MonoBehaviour
{
    private Animator animator;
    //public GameObject prefabGrama;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //prefabGrama = GetComponent
    }

    // Update is called once per frame
  
    public void cortaGrama(){
        animator.SetBool("cortada", true);
    }

    public void apagaSprite(){
        Destroy(gameObject);
    }
}

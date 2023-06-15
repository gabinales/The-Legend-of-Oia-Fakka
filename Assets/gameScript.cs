using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameScript : MonoBehaviour
{
    public static void damage(string arma, GameObject danificado)
    {
        if (arma == "sabreDeLuz")
        {
            Debug.Log(arma+ " danificou " + danificado);
            danificado.GetComponent<Rigidbody2D>().gravityScale = 1;
            //Destroy(danificado);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
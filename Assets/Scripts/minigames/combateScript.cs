using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combateScript : MonoBehaviour
{
    //private Animator animator = new Animator();

    public static void damage(string arma, GameObject danificado)
    {
        if (arma == "sabreDeLuz")
        {
            //Debug.Log(arma + " danificou " + danificado);
            Destroy(danificado);
        }
    }

    public static void kill(string arma, GameObject danificado)
    {
        if (arma == "sabreDeLuz")
        {
            Debug.Log(arma + " aniquilou " + danificado);
            Destroy(danificado);
        }
    }

    public static void parry(GameObject parrier, GameObject parried)
    {
        Debug.Log(parrier.name + " PAROU O ATAQUE DE " + parried.name);

        parried.GetComponent<Animator>().SetTrigger("Parried");
    }
}
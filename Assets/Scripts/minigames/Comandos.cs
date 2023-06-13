using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Comandos : MonoBehaviour
{
    void Update()
    {
     if (Input.GetKeyDown(KeyCode.F))
     {
        SceneManager.LoadScene("Mjonir Field 01");
     }
    }
}
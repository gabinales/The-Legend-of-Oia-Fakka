using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Comandos : MonoBehaviour
{
    public gridManager gridManagerInstance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) SceneManager.LoadScene("Mjonir Field 01");

        if (Input.GetKeyDown(KeyCode.Space)) gridManagerInstance.Spawn(2);

        if (Input.GetKeyDown(KeyCode.Z)) gridManagerInstance.Spawn(1);
    }
}
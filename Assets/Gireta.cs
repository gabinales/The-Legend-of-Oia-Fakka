using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gireta : MonoBehaviour
{
    public float velocidadeGiroX = 30f; // Velocidade de giro em graus por segundo no eixo X

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime));
    }
}

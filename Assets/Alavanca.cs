using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alavanca : MonoBehaviour, ISwitch
{
    public void Ativa(){
        Debug.Log("Ativou a alavanca!");
    }
    public void Desativa(){
        Debug.Log("Desativou a alavanca!");
    }
}

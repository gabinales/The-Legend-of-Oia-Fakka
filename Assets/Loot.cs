using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Loot : ScriptableObject, IColetavel
{
    public Sprite spriteLoot;
    public string nomeLoot;
    public int chanceDrop;

    public Loot(string nomeLoot, int chanceDrop){
        this.nomeLoot = nomeLoot;
        this.chanceDrop = chanceDrop;
    }

    // Patrick 22.06 --- Função Collect() do Loot
    public void Collect(){
        Debug.Log("Collect");
    }
}

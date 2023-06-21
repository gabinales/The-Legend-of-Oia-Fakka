using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Loot : ScriptableObject
{
    public Sprite spriteLoot;
    public string nomeLoot;
    public int chanceDrop;

    public Loot(string nomeLoot, int chanceDrop){
        this.nomeLoot = nomeLoot;
        this.chanceDrop = chanceDrop;
    }
}

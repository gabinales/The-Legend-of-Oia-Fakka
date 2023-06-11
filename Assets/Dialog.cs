using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialog{
    [SerializeField] List<string> falas; // O diálogo será uma lista de sentenças

    public List<string> Falas{ // public para que possa ser reutilizado por outros métodos futuramente.
        get{
            return falas;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAtacavel
{
    void Dano(int quantidade);

    void Morre();

    int HpMax {get; set; }
    int HpAtual {get; set; }
}

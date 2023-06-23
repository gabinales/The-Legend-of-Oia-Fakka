using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAtacavel // Implementa os métodos de dano e cura, tanto no Player quanto nos inimigos q tiles destrutíveis
{
    void Dano(int quantidade);
    void Cura(int quantidade);
}

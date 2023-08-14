using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInimigoMovivel
{
    Rigidbody2D rb { get; set; }
    bool IsFacingRight { get; set; }

    void MoveEnemy(Vector2 velocidade);
    void CheckForLeftOrRightFacing(Vector2 velocidade);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    public void Damage(GameObject target)
    {
        Debug.Log("target: " + target);
        if (target.name == "npcEnemy")
        {
            Transform targetTransform = target.transform;
            // Increase the Y scale by 1
            Vector3 currentScale = targetTransform.localScale;
            currentScale.y += 1.0f;
            targetTransform.localScale = currentScale;
        }
        if (target.CompareTag("enemy"))
        Debug.Log("inimigo");
        {
            target.GetComponent<Cohen>().HP -= 1;
            
        }

    }
}
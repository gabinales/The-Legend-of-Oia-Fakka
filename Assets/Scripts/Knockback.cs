using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float investida;

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("enemy")){
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if(rb != null){
                StartCoroutine(KnockbackCoroutine(rb));
            }
        }
    }

    private IEnumerator KnockbackCoroutine(Rigidbody2D rb){
        Vector2 forceDirection = rb.transform.position - transform.position;
        Vector2 force = forceDirection.normalized * investida;

        rb.velocity = force;
        yield return new WaitForSeconds(.3f);

        rb.velocity = new Vector2();
    }
}

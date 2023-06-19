using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageIndicator : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float lifetime = 1f;
    private float VelYInicial = 5f;
    private float VelXInicialIntervalo = 3f;

    private Rigidbody2D canvasRigidBody;
    private Vector3 iniPos;
    private Vector3 targetPos;
    private float timer;

    private int valor;

    // Start is called before the first frame update
    void Start()
    {
        //transform.LookAt(2 * transform.position - Camera.main.transform.position);
        canvasRigidBody = gameObject.GetComponent<Rigidbody2D>();
        canvasRigidBody.velocity = new Vector2(Random.Range(-VelXInicialIntervalo, VelXInicialIntervalo), VelYInicial);
        canvasRigidBody.gravityScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //float fraction = lifetime / 2f;

        if (timer > lifetime) Destroy(gameObject);
        //else if (timer > fraction) textMeshPro.color = Color.Lerp(textMeshPro.color, Color.clear, (timer - fraction) / (lifetime - fraction));
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target; // Objeto que se usar� como referencia para la direcci�n
    public float speed = 5f; // Velocidad a la que se mover� el objeto
    Vector3 direction;
    float distance;
    private void Start()
    {
        target = GameObject.Find("centerPoint").transform;
        direction = target.up;
    }

    void Update()
    {
        // Comprueba que se ha asignado un objeto referencia
        
            // Calcula la direcci�n hacia adelante del objeto referencia
            

            // Calcula la cantidad de movimiento necesaria para moverse hacia adelante
            distance = speed * Time.unscaledDeltaTime;

            // Mueve el objeto en la direcci�n hacia adelante
            transform.Translate(direction.normalized * distance, Space.World);
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropLogic : MonoBehaviour
{
    public Transform player; // Objeto jugador al que se moverá
    public float minDistance = 5f; // Distancia mínima a la que se iniciará el movimiento
    public float startSpeed = 1f; // Velocidad inicial a la que se moverá
    public float maxSpeed = 5f; // Velocidad máxima a la que se moverá
    private float currentSpeed; // Velocidad actual de movimiento
    public int expGiven = 10;
    float distanceToPlayer;
    

    void Awake()
    {
        if (SceneManager.GetActiveScene().name.Equals("GamePlay")) {
            // Inicializa la velocidad actual con la velocidad inicial
            player = GameObject.Find("Player").transform;
            currentSpeed = startSpeed;
        }

       
    }

    private void Start()
    {
        if (this.gameObject.tag.Equals("Drop"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = UnityEngine.Random.ColorHSV(); 
        }
        if (!SceneManager.GetActiveScene().name.Equals("GamePlay"))
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        
        // Calcula la distancia entre el objeto y el jugador
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Comprueba si el jugador está a una distancia adecuada
        if (distanceToPlayer <= minDistance)
        {
            // Calcula la dirección hacia el jugador
            Vector3 direction = player.position - transform.position;

            // Incrementa la velocidad actual según la distancia al jugador
            currentSpeed = Mathf.Lerp(startSpeed, maxSpeed, (minDistance - distanceToPlayer) / (minDistance));

            // Mueve el objeto en la dirección del jugador con la velocidad actual
            transform.Translate(direction.normalized * currentSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player") {
            if(this.gameObject.tag == "Drop")
            {
                collision.gameObject.GetComponent<StatsLogic>().CalculateExpIncrease(expGiven);
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().currentCoins++;
            }
            Destroy(this.gameObject);
        
        }
    }
}

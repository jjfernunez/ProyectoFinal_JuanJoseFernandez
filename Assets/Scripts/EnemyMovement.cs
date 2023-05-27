using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    public Transform target; // Objeto hacia el que se moverá el objeto
    public float speed = 5f; // Velocidad a la que se moverá el objeto
    public GameObject[] drop;
    Vector3 direction;
    float distance;
    GameObject manager;
    private float lastPosition;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager");
        drop = new GameObject[2];
        target = GameObject.Find("Player").transform;
        drop.SetValue(Resources.Load<GameObject>("Prefabs/Drop"), 0);
        drop.SetValue(Resources.Load<GameObject>("Prefabs/Coin"), 1);
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameIsPaused)
        {
            direction = target.position - transform.position;

            // Calcula la cantidad de movimiento necesaria para llegar al destino
            distance = speed * Time.unscaledDeltaTime;

            // Si la distancia a recorrer es menor que la distancia hacia el objetivo, se llega al objetivo
            if (direction.magnitude <= distance)
            {
                transform.position = target.position;
            }
            else
            {
                // Mueve el objeto en la dirección del objetivo
                transform.Translate(direction.normalized * distance, Space.World);
            }
            SpriteFlip();
        }

    }

    private void SpriteFlip()
    {
        float currentPosition = transform.position.x;

        if (currentPosition != lastPosition)
        {
            if (currentPosition < lastPosition)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;
        }

        lastPosition = currentPosition;
    }

    private void OnDestroy()
    {
        if (SceneManager.GetActiveScene().name.Equals("GamePlay"))
        {
            manager.GetComponent<GameManager>().currentKills++;

            Instantiate(drop[0], this.transform.position + GenerateRandomPos(), drop[0].transform.rotation);

            int coinChance = Random.Range(1, 100);

            if (coinChance >= 1 && coinChance <= 15)
            {

                Instantiate(drop[1], this.transform.position + GenerateRandomPos(), drop[1].transform.rotation);
            }
        }
    }

    private Vector3 GenerateRandomPos()
    {
        Vector3 randomPos = new Vector3(Random.Range(0, 1), Random.Range(0, 1));

        return randomPos;
    }

}


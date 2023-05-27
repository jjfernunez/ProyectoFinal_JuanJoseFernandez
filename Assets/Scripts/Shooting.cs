using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectile; // Objeto que se instanciar� al hacer clic
    public float fireRate = 0.5f; // Tiempo de recarga entre disparos
    private float nextFire = 0f; // Tiempo en el que se podr� disparar de nuevo

    void Update()
    {
        // Comprueba si se ha hecho clic y si ya se puede disparar
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            // Instancia el objeto del proyectil en la posici�n y rotaci�n del objeto actual
            Instantiate(projectile, transform.position, transform.rotation);

            // Actualiza el tiempo en el que se podr� disparar de nuevo
            nextFire = Time.time + fireRate;
        }
    }
}

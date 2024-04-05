using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacar : MonoBehaviour
{

    Stats playerStats;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hola");

        // Si choca contra este objeto el jugador, le va a quitar un punto a su variable 
        // health
        if (collision.gameObject.tag == "Player")
        {
            playerStats = collision.gameObject.GetComponent<Stats>();
            playerStats.health = playerStats.health - 1;
            Debug.Log(playerStats.health);


        }
    }
}

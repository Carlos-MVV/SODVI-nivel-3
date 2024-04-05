using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    Stats playerStats;

    // Update is called once per frame

    //Cuando lo toque cambia la vida del jugador a 0.   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerStats = collision.gameObject.GetComponent<Stats>();
        playerStats.health = 0;
    }
}

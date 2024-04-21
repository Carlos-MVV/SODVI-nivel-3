using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacar : MonoBehaviour
{

    Stats playerStats;
    public List<GameObject> healthObjects = new List<GameObject>();
    [SerializeField] AudioSource audioSource;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hola");

        // Si choca contra este objeto el jugador, le va a quitar un punto a su variable 
        // health
        if (collision.gameObject.tag == "Player")
        {
            // Reproduce el sonido asociado con el objeto recolectable.
            if (audioSource != null)
                audioSource.Play();

            playerStats = collision.gameObject.GetComponent<Stats>();
            playerStats.health = playerStats.health - 1;
            Debug.Log(playerStats.health);

            // Desactivar el objeto de vida correspondiente según las vidas restantes
            if (playerStats.health >= 0 && playerStats.health < healthObjects.Count)
            {
                healthObjects[playerStats.health].SetActive(false);
            }

        }
    }
}

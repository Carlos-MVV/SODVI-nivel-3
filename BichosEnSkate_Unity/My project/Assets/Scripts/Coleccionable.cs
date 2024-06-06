using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] int puntos = 15; // Puntos que otorga este coleccionable


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si choca contra este objeto el jugador
        if (collision.gameObject.tag == "Player")
        {
            // Reproduce el sonido asociado con el objeto recolectable.
            if (audioSource != null)
            {
                audioSource.Play();
                Debug.Log("Sonido");
            }
           
            // Incrementa los puntos en el GameMaster
            GameMaster gameMaster = FindObjectOfType<GameMaster>();
            if (gameMaster != null)
                gameMaster.IncrementarPuntos(puntos);

            // Desactivar este objeto recolectable.
            gameObject.SetActive(false); ;
        }
    }

}

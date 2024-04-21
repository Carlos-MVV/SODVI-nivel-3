using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] int puntos = 15; // Puntos que otorga este coleccionable

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si choca contra este objeto el jugador
        if (collision.gameObject.tag == "Player")
        {
            // Reproduce el sonido asociado con el objeto recolectable.
            if (audioSource != null)
                audioSource.Play();

            // Incrementa los puntos en el GameMaster
            GameMaster gameMaster = FindObjectOfType<GameMaster>();
            if (gameMaster != null)
                gameMaster.IncrementarPuntos(puntos);

            // Desactivar este objeto recolectable.
            gameObject.SetActive(false); ;
        }
    }

}

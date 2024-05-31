using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    public int health = 3;
    private GameMaster gm;
    
    void Start()
    {
        //Obtenemos la posición guardada en el GameMaster y se la asignamos al jugador
        //Al inicio no hay problema porque incia junto con el jugador
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;

        gm.ActualizarUI();
    }
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            //Si pierdes todas tus vida se resetea la escena desde el inicio
            muerteReset();

        }
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{


    //}
    public void respawn()// Llama al método Respawn cuando la salud decremente
    {
        if (health <= 0)
        {
            //Si pierdes todas tus vida se resetea la escena desde el inicio
            muerteReset();

        } else
        {
            transform.position = gm.lastCheckPointPos; // Reaparece en el último checkpoint

            // health = 3; // Reinicia la salud del jugador
            gm.ActualizarUI(); // Actualiza la UI al reaparecer

            // en GameMaster por los Checkpoints
            //transform.position = gm.lastCheckPointPos;
            if (gm.puntajeSystem != null)
            {
                gm.puntajeSystem.ActualizarPuntos(gm.score);
            }
        }

    }

    public void muerteReset()
    {
        gm.lastCheckPointPos = new Vector2(0,0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

        gm.ActualizarUI(); // Actualiza la UI al reaparecer
        if (gm.puntajeSystem != null)
        {
            gm.puntajeSystem.ActualizarPuntos(gm.score);
        }
    }
}

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
    }
    // Update is called once per frame
    void Update()
    {
        respawn();
    }


    void respawn()
    {
        //Si pierdes todas tus vida se resetea la escena desde la última posición guardada
        // en GameMaster por los Checkpoints
        if (health <= 0)
        {
           //Obtenemos el índice de la la escena actual y la cargamos
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

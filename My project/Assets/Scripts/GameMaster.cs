using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector2 lastCheckPointPos;

    public int score = 0;
    public UIController puntajeSystem; // Referencia al script UIController


    public void IncrementarPuntos(int cantidad)
    {
        score += cantidad; // Incrementar los puntos por la cantidad especificada
        //mostrar los puntos en la interfaz de usuario
        if (puntajeSystem != null)
            puntajeSystem.ActualizarPuntos(score); // Actualizar el texto en la UI
    }

    public void ReiniciarPuntuacion()
    {
        score = 0;
        if (puntajeSystem != null)
            puntajeSystem.ActualizarPuntos(score); // Actualizar el texto en la UI
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
           // DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

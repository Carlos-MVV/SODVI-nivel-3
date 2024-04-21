using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text puntajeText; // Referencia al objeto de texto para mostrar los puntos

    // Método para actualizar el texto con los puntos acumulados
    public void ActualizarPuntos(int score)
    {
        if (puntajeText != null)
            puntajeText.text = "Puntaje: " + score.ToString(); // Actualiza el texto con la cantidad de puntos // Actualiza el texto con la cantidad de puntos    }
    }
}

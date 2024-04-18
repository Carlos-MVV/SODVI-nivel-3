using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralaje_Fondo : MonoBehaviour
{
    public float speed = 1f; // Velocidad general de movimiento

    void Update()
    {
        // Obtener el desplazamiento en el eje X
        float offsetX = Time.time * speed;

        // Aplicar el desplazamiento a cada capa de fondo
        foreach (Transform layer in transform)
        {
            // Calcular la posición deseada para la capa de fondo
            float posX = offsetX * layer.position.z;

            // Crear un vector de posición actualizado
            Vector3 targetPos = new Vector3(posX, layer.position.y, layer.position.z);

            // Aplicar la posición actualizada a la capa de fondo
            layer.position = Vector3.Lerp(layer.position, targetPos, Time.deltaTime);
        }
    }
}


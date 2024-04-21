using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
//using System.Collections.Specialized;
//using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mov_Flechas : MonoBehaviour
{

    public float velocidadInicial;
    public float incrementoVelocidad;
    public float velocidadMax;
    public float suavizadoDesplazamiento;


    private float desplazamientoDer = 0f;
    private float desplazamientoIzq = 0f;
    private float frenado;

    public Rigidbody2D rb2D;
    private Vector3 velocidad = Vector3.zero;

    private bool Salto = false;
    private bool DesplazarDerecha = false;
    private bool DesplazarIzquierda = false;
    private bool Frenar = false;

    private bool MirarDer = true;

    [Header("Salto")]

    public float fuerzaSalto;
    public LayerMask esSuelo;
    public Transform DetectorSuelo;
    public Vector3 tamañoDetector;
    private bool enSuelo;

    private Movimientos movimientos;

    //Animacion
    [SerializeField] Animator animator;
    //Sonido
    [SerializeField] PlayerSoundEffects sonidoCode;


    private void Awake()
    {
        movimientos = new Movimientos();
    }

    private void OnEnable()
    {
        movimientos.Enable();
    }

    private void OnDisable()
    {
        movimientos.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        desplazamientoDer = movimientos.Desplazamiento.MovDerecha.ReadValue<float>() * velocidadInicial;
        desplazamientoIzq = movimientos.Desplazamiento.MovIzquierda.ReadValue<float>() * -velocidadInicial;
        frenado = movimientos.Desplazamiento.Frenar.ReadValue<float>();

        if (movimientos.Desplazamiento.MovDerecha.triggered)
        {
            DesplazarIzquierda = false;
            DesplazarDerecha = true;
            Frenar = false;
        }

        if (movimientos.Desplazamiento.MovIzquierda.triggered)
        {
            DesplazarDerecha = false;
            DesplazarIzquierda = true;
            Frenar = false;
        }

        if (movimientos.Desplazamiento.Frenar.triggered)
        {
            Frenar = true;
        }
        //desplazamientoH = Input.GetAxis("Horizontal") * velocidadInicial;

        // Si se presiona la tecla de mover a la derecha y no estamos frenando, desplazarse hacia la derecha indefinidamente
        if (DesplazarDerecha && !Frenar)
        {
            desplazamientoDer = velocidadInicial;
        }
        // Si se presiona la tecla de mover a la izquierda y no estamos frenando, desplazarse hacia la izquierda indefinidamente
        else if (DesplazarIzquierda && !Frenar)
        {
            desplazamientoIzq = -velocidadInicial;
        }

        if (movimientos.Desplazamiento.Saltar.triggered)//Input.GetButtonDown("Jump"))
        {
            Salto = true;
        }


    }

    private void FixedUpdate()
    {
        if (Frenar)
        {
            animator.enabled = true;
            animator.SetBool("isStoping", true);
            Debug.Log("Frenando");

            // Mathf.Lerp: Esta función suaviza la transición entre la velocidad actual y cero, con la velocidad de frenado definida por suavizadoDesplazamiento
            desplazamientoDer = Mathf.Lerp(desplazamientoDer, 0f, suavizadoDesplazamiento * Time.fixedDeltaTime);
            DesplazarDerecha = false;
            Debug.Log("DETENIDO DER");
            animator.SetBool("isStoping", false);

            desplazamientoIzq = Mathf.Lerp(desplazamientoIzq, 0f, suavizadoDesplazamiento * Time.fixedDeltaTime);
            DesplazarIzquierda = false;
            Debug.Log("DETENIDO IZ");
            animator.SetBool("isStoping", false);

            Desplazar(desplazamientoDer * Time.fixedDeltaTime, Salto);

            // Verifica si la velocidad es muy baja para detener completamente el movimiento
            /*if (Mathf.Abs(desplazamientoDer) < 0.1f)
            {
                desplazamientoDer = 0f;
                DesplazarDerecha = false;  
            }

            if (Mathf.Abs(desplazamientoIzq) < 0.1f)
            {
                desplazamientoIzq = 0f;
                DesplazarIzquierda = false;   
            }*/
        }

        if (DesplazarDerecha)
        {
            Debug.Log("ACTIVADO DER");
            Desplazar(desplazamientoDer * Time.fixedDeltaTime, Salto);
        }


        if (DesplazarIzquierda)
        {
            Debug.Log("ACTIVADO IZ");
            Desplazar(desplazamientoIzq * Time.fixedDeltaTime, Salto);
        }
        // Aplicar suavizado cuando no se esté desplazando ni a la izquierda ni a la derecha
      /*  if (!DesplazarDerecha && !DesplazarIzquierda)
        {
            desplazamientoDer = Mathf.Lerp(desplazamientoDer, 0f, suavizadoDesplazamiento * Time.fixedDeltaTime);
            desplazamientoIzq = Mathf.Lerp(desplazamientoIzq, 0f, suavizadoDesplazamiento * Time.fixedDeltaTime);
            
        }*/

        enSuelo = Physics2D.OverlapBox(DetectorSuelo.position, tamañoDetector, 0f, esSuelo);
        Salto = false;
    }


    private void Desplazar(float despl, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(despl, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDesplazamiento);

        if (despl > 0 && !MirarDer)
        {
            //animator.SetBool("isJumpingR", false);
            Girar();
            //Activamos animación
            animator.enabled = true;
        }
        else if (despl < 0 && MirarDer)
        {
            //animator.SetBool("isJumpingR", false);
            Girar();
            //Activamos animación
            animator.enabled = true;
        }

        if (enSuelo && saltar)
        {
            rb2D.AddForce(new Vector2(0f, fuerzaSalto));
            Debug.Log("Activamos salto animacion");
            animator.enabled = true;
            animator.SetBool("isJumpingR", true);
            //Sonido
            sonidoCode.Jump();
            enSuelo = false;
        }
        // Desactivar la animación de salto cuando el personaje ya no está en el suelo
        else if (enSuelo && animator.GetBool("isJumpingR"))
        {
            Debug.Log("Desactivamos salto animacion");
            animator.SetBool("isJumpingR", false);
        }



        if (Mathf.Abs(despl) < velocidadMax)
        {
            desplazamientoDer += incrementoVelocidad * Time.fixedDeltaTime;
            desplazamientoIzq -= incrementoVelocidad * Time.fixedDeltaTime;
        }
    }

    private void Girar()
    {
        MirarDer = !MirarDer;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(DetectorSuelo.position, tamañoDetector);
    }
}

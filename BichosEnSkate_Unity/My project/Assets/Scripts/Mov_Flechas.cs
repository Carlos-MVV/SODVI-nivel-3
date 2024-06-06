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
    public bool Frenar = false;

    private bool MirarDer = true;

    [Header("Animations")]
    public float delayAnim = 1f;
    public float delayAtackAnim = 1f;

    [Header("Instrucciones")]
    public float delayInstructions = 3f;
    public GameObject instructions1;
    //public GameObject instructions2;


    [Header("Barrido")]
    public float duracionAtaque = 1f; // Duración del ataque en segundos
    public float radioAtaque = 0.5f; // Radio de detección de ataque
    private bool atacando = false;


    [Header("Salto")]

    public float fuerzaSalto;
    public LayerMask esSuelo;
    public Transform DetectorSuelo;
    public Vector3 tamañoDetector;
    private bool enSuelo;

    private Movimientos movimientos;
    

    //Animacion
    [SerializeField] Animator animator;
    [Header("SONIDOS")]
    [SerializeField] AudioSource audioSource;
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

        if (movimientos.Desplazamiento.Atacar.triggered && !atacando)//Input.GetButtonDown(""))
        {
            ActivarAtaque();
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            ActivarImpacto();
        }

        if (collision.gameObject.tag == "Rampa")
        {
            ActivarSubida();
        }
        if (collision.gameObject.tag == "DeathZone")
        {
            Frenar = true;
        }

    }    


    private void FixedUpdate()
    {
        if (Frenar)
        {
            //instructions2.SetActive(false);

            
            Debug.Log("Frenando");
            
            // Mathf.Lerp: Esta función suaviza la transición entre la velocidad actual y cero, con la velocidad de frenado definida por suavizadoDesplazamiento
            desplazamientoDer = Mathf.Lerp(desplazamientoDer, 0f, suavizadoDesplazamiento * Time.fixedDeltaTime);
            DesplazarDerecha = false;
            Debug.Log("DETENIDO DER");
            animator.SetBool("isStoping", true);

            desplazamientoIzq = Mathf.Lerp(desplazamientoIzq, 0f, suavizadoDesplazamiento * Time.fixedDeltaTime);
            DesplazarIzquierda = false;
            Debug.Log("DETENIDO IZ");
            animator.SetBool("isStoping", true);
            


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
            animator.SetBool("isStoping", false);
            Debug.Log("ACTIVADO DER");
            Desplazar(desplazamientoDer * Time.fixedDeltaTime, Salto);
            instructions1.SetActive(false);
            // Invoca el método para mostrar las siguientes instrucciones después de un retraso
           /* if (!instructions2.activeSelf)
            {
                Invoke("ShowNextInstructions", delayInstructions);
            }*/
        }


        if (DesplazarIzquierda)
        {
            animator.SetBool("isStoping", false);
            Debug.Log("ACTIVADO IZ");
            Desplazar(desplazamientoIzq * Time.fixedDeltaTime, Salto);
            instructions1.SetActive(false);
            // Invoca el método para mostrar las siguientes instrucciones después de un retraso
            /*if (!instructions2.activeSelf)
            {
                Invoke("ShowNextInstructions", delayInstructions);
            }*/
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
            //Animacion
            Debug.Log("Activamos salto animacion");
            //animator.enabled = true;
            animator.SetBool("isJumpingR", true);
            //Sonido
            sonidoCode.Jump();
            enSuelo = false;
        }
        // Desactivar la animación de salto cuando el personaje ya no está en el suelo
        else if (enSuelo && animator.GetBool("isJumpingR"))
        {
            Debug.Log("Desactivamos salto animacion");
            Invoke("JumpAnimation", delayAnim);
            
        }



        if (Mathf.Abs(despl) < velocidadMax)
        {
            desplazamientoDer += incrementoVelocidad * Time.fixedDeltaTime;
            desplazamientoIzq -= incrementoVelocidad * Time.fixedDeltaTime;
        }
    }


    //CORUTINA DE ATAQUE BARRIDO

    public void ActivarAtaque()
    {
        // Iniciar la corutina para manejar el impacto
        StartCoroutine(AtaqueCorutina());
    }

    IEnumerator AtaqueCorutina()
    {
        atacando = true;
        Debug.Log("ATACANDO");
        // Detectar enemigos en el radio de ataque y eliminarlos
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(transform.position, radioAtaque);
        foreach (Collider2D enemy in enemigos)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Debug.Log("Enemigo Detectado");
                enemy.GetComponent<Atacar>().SerGolpeado();
            }
        }

        // Iniciar animación de ataque 
        animator.SetBool("isAttacking", true);
        if (audioSource != null)
            audioSource.Play();

            // Esperar por 0.5 segundos (puedes ajustar este valor)
            yield return new WaitForSeconds(0.9f);

        animator.SetBool("isAttacking", false);
        atacando = false;
    }


    //CORUTINA DE IMPACTO CHOQUE

    public void ActivarImpacto()
    {
        // Iniciar la corutina para manejar el impacto
        StartCoroutine(ImpactoCorutina());
    }

    IEnumerator ImpactoCorutina()
    {
        // Activar la animación de impacto
        animator.SetBool("isCrashing", true);

        // Esperar por 0.5 segundos (puedes ajustar este valor)
        yield return new WaitForSeconds(0.5f);

        // Desactivar la animación de impacto
        animator.SetBool("isCrashing", false);
    }

    //CORUTINA DE SUBIDA RAMPAS

    public void ActivarSubida()
    {
        // Iniciar la corutina para manejar el impacto
        StartCoroutine(SubidaCorutina());
    }

    IEnumerator SubidaCorutina()
    {
        // Activar la animación de impacto
        animator.SetBool("isGoUp", true);

        // Esperar por 0.5 segundos (puedes ajustar este valor)
        yield return new WaitForSeconds(0.6f);

        // Desactivar la animación de impacto
        animator.SetBool("isGoUp", false);
    }

    private void JumpAnimation()
    {
        animator.SetBool("isJumpingR", false);
    }

    /*
    private void ShowNextInstructions()
    {
        instructions2.SetActive(true);
    }
    */

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
        Gizmos.DrawWireSphere(transform.position, radioAtaque);
    }

}

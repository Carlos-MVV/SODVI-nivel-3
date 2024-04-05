using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    //Componentes
    [SerializeField] GameObject piedra;
    [SerializeField] GameObject player;
    Rigidbody2D playerRigidbody2D;
    SpriteRenderer playerSprite;
    TouchCode playerTouchCode;
    [SerializeField] Animator animator;
    //Sonido
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Cambia la dirección del jugador
        if (collision.gameObject.tag == "Player")
        {
            //Cambia la variable "dir" de TouchCode a la opuesta de la actual
            Debug.Log("Switch!!");
            playerTouchCode = collision.gameObject.GetComponent<TouchCode>();
            playerTouchCode.dir = !playerTouchCode.dir;
            //playerSprite = collision.gameObject.GetComponent<SpriteRenderer>();
            //playerSprite.flipX = !playerSprite.flipX;
            //Activamos sonido de cambio
            audioSource.clip = audioClip;
            audioSource.Play();
            if (playerTouchCode.dir == true)
            {
                animator.SetBool("isLeft", false);
            }
            else
            {
                animator.SetBool("isLeft", true);
            }
            
        }
    }
}

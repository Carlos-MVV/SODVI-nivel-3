using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impulsoVertical : MonoBehaviour
{
    Rigidbody2D playerRigidBody;
    //TouchCode playerTouchCode;
    private float boostJump = 14f;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           // playerTouchCode = collision.gameObject.GetComponent<TouchCode>();
            playerRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRigidBody.AddForce(new Vector2(playerRigidBody.velocity.x,boostJump), ForceMode2D.Impulse);
            animator.enabled = true;
            if (audioSource != null)
                audioSource.Play();
        }


    }
}

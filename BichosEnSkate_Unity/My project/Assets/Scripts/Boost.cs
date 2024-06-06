using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{

    Rigidbody2D playerRigidBody;
    TouchCode playerTouchCode;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] float boostTime = 10f;

    // Update is called once per frame
    void Update()
    {
      
    }

    IEnumerator speedUp()
    {
        playerTouchCode.coroutineEnemy = true;
        Debug.Log("Boost!!!!");
        if (playerTouchCode.dir == true)
        {
            playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x + boostSpeed, playerRigidBody.velocity.y);
            yield return new WaitForSeconds(boostTime);
            playerTouchCode.coroutineEnemy = false;
        }else
        {
            playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x - boostSpeed, playerRigidBody.velocity.y);
            yield return new WaitForSeconds(boostTime);
            playerTouchCode.coroutineEnemy = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerTouchCode = collision.gameObject.GetComponent<TouchCode>();
            playerRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
            StartCoroutine(speedUp());
        }


    }

}

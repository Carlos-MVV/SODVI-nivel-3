using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] int nivel;

    private GameMaster gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
           // gm.isEndOfLevel = true;
            gm.lastCheckPointPos = new Vector2(0, 0);
            Invoke("ReloadScene",2f);
        }
        
    }

    void ReloadScene()
    {
        Debug.Log("Ganaste");
        // gm.isEndOfLevel = false; // Reinicia el estado del final del nivel
        SceneManager.LoadScene(nivel);
    }
}

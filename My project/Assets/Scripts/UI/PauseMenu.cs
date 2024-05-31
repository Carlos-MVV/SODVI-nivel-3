using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject DarkBackGround;
    [SerializeField] AudioSource audioSourcePause;
    [SerializeField] AudioSource audioSourceMainSong;
    [SerializeField] AudioSource audioSourceButton;

    private GameMaster gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    public void Pause()
    {
        audioSourceButton.Play();
        // Reproduce el sonido asociado con el objeto recolectable.
        if (audioSourcePause != null) { 
            audioSourceMainSong.Pause();
            audioSourcePause.Play();
        }
        pauseMenu.SetActive(true);
        DarkBackGround.SetActive(true);
        Time.timeScale = 0;
    }

    public void MainMenu()
    {
        audioSourceButton.Play();
        gm.lastCheckPointPos = new Vector2(0, 0);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Resume()
    {
        audioSourceButton.Play();
        pauseMenu.SetActive(false);
        audioSourcePause.Stop();
        audioSourceMainSong.Play();
        DarkBackGround.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        audioSourceButton.Play();
        gm.lastCheckPointPos = new Vector2(0,0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}

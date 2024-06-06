using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelMenu : MonoBehaviour
{
    public void ReloadScene(int nivel)
    {
        Debug.Log("Iniciando nivel");
        SceneManager.LoadScene(nivel);
    }
}

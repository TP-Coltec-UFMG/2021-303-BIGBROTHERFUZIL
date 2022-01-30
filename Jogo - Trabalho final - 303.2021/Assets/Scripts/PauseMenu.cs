using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public bool GameRestarted = false;

    public GameObject pauseMenuUI;
    public GameObject canvas;

    public AudioClip resume;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused == true){
                Resume();
            }else{
                Pause();
            }
        }
    }

    public void Resume(){
        GetComponent<AudioSource>().PlayOneShot(resume);
        pauseMenuUI.SetActive(false);
        canvas.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        canvas.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;

        GetComponent<GeneralButton>().PlaySound();
    }

    public void LoadMenu(){
        SceneManager.LoadScene("MenuInicial");
    }

    public void QuitGame(){
        GameRestarted = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Aqui, irá pra outra tela, mas ainda não fizemos essa parte.
        Debug.Log("Jogo em construcao.");
        SceneManager.LoadScene("GameLevel");
    }

    public void Exit()
    {
        // Aqui, irá pra outra tela, mas ainda não fizemos essa parte.
        Debug.Log("SAIR.");
        Application.Quit();
    }
}

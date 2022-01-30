using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public GameObject inimigo;
    private float momentoDaUltimaGeracao;
    private float tempoDesaparece;
    [SerializeField] private float tempoDeCriacao = 7f;
    public float tempoTexto = 3f;
    public GameObject gameOver;
    public GameObject text;
    public GameObject PauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
        tempoDesaparece = Time.time + tempoDeCriacao;
    }

    // Update is called once per frame
    void Update()
    {
        float tempo = Time.time;
        
        if (PauseMenu.GetComponent<PauseMenu>().GameRestarted == true){
            momentoDaUltimaGeracao = tempo;
            tempo = 0;
            PauseMenu.GetComponent<PauseMenu>().GameRestarted = false;
        }
        
        if (!gameOver.activeSelf){
            if (tempo > momentoDaUltimaGeracao + tempoDeCriacao){
                momentoDaUltimaGeracao = tempo;
                Vector3 posicaoDoGerador = new Vector3 (Random.Range(-10f, 10f), -4.76f, 0f);
                Instantiate(inimigo, posicaoDoGerador, Quaternion.identity);
                text.SetActive(true);
                text.GetComponent<GeneralButton>().PlaySound();
            }
        }

        if (text.activeSelf == true && tempo > tempoDesaparece + tempoTexto){
            tempoDesaparece = tempo;
            text.SetActive(false);
        }
    }
}

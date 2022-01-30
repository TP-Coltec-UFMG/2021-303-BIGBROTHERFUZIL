using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    Dropdown dropdown;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(delegate{
            mudaFonte(dropdown, canvas);
        });        
    }

    public void mudaFonte(Dropdown dropdown, GameObject canvas1){
        int grande = 0, normal = 0, pequeno = 0;
        
        foreach (Transform obj in canvas1.transform){
            foreach(Transform child in obj){
                Text text = child.gameObject.GetComponentInChildren<Text>();
                
                // Se o texto não estiver em outro gameObject, pega ele direto
                if (text == null) text = child.gameObject.GetComponent<Text>();
                
                if (text != null){
                    if (dropdown.value == 0){
                        grande = 24;
                        normal = 18;
                        pequeno = 14;
                    }else if(dropdown.value == 1){
                        grande = 18;
                        normal = 14;
                        pequeno = 10;
                    }
                    else if(dropdown.value == 2){
                        grande = 45;
                        normal = 24;
                        pequeno = 20;
                    }
                    altera(text, grande, normal, pequeno);
                }
            }
        }
    }

    private void altera (Text text, int tamGrande, int tamNormal, int tamPequeno){
        if(text.tag == "Titulo"){
            text.fontSize = tamGrande;
        }else if(text.tag == "Normal"){
            text.fontSize = tamNormal;
        }else if (text.tag == "Pequeno"){
            text.fontSize = tamPequeno;
        }
    }
}
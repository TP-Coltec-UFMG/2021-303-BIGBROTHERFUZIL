using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GeneralButton : MonoBehaviour
{
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
    }


    public void PlaySound()
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
    }
}

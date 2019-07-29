﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text uiText;
    public float mainTimer;

    public float timer;
    public bool canCount = true;
    public bool doOnce = false;
    public GameObject panel;


    void Start()
    {
        timer = mainTimer;
    }

    void Update()
    {
           if(timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            uiText.text = timer.ToString("F");
           
        } 
         
        else if(timer <= 0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            uiText.text = "0.00";
            timer = 0.0f;
            panel.gameObject.SetActive(true);
        }
    }

    public void ResetBtn()
    {
        timer = mainTimer;
        canCount = true;
        doOnce = false;
        panel.gameObject.SetActive(false);
    }

}

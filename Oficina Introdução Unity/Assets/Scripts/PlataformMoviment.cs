﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformMoviment : MonoBehaviour
{
    private bool moveUp = true;
    private bool moveHorizontal = true;
    public float moveSpeed, distance;
    public bool moverVetical, moverHorizontal;
    


    void Update()
    {
        MoveToHorizontal();
        moveToVertical();

    }
    
    void MoveToHorizontal()
    {
        if (moverVetical)
        {
            if (transform.position.x > distance)
            {
                moveHorizontal = false;
            }
            if (transform.position.x < -distance)
            {
                moveHorizontal = true;
            }
            if (moveHorizontal)
            {
                transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            }
        }
    }

    void moveToVertical()
    {
        if (moverHorizontal)
        {
            if (transform.position.y > distance)
            {
                moveUp = false;
            }
            if (transform.position.y < -distance)
            {
                moveUp = true;
            }
            if (moveUp)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
            }
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlowWind : MonoBehaviour
{
    bool isDragging = false;
    bool changedDir = false;

    Vector2 lastMousePos;
    float diffTime = 0;
    public float maxPower;

    private void OnMouseDown()
    {
        isDragging = true;
        lastMousePos = Input.mousePosition; //todo que solo cuente el drag si ha salido del button
    }
    private void OnMouseUp()
    {
        if (!changedDir)
        {
            CalculateAndSend();
        }
        if (Input.GetMouseButton(0))
        {
            changedDir = true;
        }
        else
        {
            changedDir = false;
        }

    }

    private void CalculateAndSend()
    {
        Vector2 mousePos = Input.mousePosition;

        float xDelta = Mathf.Abs(lastMousePos.x - mousePos.x);
        float mouseSpeed = xDelta / diffTime/100;
        SendWind(mouseSpeed);

        diffTime = 0;
        isDragging = false;
    }

    private void OnMouseDrag()
    {
        diffTime += Time.deltaTime;

        if (Input.GetAxis("Mouse X") < 0f)
        {
            OnMouseUp();
        }
    }

    void SendWind(float power)
    {
        if (power > maxPower)
        {
            power = maxPower;
        }
        WindManager.Instance.airFactor += (power*0.7f) / maxPower;
        if (WindManager.Instance.airFactor > 1)
        {
            WindManager.Instance.airFactor = 1;
        }
    }
}

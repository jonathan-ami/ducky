using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiding : MonoBehaviour
{
    private bool isHiding;
    private bool isUnderwater;
    private float startTime;
    public float hidingTime = 3f;
    public void SetHiding(bool hiding)
    {
        isHiding = hiding;
    }

    public bool GetHiding()
    {
        return isHiding;
    }

    public bool GetUnderWater()
    {
        return isUnderwater;
    }

    void Update()
    {
        if (isHiding && isUnderwater)
        {
            // Check if hiding time has passed
            if (Time.time - startTime >= hidingTime)
            {
                SetHiding(false);
                isUnderwater = false; // Set isUnderwater to false when hiding ends
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                isHiding = false;
                isUnderwater = false;
            }
        }
        else
        {
            // Check for user input to start hiding
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetHiding(true);
                isUnderwater = true;
                startTime = Time.time;
            }
        }
    }

}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengerCount : MonoBehaviour
{
    public static int redPassengerCount = 0;
    public static int greenPassengerCount = 0;
    public static int yellowPassengerCount = 0;
    public static int bluePassengerCount = 0;
    public static int transported;

    public Text redPassengerDisplay;
    public Text greenPassengerDisplay;
    public Text yellowPassengerDisplay;
    public Text bluePassengerDisplay;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onPassengerPickup += UpdatePassengerCount;
        GameEvents.current.onPassengerRelease += DeletePassengerCount;
    }
    void Update()
    {
        this.gameObject.transform.Rotate(new Vector3(0, 0, 1), Space.Self);
    }

    private void UpdatePassengerCount(Color color)
    {
        Color red = new Color(1, 0, 0,1);
        Color green = new Color(0, 1, 0,1);
        Color blue = new Color(0, 0, 1,1);
        Color yellow = new Color(1, 1,0 ,1);
        

        if (color == red)
        {     
            redPassengerCount ++;
            TextChange(redPassengerDisplay, redPassengerCount);
        }
        else if (color == green)
        {
            greenPassengerCount ++;
            TextChange(greenPassengerDisplay, greenPassengerCount);
        }
        else if (color == blue)
        {
            bluePassengerCount ++;
            TextChange(bluePassengerDisplay, bluePassengerCount);
        }
        else if (color == yellow)
        {
            yellowPassengerCount ++;
            TextChange(yellowPassengerDisplay, yellowPassengerCount);
        }
        transported++;

    }

    private void DeletePassengerCount(Color color)
    {
        Debug.Log("Deleted");
        Color red = new Color(1, 0, 0, 1);
        Color green = new Color(0, 1, 0, 1);
        Color blue = new Color(0, 0, 1, 1);
        Color yellow = new Color(1, 1, 0, 1);


        if (color == red)
        {
            GameEvents.current.FuelPickup(redPassengerCount);
            redPassengerCount = 0;
            TextChange(redPassengerDisplay, redPassengerCount);
        }
        else if (color == green)
        {
            GameEvents.current.FuelPickup(greenPassengerCount);
            greenPassengerCount = 0;
            TextChange(greenPassengerDisplay, greenPassengerCount);
        }
        else if (color == blue)
        {
            GameEvents.current.FuelPickup(bluePassengerCount);
            bluePassengerCount = 0;
            TextChange(bluePassengerDisplay, bluePassengerCount);
        }
        else if (color == yellow)
        {
            GameEvents.current.FuelPickup(yellowPassengerCount);
            yellowPassengerCount = 0;
            TextChange(yellowPassengerDisplay, yellowPassengerCount);
        }
        
    }

    public void TextChange(Text Passenger, int passCount)
    {
        
        Passenger.text = passCount.ToString();
    }
}

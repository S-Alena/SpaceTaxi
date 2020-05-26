using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengerCount : MonoBehaviour
{
    public static int redPassengerCount = 0;
    public static int greenPassengerCount = 0;
    public static int yellowPassengerCount = 0;
    public static int bluePassengerCount = 0;

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

    private void UpdatePassengerCount(Color color)
    {
        Color red = new Color(1, 0, 0,1);
        Color green = new Color(0, 1, 0,1);
        Color blue = new Color(0, 0, 1,1);
        Color yellow = new Color(1, 1,0 ,1);
        

        if (color == red)
        {
            redPassengerCount ++;
            TextChange(redPassengerDisplay, redPassengerCount, "Rote");
        }
        else if (color == green)
        {
            greenPassengerCount ++;
            TextChange(greenPassengerDisplay, greenPassengerCount, "Grüne");
        }
        else if (color == blue)
        {
            bluePassengerCount ++;
            TextChange(bluePassengerDisplay, bluePassengerCount, "Blaue");
        }
        else if (color == yellow)
        {
            yellowPassengerCount ++;
            TextChange(yellowPassengerDisplay, yellowPassengerCount, "Gelbe");
        }
        
    }

    private void DeletePassengerCount(Color color)
    {
        Color red = new Color(1, 0, 0, 1);
        Color green = new Color(0, 1, 0, 1);
        Color blue = new Color(0, 0, 1, 1);
        Color yellow = new Color(1, 1, 0, 1);


        if (color == red)
        {
            redPassengerCount = 0;
        }
        else if (color == green)
        {
            greenPassengerCount = 0;
        }
        else if (color == blue)
        {
            bluePassengerCount = 0;
        }
        else if (color == yellow)
        {
            yellowPassengerCount = 0;
        }
        
    }

    public void TextChange(Text Passenger, int passCount, string Color)
    {
        
        Passenger.text = Color + " Passagiere: " + passCount;
    }
}

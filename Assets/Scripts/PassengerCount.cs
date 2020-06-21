using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengerCount : MonoBehaviour
{
    public static int redPassengerCount = 0;
    public static int pinkPassengerCount = 0;
    public static int yellowPassengerCount = 0;
    public static int bluePassengerCount = 0;
    public static int transported;

    public Text redPassengerDisplay;
    public Text pinkPassengerDisplay;
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

        if (color == TaxiManager.red)
        {     
            redPassengerCount ++;
            TextChange(redPassengerDisplay, redPassengerCount);
        }
        else if (color == TaxiManager.pink)
        {
            pinkPassengerCount ++;
            TextChange(pinkPassengerDisplay, pinkPassengerCount);
        }
        else if (color == TaxiManager.blue)
        {
            bluePassengerCount ++;
            TextChange(bluePassengerDisplay, bluePassengerCount);
        }
        else if (color == TaxiManager.yellow)
        {
            yellowPassengerCount ++;
            TextChange(yellowPassengerDisplay, yellowPassengerCount);
        }
        transported++;

    }

    private void DeletePassengerCount(Color color)
    {
        Debug.Log("Deleted");


        if (color == TaxiManager.red)
        {
            GameEvents.current.FuelPickup(redPassengerCount);
            redPassengerCount = 0;
            TextChange(redPassengerDisplay, redPassengerCount);
        }
        else if (color == TaxiManager.pink)
        {
            GameEvents.current.FuelPickup(pinkPassengerCount);
            pinkPassengerCount = 0;
            TextChange(pinkPassengerDisplay, pinkPassengerCount);
        }
        else if (color == TaxiManager.blue)
        {
            GameEvents.current.FuelPickup(bluePassengerCount);
            bluePassengerCount = 0;
            TextChange(bluePassengerDisplay, bluePassengerCount);
        }
        else if (color == TaxiManager.yellow)
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

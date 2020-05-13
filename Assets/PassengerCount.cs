using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengerCount : MonoBehaviour
{
    public int redPassengerCount = 0;
    public int greenPassengerCount = 0;
    public int yellowPassengerCount = 0;

    public Text redPassengerDisplay;
    public Text greenPassengerDisplay;
    public Text yellowPassengerDisplay;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onPassengerPickup += UpdatePassengerCount;
    }

    private void UpdatePassengerCount()
    {
        redPassengerCount = redPassengerCount + 1;
        TextChange();
    }

    public void TextChange()
    {
        redPassengerDisplay.text = "Passengers: " + redPassengerCount;
    }
}

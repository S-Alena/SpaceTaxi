using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class CollisionTrigger : MonoBehaviour
{
    public Text endText;
    public bool end = false;


    void Start()
    {
        endText.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Collided with " + collision.gameObject.name);

        int passengerCount = 0;

        if (collision.gameObject.GetComponent<SpriteRenderer>().color == TaxiManager.red)
        {
            passengerCount = PassengerCount.redPassengerCount;
        }
        else if (collision.gameObject.GetComponent<SpriteRenderer>().color == TaxiManager.pink)
        {
            passengerCount = PassengerCount.pinkPassengerCount;
        }
        else if (collision.gameObject.GetComponent<SpriteRenderer>().color == TaxiManager.blue)
        {
            passengerCount = PassengerCount.bluePassengerCount;
        }
        else if (collision.gameObject.GetComponent<SpriteRenderer>().color == TaxiManager.yellow)
        {
            passengerCount = PassengerCount.yellowPassengerCount;
        }
        else if (collision.gameObject.name == "Home")
        {

            if (PassengerCount.yellowPassengerCount == 0 && PassengerCount.pinkPassengerCount == 0 && PassengerCount.bluePassengerCount == 0 && PassengerCount.redPassengerCount == 0)
            {
                endText.text = "You made it. " + Environment.NewLine + "Transported = " + PassengerCount.transported;
            }
            else
            {
                endText.text = "Game Over." + Environment.NewLine + "Press to Restart";
            }
            endText.enabled = true;
            end = true;

        }

        GameEvents.current.PlanetCollision(collision.gameObject.name, passengerCount);
    }

}

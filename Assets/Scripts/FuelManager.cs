using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FuelManager : MonoBehaviour
{
    public int planetsInRange;
    private int messageCounter = 0;
    public GameObject spaceShip;
    private bool spaceShipIsMoving = true;
    public bool deathMessage = false;

    private void Start()
    {

    }

    private void Update()
    {
        spaceShipIsMoving = spaceShip.GetComponent<TaxiManager>().isMoving;

        if (planetsInRange <= 1 && spaceShipIsMoving == false)
        {
            messageCounter++;
        }
        
        if(messageCounter > 10)
        {
            deathMessage = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag.Equals("Planet"))
        {
            planetsInRange++;
            messageCounter = 0;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Planet"))
        {
            planetsInRange--;
        }
    }
}

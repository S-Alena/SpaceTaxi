using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }


    public event Action<string> onPlanetCollision;

    public void PlanetCollision(string nameOfPlanet)
    {
        if(onPlanetCollision != null)
        {
            onPlanetCollision(nameOfPlanet);
        }
    }


    public event Action onPassengerPickup;

    public void PassengerPickup()
    {
        if(onPassengerPickup != null)
        {
            onPassengerPickup();
        }
    }
}

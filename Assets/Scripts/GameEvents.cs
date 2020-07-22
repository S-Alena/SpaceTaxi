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


    public event Action<string,int> onPlanetCollision;

    public void PlanetCollision(string nameOfPlanet, int numberOfPassengers)
    {
        if(onPlanetCollision != null)
        {
            onPlanetCollision(nameOfPlanet, numberOfPassengers);
        }
    }


    public event Action<Color> onPassengerPickup;

    public void PassengerPickup(Color planetColor)
    {
        if(onPassengerPickup != null)
        {
            onPassengerPickup(planetColor);
        }
    }

    public event Action<Color> onPassengerRelease;

    public void PassengerRelease(Color planetColor)
    {
        if (onPassengerRelease != null)
        {
            onPassengerRelease(planetColor);
        }
    }

    public event Action<int> onFuelPickup;

    public void FuelPickup(int numberOfPassengersDropped)
    {
        if(onFuelPickup != null)
        {
            onFuelPickup(numberOfPassengersDropped);
        }
    }

    public event Action<GameObject> onFlyCommand;

    public void ReactToFlyCommand(GameObject planetWithFlyCommand)
    {
        if(onFlyCommand != null)
        {
            onFlyCommand(planetWithFlyCommand);
        }
    }

    public event Action onGameOver;

    public void RestartGame()
    {
        if(onGameOver != null)
        {
            onGameOver();
        }
    }

    public event Action onWin;

    public void ActivateNextLevel()
    {
        if (onWin != null)
        {
            onWin();
        }
    }

}

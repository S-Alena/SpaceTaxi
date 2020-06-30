using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class Multiply : MonoBehaviour
{
    //currently used for the plänet böbbel count (siehe switch case)

    public GameObject plönet;
    public GameObject böbbel;
    public int passengerRed;
    public int passengerPink;
    public int passengerBlue;
    public int passengerYellow;
    private int böbbelLimit;
    public List<GameObject> böbbelCollection = new List<GameObject>();
    public List<Color> colors = new List<Color>();


    void Start()
    {

        GameEvents.current.onPlanetCollision += OnPassengerRemoval;

        for (int i = 0; i < passengerRed; i++)
        {
            colors.Add(TaxiManager.red);
        }
        for (int i = 0; i < passengerPink; i++)
        {
            colors.Add(TaxiManager.pink);
        }
        for (int i = 0; i < passengerYellow; i++)
        {
            colors.Add(TaxiManager.yellow);
        }
        for (int i = 0; i < passengerBlue; i++)
        {
            colors.Add(TaxiManager.blue);
        }
        int count = 0;
        foreach (Color color in colors)
        {
            //erzeugt eine randomized Location
            Vector3 spawnLocation = newLocation(colors.Count, count);


            //erzeugt ein böbbel
            GameObject go = Instantiate(böbbel, spawnLocation, Quaternion.identity);
            //zählt böbbel, und setzt den Winkel eins weiter

            go.GetComponent<SpriteRenderer>().color = color;


            //print("Böbbels: " + böbbelCount);

            böbbelCollection.Add(go);
            count++;
        }


    }

    void Update()
    {

    }

    Vector3 newLocation(int limit, int böbbelNumber)
    {
        float x;
        float y;

        //Ein Kreis ist 2mal PI, geteilt durch Anzahl der böbbel, i sorgt dafür dass es um einen xten Teil verschoben wird
        float zet = böbbelNumber * 2 * Mathf.PI / limit;

        //setzt die location, ausgehend vom Mittelpunkt vom zugewiesenen PLönet
        Vector3 spawnLocation = new Vector3(
            x = Mathf.Sin(zet) * 70 + this.plönet.transform.position.x,
            y = Mathf.Cos(zet) * 70 + this.plönet.transform.position.y,
            -40);

        return spawnLocation;

    }

    private void OnPassengerRemoval(string nameOfPlanet, int numberOfColorPassengers)
    {

        if (nameOfPlanet == this.plönet.name)
        {
            int j = 0;

            //Böbbel absetzen

            for (int i = böbbelCollection.Count - 1; i >= 0; i--)
            {
                if (böbbelCollection[i].GetComponent<SpriteRenderer>().color != this.plönet.GetComponent<SpriteRenderer>().color)
                {

                    //Triggert Event das Passenger Count erhöht

                    GameEvents.current.PassengerPickup(böbbelCollection[i].GetComponent<SpriteRenderer>().color);

                    Destroy(böbbelCollection[i]);
                    böbbelCollection.Remove(böbbelCollection[i]);
                    j++;

                }
            }

            print("Anzahl Böbbel aufgenommen:" + j);

            for (int böbbelCount = 0; böbbelCount < numberOfColorPassengers; böbbelCount++)
            {
                //erzeugt eine randomized Location
                Vector3 spawnLocation = newLocation(numberOfColorPassengers, böbbelCount);

                //erzeugt ein böbbel
                GameObject go = Instantiate(böbbel, spawnLocation, Quaternion.identity);
                //zählt böbbel, und setzt den Winkel eins weiter
                go.GetComponent<SpriteRenderer>().color = this.plönet.GetComponent<SpriteRenderer>().color;

                böbbelCollection.Add(go);

                GameEvents.current.PassengerRelease(this.plönet.GetComponent<SpriteRenderer>().color);

            }
            //print("Böbbel auf Planet: " + böbbelCollection.Count);
        }


    }
}



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
    Color red = new Color(1, 0, 0,1);
    Color green = new Color(0, 1, 0,1);
    Color blue = new Color(0, 0, 1,1);
    Color yellow = new Color(1, 1,0 ,1);

    public GameObject plönet;
    public GameObject böbbel;
    public int böbbelMax;
    public int böbbelMin;
    private int böbbelLimit;
    public List<GameObject> böbbelCollection = new List<GameObject>();
    public List<Color> colors = new List<Color>();
    
    

    void Start()
    {
        Color otherColor = plönet.GetComponent<SpriteRenderer>().color;
        colors.Add(new Color(1, 0, 0, 1));
        colors.Add(new Color(0, 1, 0, 1));
        colors.Add(new Color(0, 0, 1, 1));
        colors.Add(new Color(1, 1, 0, 1));
        
        if(otherColor == red)
        {
           böbbelMin = 2;
        }
        if(otherColor == green)
        {
            böbbelMin = 2;
        }
        if(otherColor == blue)
        {
            böbbelMin = 3;
        }
        if(otherColor == yellow)
        {
            böbbelMin = 4;
        }

        GameEvents.current.onPlanetCollision += OnPassengerRemoval;

        böbbelLimit = Random.Range(böbbelMin, böbbelMax);

        for (int böbbelCount = 0; böbbelCount < böbbelLimit; böbbelCount++)
        {
            //erzeugt eine randomized Location
            Vector3 spawnLocation = newLocation(böbbelLimit, böbbelCount);


            //erzeugt ein böbbel
            GameObject go = Instantiate(böbbel, spawnLocation, Quaternion.identity);
            //zählt böbbel, und setzt den Winkel eins weiter

            go.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Count)];


            //print("Böbbels: " + böbbelCount);

            böbbelCollection.Add(go);


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



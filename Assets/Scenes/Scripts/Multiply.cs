using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class Multiply : MonoBehaviour
{
    public GameObject plönet;
    public GameObject böbbel;
    private int böbbelCount = 0;
    public int böbbelLimit;
    public List<GameObject> böbbelCollection = new List<GameObject>();
    int i = 0;


    void Start()
    {

        GameEvents.current.onPlanetCollision += OnPassengerRemoval;

        // Anzahl böbbels
        böbbelLimit = Random.Range(5, 10);

        for (böbbelCount = 0; böbbelCount <= böbbelLimit; böbbelCount++) 
        {
            //erzeugt eine randomized Location
            Vector3 spawnLocation = newLocation();


            //erzeugt ein böbbel
            GameObject go = Instantiate(böbbel, spawnLocation, Quaternion.identity);
            //zählt böbbel, und setzt den Winkel eins weiter
  
            i++;

            print("Böbbels: " + böbbelCount);

            böbbelCollection.Add(go);
            
        }

    }

    void Update()
    {

    }

    Vector3 newLocation()
    {
        float x;
        float y;
        
        //Ein Kreis ist 2mal PI, geteilt durch Anzahl der böbbel, i sorgt dafür dass es um einen xten Teil verschoben wird
        float zet =  i *  2 * Mathf.PI / böbbelLimit;
        
        //setzt die location, ausgehend vom Mittelpunkt vom zugewiesenen PLönet
        Vector3 spawnLocation = new Vector3(
            x = Mathf.Sin(zet) * 70 + plönet.transform.position.x,
            y = Mathf.Cos(zet) * 70 + plönet.transform.position.y,
            0);
        
        return spawnLocation;

      }

    private void OnPassengerRemoval(string nameOfPlanet)
    {
        //
        if (nameOfPlanet == this.plönet.name)
        {
            print("Anzah Böbbel:" + böbbelCollection.Count);
            for (int i = 0; i < böbbelCollection.Count; i++)
            {
                Destroy(böbbelCollection[i]);
                GameEvents.current.PassengerPickup(); //Triggert Event das Passenger Count erhöht
            }
            print("OnPassengerRemoval Triggered");
            
        }

    }
}


    
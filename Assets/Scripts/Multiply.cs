﻿using System.Collections;
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
    private GameObject halo;
    private SpriteRenderer haloRenderer;
    private bool mouseOnHalo = false;
    private bool mouseOnPlanet = false;
    public bool withinRange = false;
    public int passengerRed;
    public int passengerPink;
    public int passengerBlue;
    public int passengerYellow;
    private int böbbelLimit;
    public List<GameObject> böbbelCollection = new List<GameObject>();
    public List<Color> colors = new List<Color>();

    float offset = 0;


    void Start()
    {

        GameEvents.current.onPlanetCollision += OnPassengerRemoval;

        halo = plönet.transform.GetChild(0).gameObject;
        haloRenderer = halo.GetComponent<SpriteRenderer>();
        haloRenderer.enabled = false;

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
            Vector3 spawnLocation = newLocation(colors.Count, count, 0f);


            //erzeugt ein böbbel
            GameObject go = Instantiate(böbbel, spawnLocation, Quaternion.identity);

            //zählt böbbel, und setzt den Winkel eins weiter

            go.GetComponent<SpriteRenderer>().color = color;

            Animator anim = go.GetComponent<Animator>();

            float randomIdleStart = Random.Range(0, anim.GetCurrentAnimatorStateInfo(0).length); //Set a random part of the animation to start from
            anim.Play("passengerAnim", 0, randomIdleStart);

            float rotate = (count * 2 * Mathf.PI / colors.Count) * Mathf.Rad2Deg * -1;
            go.transform.rotation = Quaternion.Euler(0, 0, rotate);

            //print("Böbbels: " + böbbelCount);

            böbbelCollection.Add(go);
            count++;
        }


    }

    void Update()
    {

        offset += 0.25f * Time.deltaTime;

        for (int i = böbbelCollection.Count - 1; i >= 0; i--)
        {
            GameObject go = böbbelCollection[i];

            float thisOffset = offset;

            if (i % 2 != 0)
            {
                thisOffset *= -1;
            }
            go.transform.position = newLocation(böbbelCollection.Count, i, thisOffset);


            float rotate = (i * 2 * Mathf.PI / böbbelCollection.Count + thisOffset) * Mathf.Rad2Deg * -1;
            go.transform.rotation = Quaternion.Euler(0, 0, rotate);

        }
        mouseOnHalo = halo.GetComponent<HaloManager>().mouseOnHalo;
        UpdateHaloVisibility();
    }

    Vector3 newLocation(int limit, int böbbelNumber, float offset)
    {
        float x;
        float y;


        //Ein Kreis ist 2mal PI, geteilt durch Anzahl der böbbel, i sorgt dafür dass es um einen xten Teil verschoben wird
        float zet = böbbelNumber * 2 * Mathf.PI / limit +offset;

        //setzt die location, ausgehend vom Mittelpunkt vom zugewiesenen PLönet
        Vector3 spawnLocation = new Vector3(
            x = Mathf.Sin(zet) * this.plönet.transform.localScale.x * 4.3f + this.plönet.transform.position.x,
            y = Mathf.Cos(zet) * this.plönet.transform.localScale.x * 4.3f + this.plönet.transform.position.y,
            -böbbelNumber);

        return spawnLocation;

    }

    private void OnPassengerRemoval(string nameOfPlanet, int numberOfColorPassengers)
    {

        if (nameOfPlanet == this.plönet.name)
        {
            //Böbbel absetzen

            for (int i = böbbelCollection.Count - 1; i >= 0; i--)
            {
                if (böbbelCollection[i].GetComponent<SpriteRenderer>().color != this.plönet.GetComponent<SpriteRenderer>().color)
                {

                    //Triggert Event das Passenger Count erhöht

                    GameEvents.current.PassengerPickup(böbbelCollection[i].GetComponent<SpriteRenderer>().color);

                    Destroy(böbbelCollection[i]);
                    böbbelCollection.Remove(böbbelCollection[i]);

                }
            }
            
            int newCount = böbbelCollection.Count + numberOfColorPassengers;

            offset = 0;
            

            //neu Ordnen der Böbbel auf dem Planeten
            for (int i = 0; i < böbbelCollection.Count; i++)
            {
                GameObject existing = böbbelCollection[i];
                Vector3 spawnLocation = newLocation(newCount, i, 0f);

                float rotate = (i * 2 * Mathf.PI / newCount) * Mathf.Rad2Deg * -1;
                existing.transform.rotation = Quaternion.Euler(0, 0, rotate);
                existing.transform.position = spawnLocation;

            }

            for (int b = numberOfColorPassengers-1; b >=0 ; b--)
            {
                //erzeugt eine randomized Location
                Vector3 spawnLocation = newLocation(newCount, böbbelCollection.Count +b, 0);

                //erzeugt ein böbbel
                GameObject go = Instantiate(böbbel, spawnLocation, Quaternion.identity);
                //zählt böbbel, und setzt den Winkel eins weiter
                go.GetComponent<SpriteRenderer>().color = this.plönet.GetComponent<SpriteRenderer>().color;

                float rotate = (b  * 2 * Mathf.PI / newCount) * Mathf.Rad2Deg;
                go.transform.rotation = Quaternion.Euler(0, 0, rotate);

                böbbelCollection.Add(go);

                GameEvents.current.PassengerRelease(this.plönet.GetComponent<SpriteRenderer>().color);


            }
            //print("Böbbel auf Planet: " + böbbelCollection.Count);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "FuelRange")
        {
            withinRange = true;
            //Debug.Log(plönet.name + "is within Range");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "FuelRange")
        {
            withinRange = false;
        }
    }

    void OnMouseEnter()
    {
        mouseOnPlanet = true;
    }

    void OnMouseExit()
    {
        mouseOnPlanet = false;
    }

    void UpdateHaloVisibility()
    {
        if ((mouseOnPlanet == true || mouseOnHalo == true) && (withinRange == true))
        {
            haloRenderer.enabled = true;
        }
        else
        {
            haloRenderer.enabled = false;
        }
    }


    private void OnMouseDown()
    {
        if (withinRange == true)
        {
            GameEvents.current.ReactToFlyCommand(plönet);
        }
    }
}



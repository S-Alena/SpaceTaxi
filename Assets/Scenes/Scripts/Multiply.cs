﻿using System.Collections;
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
    int i = 0;

    void Start()
    {
        // Anzahl böbbels
        böbbelLimit = Random.Range(5, 10);
    }

    void Update()
    {
        if (böbbelCount < 10)
        {
            //erzeugt eine randomized Location
            Vector3 spawnLocation = newLocation();
            
            
            //erzeugt ein böbbel
            GameObject go = Instantiate(böbbel, spawnLocation, Quaternion.identity);
            //zählt böbbel, und setzt den Winkel eins weiter
            böbbelCount ++;
            i ++;
            
            print("Böbbels: " + böbbelCount);

        }
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
}


    
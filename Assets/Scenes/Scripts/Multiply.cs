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
    public GameObject böbbel;
    private int böbbelCount = 0;
    public int böbbelLimit;
    
    //soll dafür sorgen dass sie in einer area spawnen
    public Rect spawnArea;


    void Start()
    {
        // erzeuge zwischen 1 und 6 böbbels
        böbbelLimit = Random.Range(1, 6);
    }

    void Update()
    {  
        if(böbbelCount < böbbelLimit) {
            
            int a = 20;
            Vector3 spawnLocation = newLocation(a);
            

            GameObject go = Instantiate(böbbel, spawnLocation, Quaternion.identity);
            böbbelCount += 1;
                
            print("Böbbels: " +böbbelCount);

        }
    }
    
    Vector3 newLocation(int a)
    {
        Vector3 spawnLocation = new Vector3(
            Random.Range(spawnArea.x, spawnArea.y) + a, // x position
            Random.Range(spawnArea.width, spawnArea.height) + a, // y position
            0);

        return spawnLocation;
    }
}

    
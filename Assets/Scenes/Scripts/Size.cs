using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Size : MonoBehaviour
{
    //public Renderer renderer;
    internal static Vector3 mSize;
    
    void Start()
    {
        mSize = GetComponent<EdgeCollider2D>().bounds.size/2;
        //print("IDK Mate: " + mSize);
        
    }


    void Update()
    {
        

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloManager : MonoBehaviour
{
    
    public bool mouseOnHalo = false;
    private GameObject parentPlanet;
    void Start()
    {
        parentPlanet = this.gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnMouseEnter()
    {
        //Debug.Log("halo entered: " + this.gameObject.name);
        mouseOnHalo = true;
    }

    private void OnMouseExit()
    {
        mouseOnHalo = false;
        //Debug.Log("halo exit: " + this.gameObject.name);
    }

    private void OnMouseDown()
    {
        if (parentPlanet.GetComponent<Multiply>().withinRange == true)
        {
            GameEvents.current.ReactToFlyCommand(parentPlanet);
        }
    }
}

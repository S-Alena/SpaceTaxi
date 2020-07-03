using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Specialized;
//using System.Diagnostics;

public class TaxiManager : MonoBehaviour
{

    //relevant for movement
    public float speed = 4;
    private Vector3 targetPosition;
    private bool isMoving = false;
    //************

    //relevant for collision
    public Text endText;
    //************

    //relevant for fuel
    private float fuelRange = 3000;
    public GameObject fuelDisplay;
    private SpriteRenderer fuelDisplayRenderer; //reference to the circle sprite that shows the fuel

    private float oneFuelLoad = 400;
    private float maxFuel = 3000;

    private Vector3 positonBeforeMoving;
    private Vector3 positionAfterMoving;
    private float movingDistance;

    private Vector3 prevPosition;
    //************

    //relevant for end
    private bool end = false;

    //relevant for planning
    public List<GameObject> böbbelCollection = new List<GameObject>();
    public GameObject plänet;

    public static Color red = new Color(252 / 255f, 142 / 255f, 101 / 255f, 1);
    public static Color pink = new Color(0.8773585f, 0.08690815f, 0.4414128f, 1);
    public static Color blue = new Color(133 / 255f, 198 / 255f, 230 / 255f, 1);
    public static Color yellow = new Color(1, 228 / 255f, 184 / 255f, 1);


    // Start is called before the first frame update
    void Start()
    {
        endText.enabled = false;
        fuelDisplayRenderer = fuelDisplay.GetComponent<SpriteRenderer>(); // circle sprite is first child element of the SpaceTaxi
        fuelDisplayRenderer.enabled = true;
        targetPosition.Set(transform.position.x, transform.position.y, transform.position.z);

        GameEvents.current.onFuelPickup += AddFuel;
        GameEvents.current.onFlyCommand += UpdateTargetPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (end == true && Input.anyKeyDown)
        {
            GameEvents.current.RestartGame();
            PassengerCount.redPassengerCount = 0;
            PassengerCount.pinkPassengerCount = 0;
            PassengerCount.yellowPassengerCount = 0;
            PassengerCount.bluePassengerCount = 0;
            PassengerCount.transported = 0;
        }


        //MovementUpdateOld(); //Move SpaceTaxi (2)
        MovementUpdate();
        //CalculateMovingDistance(); //Calculate the distace the SpaceTaxi has moved with the values from 1 and 3 (4)
        FuelUpdate(); //Substract the movingDistance from the fuelRange and Update fuelDisplayRenderer (5)
        int i = 0;
        foreach (var böbbel in böbbelCollection)
        {
            //böbbelUpdate(böbbel, i);
            i++;
        }
        prevPosition = this.transform.position;
        if(isMoving == true)
        {
           this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        }
    }



    //**** Point & Click Movement ****

    /*
    void MovementUpdateOld() //called once per frame
    {
        positonBeforeMoving = transform.position;
        if (Input.GetMouseButtonDown(0))
        {
            SetTargetPosition();
        }
        if (isMoving == true)
        {
            Move();
        }
    }

    void SetTargetPosition()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;

        isMoving = true;
    }

    void Move()
    {
        //transform.rotation = Quaternion.LookRotation(targetPosition,Vector3.forward); //rotation of Player
        if(fuelRange >= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            fuelDisplay.transform.position = Vector3.MoveTowards(fuelDisplay.transform.position, targetPosition, speed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }else{
            endText.text = "Fuel Empty." + Environment.NewLine + "Press to Restart";
            endText.enabled = true;
            end = true;

        }
    }
    */


    void UpdateTargetPosition(Vector3 planetPosition)
    {
        this.targetPosition.Set(planetPosition.x, planetPosition.y, planetPosition.z);
    }

    void MovementUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
    //*************************************



 




    private void FuelUpdate()
    {
        if(isMoving == true)
        {
            float dist = Vector3.Distance(prevPosition, transform.position);
            Debug.Log("Travel Distance since last Frame: " + dist);
            fuelRange = fuelRange - (2*dist); 
        }
        if(fuelRange >= 0)
        {
            this.fuelDisplayRenderer.transform.localScale = new Vector3(fuelRange, fuelRange, 1);
        }
        if(fuelRange < 0)
        {
            endText.text = "Fuel Empty." + Environment.NewLine + "Press to Restart";
            endText.enabled = true;
            end = true;
        }
        //Debug.Log("Fuel Overlap: "+ fuelDisplay.GetComponent<FuelOverlap>().isOverlapping);
        if (!fuelDisplay.GetComponent<FuelOverlap>().isOverlapping)
        {
            endText.text = "No Planet reachable" + Environment.NewLine + "Press to Restart";
            endText.enabled = true;
            end = true;
        }
        
    }

    private void AddFuel(int numberOfFuelLoads)
    {
        fuelRange = fuelRange + (numberOfFuelLoads * oneFuelLoad);
        if (fuelRange > maxFuel)
        {
            fuelRange = maxFuel;
        }
        Debug.Log("Fuel: " + fuelRange);
    }





    //**** Planet Collision ****
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log("Collided with " + collision.gameObject.name);

        int passengerCount = 0;

        if (collision.gameObject.GetComponent<SpriteRenderer>().color == red)
        {
            passengerCount = PassengerCount.redPassengerCount;
        }
        if (collision.gameObject.GetComponent<SpriteRenderer>().color == pink)
        {
            passengerCount = PassengerCount.pinkPassengerCount;
        }
        if (collision.gameObject.GetComponent<SpriteRenderer>().color == blue)
        {
            passengerCount = PassengerCount.bluePassengerCount;
        }
        if (collision.gameObject.GetComponent<SpriteRenderer>().color == yellow)
        {
            passengerCount = PassengerCount.yellowPassengerCount;
        }
        if (collision.gameObject.name == "Home")
        {

            if (PassengerCount.yellowPassengerCount == 0 && PassengerCount.pinkPassengerCount == 0 && PassengerCount.bluePassengerCount == 0 && PassengerCount.redPassengerCount == 0)
            {
                endText.text = "You made it. " + Environment.NewLine + "Transported = " + PassengerCount.transported;
            }
            else
            {
                endText.text = "You didn't bring " + Environment.NewLine + "all the Passengers home!" + Environment.NewLine + "Press to Restart";
            }
            endText.enabled = true;
            end = true;

        }

        GameEvents.current.PlanetCollision(collision.gameObject.name, passengerCount);

    }
    //*************************************

}

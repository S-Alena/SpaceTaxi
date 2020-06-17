using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
    private float fuelRange = 120;
    private SpriteRenderer fuelDisplayRenderer; //reference to the circle sprite that shows the fuel

    private float oneFuelLoad = 20;
    private float maxFuel = 120;

    private Vector3 positonBeforeMoving;
    private Vector3 positionAfterMoving;
    private float movingDistance;
    //************

    //relevant for end
    private bool end = false;



    // Start is called before the first frame update
    void Start()
    {
        endText.enabled = false;
        fuelDisplayRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>(); // circle sprite is first child element of the SpaceTaxi
        fuelDisplayRenderer.enabled = true;

        GameEvents.current.onFuelPickup += AddFuel;
    }

    // Update is called once per frame
    void Update()
    {
        if (end == true && Input.anyKey)
        {
            GameEvents.current.RestartGame();
            PassengerCount.redPassengerCount = 0;
            PassengerCount.greenPassengerCount = 0;
            PassengerCount.yellowPassengerCount = 0;
            PassengerCount.bluePassengerCount = 0;
            PassengerCount.transported = 0;
        }

        //ReadPostitionBeforeMoving(); //Save Position in Variable before SpaceTaxi is moved (1)
        MovementUpdate(); //Move SpaceTaxi (2)
        //ReadPostitionAfterMoving(); //Save Position in Variable after SpaceTaxi is moved (3)
        //CalculateMovingDistance(); //Calculate the distace the SpaceTaxi has moved with the values from 1 and 3 (4)
        FuelUpdate(); //Substract the movingDistance from the fuelRange and Update fuelDisplayRenderer (5)
    }


    //**** Save Position in Variable before SpaceTaxi is moved ****
    private void ReadPostitionBeforeMoving()
    {
        positonBeforeMoving = transform.position;
        Debug.Log("positonBeforeMoving: " + positonBeforeMoving);
    }

    //**** Point & Click Movement ****
    void MovementUpdate() //called once per frame
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

    //*************************************



    //**** Save Position in Variable after SpaceTaxi is moved ****
    private void ReadPostitionAfterMoving()
    {
        positionAfterMoving = transform.position;
        Debug.Log("positonAfterMoving: " + positionAfterMoving);
    }

    //**** Calculate the Distance the SpaceTaxi has moved ****
    private void CalculateMovingDistance()
    {


        if(isMoving == true)
        {
            Vector3 positionToMoveTo = Vector3.MoveTowards(positonBeforeMoving, targetPosition, speed * Time.deltaTime);
            movingDistance = Vector3.Distance(positonBeforeMoving, positionToMoveTo);

            Debug.Log("movingDistance: " + movingDistance);
        }
        else
        {
            movingDistance = 0;
        }

    }




    private void FuelUpdate()
    {
        if(isMoving == true)
        {
            fuelRange = fuelRange - (66f * Time.deltaTime);
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

        Debug.Log("Collided with " + collision.gameObject.name);


        Color red = new Color(1, 0, 0, 1);
        Color green = new Color(0, 1, 0, 1);
        Color blue = new Color(0, 0, 1, 1);
        Color yellow = new Color(1, 1, 0, 1);
        int passengerCount = 0;

        if (collision.gameObject.GetComponent<SpriteRenderer>().color == red)
        {
            passengerCount = PassengerCount.redPassengerCount;
        }
        else if (collision.gameObject.GetComponent<SpriteRenderer>().color == green)
        {
            passengerCount = PassengerCount.greenPassengerCount;
        }
        else if (collision.gameObject.GetComponent<SpriteRenderer>().color == blue)
        {
            passengerCount = PassengerCount.bluePassengerCount;
        }
        else if (collision.gameObject.GetComponent<SpriteRenderer>().color == yellow)
        {
            passengerCount = PassengerCount.yellowPassengerCount;
        }
        else if (collision.gameObject.name == "Home")
        {

            if (PassengerCount.yellowPassengerCount == 0 && PassengerCount.greenPassengerCount == 0 && PassengerCount.bluePassengerCount == 0 && PassengerCount.redPassengerCount == 0)
            {
                endText.text = "You made it. " + Environment.NewLine + "Transported = " + PassengerCount.transported;
            }
            else
            {
                endText.text = "You didnt bring all the Passengers home!" + Environment.NewLine + "Press to Restart";
            }
            endText.enabled = true;
            end = true;

        }

        GameEvents.current.PlanetCollision(collision.gameObject.name, passengerCount);
    }
    //*************************************



}

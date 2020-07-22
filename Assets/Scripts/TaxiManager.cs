using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Specialized;
using UnityEngine.SceneManagement;
//using System.Diagnostics;

public class TaxiManager : MonoBehaviour
{

    //relevant for movement
    public float speed = 4;
    private Vector3 targetPosition;
    public bool isMoving = false;
    //************

    //relevant for collision
    public GameObject menu;
    public Text endText;
    //************

    //relevant for fuel
    public float fuelRange = 3000;
    private float fuelRangeAfterMoving = 3000;
    public GameObject fuelDisplay;
    private SpriteRenderer fuelDisplayRenderer; //reference to the circle sprite that shows the fuel

    public float oneFuelLoad = 400;
    public float maxFuel = 3000;

    private Vector3 positonBeforeMoving;
    private Vector3 positionAfterMoving;
    private float movingDistance;

    private Vector3 prevPosition;
    //************

    //relevant for interaction with planet
    private GameObject activePlanet;
    //************

    //relevant for end
    public bool end = false;
    private bool deathMessage;
    //************

    //relevant for planning
    public List<GameObject> böbbelCollection = new List<GameObject>();
    public GameObject plänet;

    public static Color red = new Color(252 / 255f, 142 / 255f, 101 / 255f, 1);
    public static Color pink = new Color(0.8773585f, 0.08690815f, 0.4414128f, 1);
    public static Color blue = new Color(133 / 255f, 198 / 255f, 230 / 255f, 1);
    public static Color yellow = new Color(1, 228 / 255f, 184 / 255f, 1);

    //relevant for animation
    float angle = 0;
    float targetAngle = 0;

    public GameObject beam;
    public AudioSource beamSFX;
    float beamTime;


    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        fuelDisplayRenderer = fuelDisplay.GetComponent<SpriteRenderer>(); // circle sprite is first child element of the SpaceTaxi
        fuelDisplayRenderer.enabled = true;
        targetPosition.Set(transform.position.x, transform.position.y, transform.position.z);

        GameEvents.current.onFuelPickup += AddFuel;
        GameEvents.current.onFlyCommand += UpdateTargetPosition;
        GameEvents.current.onFlyCommand += UpdateActivePlanet;
        GameEvents.current.onFlyCommand += UpdateFuelValues;
        beam.active = false;

        prevPosition = this.transform.position;




        fuelRangeAfterMoving = fuelRange;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            endText.text = "Pause Menu";
            if(menu.active == false)
            {
                menu.SetActive(true);
            }
            else
            {
                menu.SetActive(false);
            }
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
        //prevPosition = this.transform.position;
        if(isMoving == true)
        {
           this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;


            if (Mathf.Abs(angle) >= 3)
            {
                if(angle >= 0)
                {
                    angle -= 3;
                }else
                {
                    angle += 3;
                }
            }
            else
            {
                angle = 0;
            }
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        deathMessage = fuelDisplay.GetComponent<FuelManager>().deathMessage;
        if(deathMessage == true)
        {
            endText.text = "Fuel Empty." + Environment.NewLine + "Press to Restart";
            menu.SetActive(true);
            end = true;
        }

        //Debug.Log("currentTime: " + Time.timeSinceLevelLoad);
        //Debug.Log("beamTime: " + beamTime);

        if(Time.timeSinceLevelLoad - beamTime >  1f || isMoving)
        {
            beam.active = false;
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

    void MoveOld()
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


    void UpdateTargetPosition(GameObject planetPosition)
    {
        this.targetPosition.Set(planetPosition.transform.position.x, planetPosition.transform.position.y + 300, 0);
        isMoving = true;

        Vector2 targetDir = new Vector2(this.targetPosition.x - this.gameObject.transform.position.x, this.targetPosition.y - this.gameObject.transform.position.y);
        targetAngle = Vector2.Angle(Vector2.up, targetDir);

        if (targetAngle > 20f)
        {
            targetAngle = 20;
        }
            if (this.targetPosition.x > this.gameObject.transform.position.x)
        {
            targetAngle *= -1;
        }
    }

    void MovementUpdate()
    {
        if (isMoving == true)
        {
            Move();
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        fuelDisplay.transform.position = Vector3.MoveTowards(fuelDisplay.transform.position, targetPosition, speed * Time.deltaTime);
        

        if(Mathf.Abs(angle-targetAngle) >=3 )
        {
            if(angle > targetAngle)
            {
                angle -=3;
            }
            else
            {
                angle += 3;
            }
        }
        else
        {
            angle = targetAngle;
        }


        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);


        if (transform.position == targetPosition)
        {
            isMoving = false;
            removeRoundingErrorsInFuelDisplay();
            CollectPassengers();
        }
        
    }
    //*************************************







    private void UpdateFuelValues(GameObject planetEndPos)
    {


        Vector3 currentPosition = this.transform.position;
        float flyDistance = Vector3.Distance(targetPosition, currentPosition);
        //Debug.Log("................. flyDistance: " + flyDistance);
        //Debug.Log("fuelRange before calculation: " + fuelRange);
        fuelRangeAfterMoving = fuelRange - (flyDistance * 2);
        //Debug.Log("fuelRange after calculation: " + fuelRangeAfterMoving);
        if (fuelRangeAfterMoving < 0f)
        {
            fuelRangeAfterMoving = 0;
        }
    }

    private void FuelUpdate()
    {
        if(isMoving == true)
        {
            float dist = Vector3.Distance(prevPosition, transform.position);
            //Debug.Log("Travel Distance since last Frame: " + dist);
            fuelRange = fuelRange - (2*dist);
            prevPosition = this.transform.position;
        }
        if(fuelRange < 0f)
        {
            fuelRange = 0;
        } 
        this.fuelDisplayRenderer.transform.localScale = new Vector3(fuelRange, fuelRange, 1);
        //Debug.Log("FuelRange = " + fuelRange);

    }

    private void removeRoundingErrorsInFuelDisplay()
    {
        //Debug.Log("FuelRangeAfterMoving = " + fuelRangeAfterMoving);
        fuelRange = fuelRangeAfterMoving;
        //Debug.Log("FuelRange (after moving) = " + fuelRangeAfterMoving);
    }

    private void AddFuel(int numberOfFuelLoads)
    {
        fuelRange = fuelRange + (numberOfFuelLoads * oneFuelLoad);
        if (fuelRange > maxFuel)
        {
            fuelRange = maxFuel;          
        }
        //Debug.Log("FuelRange (after adding) = " + fuelRange);
    }




    //**** Böbbel Collection ****
    private void UpdateActivePlanet(GameObject planet)
    {
        activePlanet = planet;
        //Debug.Log("active Planet: " + activePlanet);
    }

    private void CollectPassengers()
    {
        //Debug.Log("passenger collection Started");
        if (activePlanet.name != "Home")
        {
            beam.active = true;
            beamSFX.Play();
        }
        beamTime = Time.timeSinceLevelLoad;



        int passengerCount = 0;

        if (activePlanet.GetComponent<SpriteRenderer>().color == red)
        {
            passengerCount = PassengerCount.redPassengerCount;
        }
        if (activePlanet.GetComponent<SpriteRenderer>().color == pink)
        {
            passengerCount = PassengerCount.pinkPassengerCount;
        }
        if (activePlanet.GetComponent<SpriteRenderer>().color == blue)
        {
            passengerCount = PassengerCount.bluePassengerCount;
        }
        if (activePlanet.GetComponent<SpriteRenderer>().color == yellow)
        {
            passengerCount = PassengerCount.yellowPassengerCount;
        }
        if (activePlanet.name == "Home")
        {

            if (PassengerCount.yellowPassengerCount == 0 && PassengerCount.pinkPassengerCount == 0 && PassengerCount.bluePassengerCount == 0 && PassengerCount.redPassengerCount == 0)
            {
                GameEvents.current.ActivateNextLevel();
                float score = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name,0);
                if(PassengerCount.transported > score)
                {
                    PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, PassengerCount.transported);
                    endText.text = "New Highscore! " + Environment.NewLine + "Transported = " + PassengerCount.transported;
                }
                else
                {
                    endText.text = "You made it. " + Environment.NewLine + "Transported = " + PassengerCount.transported;
                }

            }
            else
            {
                endText.text = "You didn't bring " + Environment.NewLine + "all the Passengers home!" + Environment.NewLine + "Press to Restart";
            }
            menu.SetActive(true);
            end = true;

        }

        GameEvents.current.PlanetCollision(activePlanet.name, passengerCount);
    }
    //*************************************


    //**** Planet Collision ****

    /*
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
    */
    //*************************************


}

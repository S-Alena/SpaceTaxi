using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Steuerung : MonoBehaviour
{
    public Vector3 velocity;
    public float fuel = 100;
    public GameObject fuelDisplay;
    public GameObject home;

    private List<GameObject> rocketBobbels = new List<GameObject>();

    public bool end = false;
    public Text endText;
    private Vector3 pos = new Vector3();
   
    void Start()
    {
        pos = Size.mSize;
        velocity = new Vector3(0, 0, 0);
        endText.enabled =false;

        rb = GetComponent<Rigidbody2D>();
    }

    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;


    private void Update()
    {
        if (end && Input.anyKey && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }

        if(!end){

            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            moveVelocity = moveInput.normalized * speed;

            //fuelDisplay.GetComponent<SpriteRenderer>().size = new Vector2(fuel,1);
            changeFuel();
            
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    //Treibstoffansicht entsprechend der geflogenen Strecke anpassen
    void changeFuel()
    {
        fuel -= 0.15f;
        fuelDisplay.transform.localScale = new Vector3(fuel, 1, fuel);
        if (fuel <= 0)
        {
            endText.text = "Game Over." + Environment.NewLine + "Press to Restart";
            endText.enabled = true;
            end = true;
        }
        print("fuelDisplay-Scale: " + fuelDisplay.transform.localScale.x);
    }

    //Collision Control Border
    void CheckPos()
    {
            if (transform.position.y > pos.y) //W
            {
               transform.position = new Vector3(transform.position.x, pos.y, 0);
            }

            if (transform.position.x < -pos.x) //A
            {
                transform.position = new Vector3(-pos.x, transform.position.y, 0);
            }
        
            if (transform.position.y < -pos.y) //S
            {
                transform.position = new Vector3(transform.position.x, -pos.y, 0);
                
            }
            if (transform.position.x > pos.x) //D
            {
                transform.position = new Vector3(pos.x, transform.position.y, 0);
            }
        }
        
        
            
    
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Debug.Log("Collided with " + collision.gameObject.name);


        Color red = new Color(1, 0, 0,1);
        Color green = new Color(0, 1, 0,1);
        Color blue = new Color(0, 0, 1,1);
        Color yellow = new Color(1, 1, 0,1);
        int passengerCount = 0;
 
            if (collision.gameObject.GetComponent<SpriteRenderer>().color == red) {
                passengerCount = PassengerCount.redPassengerCount;
            } else if (collision.gameObject.GetComponent<SpriteRenderer>().color == green)
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
        else
        {
            
            if (PassengerCount.yellowPassengerCount==0 && PassengerCount.greenPassengerCount == 0 && PassengerCount.bluePassengerCount == 0 && PassengerCount.redPassengerCount == 0)
            {
                endText.text = "You made it. " + Environment.NewLine + "Transported = " + PassengerCount.transported;
            }
            else
            {
                endText.text = "Game Over." + Environment.NewLine + "Press to Restart";
            }
            endText.enabled =true;
            end = true;

        }


        GameEvents.current.PlanetCollision(collision.gameObject.name, passengerCount);
        
        fuel += passengerCount * 50 ;

    }
}
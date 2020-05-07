using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Steuerung : MonoBehaviour
{

    public float speed = 10f;

    private List<string> collisionOrder = new List<string>();
    private List<string> correctOrder = new List<string>();

    public bool end = false;
    public GameObject deathText;
    public GameObject successText;


    void Start()
    {

        correctOrder.Add("Plänet (1)");
        correctOrder.Add("Plänet (2)");
        correctOrder.Add("Plänet (3)");
        correctOrder.Add("Plänet (4)");

        deathText.SetActive(false);
        successText.SetActive(false);
    }



    private void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
        }

        transform.position = pos;

  
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);

        if (collision.gameObject.name != "Background" && !collisionOrder.Contains(collision.gameObject.name))
        {
            collisionOrder.Add(collision.gameObject.name);
        }

        Debug.Log(collisionOrder[0]);
        Debug.Log(collisionOrder.Count);


        if (collisionOrder.Count == correctOrder.Count)
        {
            bool equal = true;

            for (int i = 0; i < correctOrder.Count; i++)
            {
                if (!correctOrder[i].Equals(collisionOrder[i]))
                {
                    equal = false;
                }
            }


            if (equal)
            {
                successText.SetActive(true);
            }
            else
            {
                deathText.SetActive(true);
            }
            end = true;
        }
    }
}
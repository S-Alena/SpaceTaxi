using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steuerung : MonoBehaviour
{
    public Vector3 velocity;
    private List<string> collisionOrder = new List<string>();

    private List<string> correctOrder = new List<string>();

    void Start()
    {
        velocity = new Vector3(0, 0, 0);
        correctOrder.Add("Plänet (1)");

        correctOrder.Add("Plänet (2)");

        correctOrder.Add("Plänet (3)");

        correctOrder.Add("Plänet (4)");
    }

    private int max = 5;
    private int min = -5;
    public Vector2 pos = new Vector2(380, 260);

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.W) && velocity.y <= max)
        {
            velocity += new Vector3(0, 2 * Time.deltaTime, 0);
        }

        if (Input.GetKeyDown(KeyCode.S) & velocity.y >= min)
        {
            velocity += new Vector3(0, -2 * Time.deltaTime, 0);
        }

        if (Input.GetKeyDown(KeyCode.A) && velocity.x >= min)
        {
            velocity += new Vector3(-2 * Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.D) && velocity.x <= max)
        {
            velocity += new Vector3(2 * Time.deltaTime, 0, 0);
        }
        transform.position += velocity;
        CheckPos();
    }

    void CheckPos()
    {
        if (transform.position.x > pos.x)
        {
            velocity = new Vector3(0, -50 * Time.deltaTime, 0);
        }
        if (transform.position.y > pos.y)
        {
            velocity = new Vector3(0, -50 * Time.deltaTime, 0);
        }
        if (transform.position.x < -pos.x)
        {
            velocity = new Vector3(0, 50 * Time.deltaTime, 0);
        }
        if (transform.position.y < -pos.y)
        {
            velocity = new Vector3(0, 50 * Time.deltaTime, 0);
        }
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
                Debug.Log("success");
            }
        }



    }
}
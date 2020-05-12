using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Steuerung : MonoBehaviour
{
    public Vector3 velocity;
    private List<string> collisionOrder = new List<string>();
    private List<string> correctOrder = new List<string>();
    private List<GameObject> rocketBobbels = new List<GameObject>();

    public bool end = false;
    public GameObject deathText;
    public GameObject successText; 

    void Start()
    {
        velocity = new Vector3(0, 0, 0);

        correctOrder.Add("Plänet (1)");
        correctOrder.Add("Plänet (2)");
        correctOrder.Add("Plänet (3)");
        correctOrder.Add("Plänet (4)");

        deathText.SetActive(false);
        successText.SetActive(false);
    }

    private int max = 2;
    private int min = -2;
    public Vector2 pos = new Vector2(380, 260);

    private void Update()
    {
        if (end && Input.GetMouseButtonDown(0))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }

        while (Input.GetKey(KeyCode.W) && velocity.y <= max)
        {
            velocity += new Vector3(0, Time.deltaTime, 0);
        }

        while (Input.GetKey(KeyCode.S) & velocity.y >= min)
        {
            velocity += new Vector3(0, - Time.deltaTime, 0);
        }

        while (Input.GetKey(KeyCode.A) && velocity.x >= min)
        {
            velocity += new Vector3(- Time.deltaTime, 0, 0);
        }

        while (Input.GetKey(KeyCode.D) && velocity.x <= max)
        {
            velocity += new Vector3( Time.deltaTime, 0, 0);
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            velocity = new Vector3(velocity.x, velocity.y, velocity.z)*0.95f;
        }
        transform.position += velocity;
        CheckPos();
    }

    void CheckPos()
    {
        if (transform.position.x > pos.x)
        {
            //velocity = new Vector3(velocity.x, velocity.y, 0);
            transform.position = new Vector3(pos.x, transform.position.y, 0);
        }
        if (transform.position.y > pos.y)
        {
            //velocity = new Vector3(0, 50 * Time.deltaTime, 0);
            transform.position = new Vector3(transform.position.x, pos.y, 0);
        }
        if (transform.position.x < -pos.x)
        {
            //velocity = new Vector3(0, 50 * Time.deltaTime, 0);
            transform.position = new Vector3(-pos.x, transform.position.y, 0);
        }
        if (transform.position.y < -pos.y)
        {
            //velocity = new Vector3(0, 50 * Time.deltaTime, 0);
            transform.position = new Vector3(transform.position.x, -pos.y, 0);
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
                successText.SetActive(true);
            }
            else
            {
                deathText.SetActive(true);
            }
            end = true;
        }

        GameEvents.current.PlanetCollision(collision.gameObject.name);

    }
}
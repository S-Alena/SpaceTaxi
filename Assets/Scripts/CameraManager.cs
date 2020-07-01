using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //relevant for zoom
    public float zoomSize;
    private float maxZoom = 1800f;
    private float minZoom = 800f;
    private Camera cam;

    //relevant for movement
    public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    //relevant for edge scrolling
    float edgeSize = 30f;

    public GameObject worldCenter;

    public float maxStray = 3000f; //maximal distance between camera position and taxi position


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //zoom
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(zoomSize > minZoom)
            {
                zoomSize -= 100;
                MoveCam();
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(zoomSize < maxZoom)
            {
                zoomSize += 100;
            }
        }

        cam.orthographicSize = zoomSize;


        //movement
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //edge scrolling
        /*
        if (Input.mousePosition.x > Screen.width - edgeSize)
        {
            moveInput.Set(1, 0);
        }
        if(Input.mousePosition.x < edgeSize)
        {
            moveInput.Set(-1, 0);
        }
        if(Input.mousePosition.y > Screen.height - edgeSize)
        {
            moveInput.Set(0, 1);
        }
        if (Input.mousePosition.y < edgeSize)
        {
            moveInput.Set(0, -1);
        }
        */

        moveVelocity = moveInput.normalized * moveSpeed;

    }

    private void FixedUpdate()
    {
        float strayDistance = Vector2.Distance(worldCenter.transform.position, (rb.position + moveVelocity * Time.fixedDeltaTime));

        Debug.Log("Distance: " + strayDistance);


        if(strayDistance < maxStray)
        {
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        }

    }


    void MoveCam()
    {
        Vector3 moveTowards = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float amount = 100f;

        // Calculate how much we will have to move towards the zoomTowards position
        float multiplier = (1.0f / cam.orthographicSize * amount);

        // Move camera
        transform.position += (moveTowards - transform.position) * multiplier;
    }
}

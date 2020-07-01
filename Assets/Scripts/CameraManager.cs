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

    public GameObject spaceShip;
    private Vector2 spaceShipPos;
    public Renderer shipRenderer;

    public float maxStray = 3000f; //maximal distance between camera position and taxi position


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        shipRenderer = spaceShip.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spaceShipPos = spaceShip.transform.position;


        //zoom
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(zoomSize > minZoom)
            {
                zoomSize -= 100;
 
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

        moveVelocity = moveInput.normalized * moveSpeed;

    }

    private void FixedUpdate()
    {
        float strayDistance = Vector2.Distance(spaceShipPos, (rb.position + moveVelocity * Time.fixedDeltaTime));

        Debug.Log("Distance: " + strayDistance);


        if(strayDistance < maxStray)
        {
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        }

    }
}

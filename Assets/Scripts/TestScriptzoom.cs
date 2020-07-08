using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptzoom : MonoBehaviour
{
    // Ortographic camera zoom towards a point (in world coordinates). Negative amount zooms in, positive zooms out
    // TODO: when reaching zoom limits, stop camera movement as well

    private Camera cam;
    private float minZoom = 800;
    private float maxZoom = 2000;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        // Scroll forward
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ZoomOrthoCamera(Camera.main.ScreenToWorldPoint(Input.mousePosition), 100);
        }

        // Scoll back
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            cam.orthographicSize += 100;
        }
    }

    void ZoomOrthoCamera(Vector3 zoomTowards, float amount)
    {
        // Calculate how much we will have to move towards the zoomTowards position
        float multiplier = (1.0f / cam.orthographicSize * amount);

        // Move camera
        transform.position += (zoomTowards - transform.position) * multiplier;

        // Zoom camera
        cam.orthographicSize -= amount;

        // Limit zoom
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
    }





}

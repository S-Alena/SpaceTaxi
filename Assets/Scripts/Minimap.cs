using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private bool isShowing = true;
    private Canvas map;
    // Start is called before the first frame update
    void Start()
    {
        map = this.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            isShowing = !isShowing;
            map.enabled = isShowing;
        }
    }
}

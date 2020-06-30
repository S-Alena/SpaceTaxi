using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelOverlap : MonoBehaviour
{
    int _overlaps;

    public bool isOverlapping
    {
        get
        {
            return _overlaps > 0;
        }
    }

    // Count how many colliders are overlapping this trigger.
    // If desired, you can filter here by tag, attached components, etc.
    // so that only certain collisions count. Physics layers help too.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Fuel Enter: "+collision.gameObject.name);
        if (collision.gameObject.name.Contains("Planet"))
        {
        
            _overlaps++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        Debug.Log("Fuel Exit: "+collision.gameObject.name);
        if (collision.gameObject.name.Contains("Planet"))
        {
            _overlaps--;
        }
    }
}

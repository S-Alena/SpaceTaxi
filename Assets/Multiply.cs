using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Multiply : MonoBehaviour
{
    public Transform prefab;

    void Start()
    { 
        CreateInstances();
    }

    public Transform[] yourPrefabVariations;
    public int instanceCount = 10;
    public Vector2 hey = new Vector2(100,100);
     
    
    private List<Transform> _instances = new List<Transform>();
 
    private void CreateInstances() {
        for (int i = 0; i < instanceCount; ++i) {
            // Randomly pick prefab variation:
            var yourPrefab = yourPrefabVariations[ Random.Range(0, yourPrefabVariations.Length) ];
            var instance = Instantiate(yourPrefab) as Transform;
            _instances.Add(instance);
 
            // Then adjust your position as needed.
            instance.localPosition = hey;
            hey += hey; 
        }
    }
    }

    
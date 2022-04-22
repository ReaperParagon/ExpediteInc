using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTracker : MonoBehaviour
{
    public int spawnItems = 3;
    public int increment = 1;

    public int remainingItems = 0;
    public bool canSpawn = true;

    private void OnEnable()
    {
        ObjectSpawner.OnObjectSpawn += CountObject;
        Container.OnObjectEnterContainer += ReduceObjectCount;
    }

    private void OnDisable()
    {
        ObjectSpawner.OnObjectSpawn -= CountObject;
        Container.OnObjectEnterContainer -= ReduceObjectCount;
    }

    private void FixedUpdate()
    {
        if (canSpawn && remainingItems <= 0)
            ItemsCompleted();
    }

    /// Functions ///

    private void ItemsCompleted()
    {
        // Start Spawning More
        ObjectSpawner.InvokeOnSpawnStart(spawnItems);
        spawnItems += increment;

        canSpawn = false;
    }

    private void CountObject()
    {
        remainingItems++;
        canSpawn = true;
    }

    private void ReduceObjectCount()
    {
        remainingItems--;
    }

}

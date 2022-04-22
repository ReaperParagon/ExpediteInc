using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public delegate void OnObjectSpawnEvent();
    public static event OnObjectSpawnEvent OnObjectSpawn;

    public delegate void OnFinishSpawningEvent();
    public static event OnFinishSpawningEvent OnFinishSpawn;

    public delegate void OnStartSpawningEvent(int numObjects);
    public static event OnStartSpawningEvent OnStartSpawn;

    [SerializeField]
    private LayerMask platformLayerMask;

    [SerializeField]
    private Collection spawnableObjects;

    [SerializeField]
    private float spawnDelay = 0.15f;

    private BoxCollider spawnArea;
    private IEnumerator spawnCoroutine;
    private GameObject nextObject;

    void OnEnable()
    {
        spawnArea = GetComponent<BoxCollider>();

        OnStartSpawn += SpawnSet;
    }

    private void OnDisable()
    {
        OnStartSpawn -= SpawnSet;

        if (spawnCoroutine == null)
            return;

        StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;
    }

    /// Functions ///

    public static void InvokeOnSpawnStart(int numObjs)
    {
        OnStartSpawn?.Invoke(numObjs);
    }

    private void SpawnSet(int numObjects)
    {
        spawnCoroutine = SpawnObjectCoroutine(numObjects);
        StartCoroutine(spawnCoroutine);
    }

    private Vector3 GetRandomPointInSpawnArea()
    {
        float x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        float z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

        return new Vector3(x, spawnArea.bounds.center.y, z);
    }

    private Vector3 GetSpawnPoint()
    {
        // Get a point inside the box collider
        Vector3 point = GetRandomPointInSpawnArea();

        // Raycast check against platform
        if (Physics.Raycast(point, Vector3.down, 100.0f, platformLayerMask))
            return point;

        return GetSpawnPoint();
    }

    private Quaternion GetRandomRotation()
    {
        float rotation = Random.Range(0.0f, 360.0f);

        return Quaternion.Euler(0.0f, rotation, 0.0f);
    }

    private void GetNextObject()
    {
        nextObject = spawnableObjects.GetObject();
    }

    private void SpawnObject()
    {
        if (nextObject == null)
            return;

        GameObject obj = Instantiate(nextObject, transform, true);
        obj.transform.position = GetSpawnPoint();
        obj.transform.rotation = GetRandomRotation();

        // Set Object Type and Color
        if (obj.TryGetComponent(out ObjectType objType))
            objType.SetRandomObjectColor();

        OnObjectSpawn?.Invoke();
    }

    /// Coroutines ///

    IEnumerator SpawnObjectCoroutine(int numObjs)
    {
        for (int i = 0; i < numObjs; i++)
        {
            GetNextObject();
            yield return new WaitForSeconds(spawnDelay);
            SpawnObject();
        }

        OnFinishSpawn?.Invoke();

        spawnCoroutine = null;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PackageManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnpointList;
    [SerializeField] private List<Package> packagePrefabList;

    private void CreatePackage(int spawnpoint, int size, float distance)
    {
        Instantiate(packagePrefabList[0], spawnpointList[spawnpoint].transform.position, Quaternion.identity).Initialize(distance, size);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CreatePackage(Random.Range(0, spawnpointList.Count), Random.Range(1, 4), 100);
        }
    }
}

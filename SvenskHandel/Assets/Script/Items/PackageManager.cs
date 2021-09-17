using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Items
{
    public class PackageManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> spawnpointList;
        [SerializeField] private List<Package> packagePrefabList;
        [SerializeField] private List<Package> packagesSpawned;

        private void CreatePackage(int spawnpoint, int id, int distanceIndex, int methodIndex)
        {
            Package pack;
            pack = Instantiate(packagePrefabList[0], spawnpointList[spawnpoint].transform.position, Quaternion.identity);
            pack.Initialize(id, distanceIndex, methodIndex);
            packagesSpawned.Add(pack);
        }

        private void Start()
        {
            GameManager.instance.GivePoints(100f);
        }

        private void Update()
        {
            PlayerInput();
            UpdatePackages();
        }

        private void PlayerInput()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CreatePackage(Random.Range(0, spawnpointList.Count), Random.Range(100, 1000), Random.Range(1, 4), Random.Range(1, 4));
            }
        }

        private void UpdatePackages()
        {
            foreach (var pack in packagesSpawned)
            {
                pack.UpdateMe();
            }
        }
    }
}

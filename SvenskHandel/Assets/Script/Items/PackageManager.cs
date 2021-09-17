using System;
using System.Collections;
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
        [SerializeField][Range(1, 10)] private float packageSpawnInterval;

        private void CreatePackage(int spawnpoint, int id, int distanceIndex, int methodIndex)
        {
            Package pack;
            pack = Instantiate(packagePrefabList[0], spawnpointList[spawnpoint].transform.position, spawnpointList[spawnpoint].transform.rotation);
            pack.Initialize(id, distanceIndex, methodIndex);
            packagesSpawned.Add(pack);
        }
        private void Start()
        {
            foreach(GameObject spawnPoint in spawnpointList)
            {
                spawnPoint.SetActive(false);
            }
            GameManager.instance.GivePoints(100f);
            StartCoroutine(SpawnPackages());
        }

        private void Update()
        {
            //PlayerInput();
            UpdatePackages();
        }

        private void PlayerInput()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CreatePackage(Random.Range(0, spawnpointList.Count), Random.Range(100, 1000), Random.Range(1, 4), Random.Range(1, 4));
            }
        }

        IEnumerator SpawnPackages()
        {
            yield return new WaitForSeconds(packageSpawnInterval);
            CreatePackage(Random.Range(0, spawnpointList.Count), Random.Range(100, 1000), Random.Range(1, 4), Random.Range(1, 4));

            if (GameManager.instance.GetTimer() > 0)
            {
                StartCoroutine(SpawnPackages());
            }
        }

        private void UpdatePackages()
        {
            /*foreach (var pack in packagesSpawned)
            {
                pack.UpdateMe();
                if (pack.GetPackageTimer() <= 0)
                {
                    Destroy(pack.gameObject);
                    packagesSpawned.Remove(pack);
                }
            }*/

            for (int i = 0; i < packagesSpawned.Count; i++)
            {
                packagesSpawned[i].UpdateMe();
                if (packagesSpawned[i].GetPackageTimer() <= 0)
                {
                    Destroy(packagesSpawned[i].gameObject);
                    packagesSpawned.RemoveAt(i);
                }
            }
        }

        public List<Package> GetPackageList()
        {
            return packagesSpawned;
        }
    }
}

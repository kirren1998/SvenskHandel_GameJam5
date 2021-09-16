using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Items
{
    public class PackageManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> spawnpointList;
        [SerializeField] private List<Package> packagePrefabList;

        private void CreatePackage(int spawnpoint, int id, int distanceIndex, int methodIndex)
        {
            Instantiate(packagePrefabList[0], spawnpointList[spawnpoint].transform.position, Quaternion.identity).Initialize( id, distanceIndex, methodIndex);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CreatePackage(Random.Range(0, spawnpointList.Count), Random.Range(100, 1000), Random.Range(1, 4), Random.Range(1, 4));
            }
        }
    }
}

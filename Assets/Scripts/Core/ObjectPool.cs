using System.Collections.Generic;
using UnityEngine;

namespace ShatteredIceStudio.Core.Objectpool
{
    public class ObjectPool : Singleton<ObjectPool>
    {
        private const int poolExpansionNumber = 5;

        /// <summary>
        /// Key: prefab, Value: pool list
        /// </summary>
        private Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();
        /// <summary>
        /// Key: spawned object, Value: prefab
        /// </summary>
        private Dictionary<GameObject, GameObject> spawnedObjects = new Dictionary<GameObject, GameObject>();

        public GameObject Spawn(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation, bool isActive = true)
        {
            GameObject obj = Spawn(prefab, position, isActive);
            obj.transform.parent = parent;
            obj.transform.rotation = rotation;
            return obj;
        }

        public GameObject Spawn(GameObject prefab, Transform parent, Vector3 position, bool isActive = true)
        {
            GameObject obj = Spawn(prefab, position, isActive);
            obj.transform.parent = parent;
            return obj;
        }

        public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, bool isActive = true)
        {
            GameObject obj = Spawn(prefab, position, isActive);
            obj.transform.rotation = rotation;
            return obj;
        }

        public GameObject Spawn(GameObject prefab, Vector3 position, bool isActive = true)
        {
            if (!pools.ContainsKey(prefab))
            {
                CreatePool(prefab);
            }
            else if (pools[prefab].Count == 0)
            {
                ExpandPool(prefab);
            }

            GameObject obj = pools[prefab].Dequeue();
            obj.transform.position = position;
            obj.SetActive(isActive);
            spawnedObjects.Add(obj, prefab);
            return obj;
        }

        public void Return(GameObject obj)
        {
            if (obj != null && spawnedObjects.ContainsKey(obj))
            {
                GameObject prefab = spawnedObjects[obj];
                spawnedObjects.Remove(obj);

                if (prefab != null && pools.ContainsKey(prefab))
                {
                    obj.SetActive(false);
                    obj.transform.parent = null;
                    pools[prefab].Enqueue(obj);
                }
                else
                {
                    Debug.LogError("Object is not a poolable object!");
                    Destroy(obj);
                }
            }
        }

        private void CreatePool(GameObject prefab)
        {
            Queue<GameObject> newPool = new Queue<GameObject>();
            pools.Add(prefab, newPool);
            ExpandPool(prefab);
        }

        private void ExpandPool(GameObject prefab)
        {
            Queue<GameObject> pool = pools[prefab];

            for (int x = 0; x < poolExpansionNumber; x++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }
        }
    }
}
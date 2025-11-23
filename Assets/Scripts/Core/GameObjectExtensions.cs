using ShatteredIceStudio.Core.Objectpool;
using UnityEngine;

public static class GameObjectExtensions
{
    public static T Spawn<T>(this GameObject obj, Transform parent, bool isActive = true) where T : Component
    {
        GameObject spawnedObject = ObjectPool.Instance.Spawn(obj, parent, Vector3.zero, isActive);
        return spawnedObject.GetComponent<T>();
    }

    public static T Spawn<T>(this GameObject obj, Transform parent, Vector3 position, bool isActive = true) where T : Component
    {
        GameObject spawnedObject = ObjectPool.Instance.Spawn(obj, parent, position, isActive);
        return spawnedObject.GetComponent<T>();
    }

    public static T Spawn<T>(this GameObject obj, Transform parent, Vector3 position, Quaternion rotation, bool isActive = true) where T : Component
    {
        GameObject spawnedObject = ObjectPool.Instance.Spawn(obj, parent, position, rotation, isActive);
        return spawnedObject.GetComponent<T>();
    }

    public static GameObject Spawn(this GameObject obj, Transform parent, bool isActive = true)
    {
        return ObjectPool.Instance.Spawn(obj, parent, Vector3.zero, isActive);
    }

    public static GameObject Spawn(this GameObject obj, Transform parent, Vector3 position, bool isActive = true)
    {
        return ObjectPool.Instance.Spawn(obj, parent, position, isActive);
    }

    public static GameObject Spawn(this GameObject obj, Transform parent, Vector3 position, Quaternion rotation, bool isActive = true)
    {
        return ObjectPool.Instance.Spawn(obj, parent, position, rotation, isActive);
    }

    public static void Recycle(this GameObject obj)
    {
        ObjectPool.Instance.Return(obj);
    }

    public static void Recycle<T>(this T obj) where T : Component
    {
        ObjectPool.Instance.Return(obj.gameObject);
    }
}
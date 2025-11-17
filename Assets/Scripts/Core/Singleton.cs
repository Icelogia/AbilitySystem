using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindFirstObjectByType(typeof(T), FindObjectsInactive.Exclude);
                if (System.Object.ReferenceEquals(instance, null))
                {
                    instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null || Instance == null)
        {
            instance = this as T;
        }
    }
}
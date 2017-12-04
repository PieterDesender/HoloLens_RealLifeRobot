using UnityEngine;

public class MySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //fields
    private static T _instance = null;
    private static object _lock = new object();
    private static bool _isApplicationQuitting = false;

    //methods
    public static T Instance()
    {
        if (_isApplicationQuitting)
        {
            Debug.LogWarning("Singleton Instacne of " + typeof(T) + " is already destroyes by the application");
            return null;
        }
        lock (_lock)
        {
            if (_instance == null)
            {
                //find object in scene
                _instance = FindObjectOfType(typeof(T)) as T;
                //if there are more than one in the scene
                if (FindObjectsOfType(typeof(T)).Length > 1)
                {
                    Debug.LogError("More than one instace of singleton " + typeof(T));
                    return _instance;
                }
                //if none exist
                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(T).ToString());
                    _instance = singleton.AddComponent<T>();
                    //flag to not be destroyed on load
                    DontDestroyOnLoad(singleton);
                }
            }
        }
        return _instance;
    }
}

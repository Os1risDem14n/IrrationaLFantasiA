using UnityEngine;

/// <summary>
/// Inherit from this base class to create a single scriptable object
/// public class ClassName : SingletonScriptableObject<ClassName>{}
/// Put this to create a menu item
/// [CreateAssetMenu(filename ="Class Name", menuName = "Scriptable Objects/Class Name")]
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonScriptableObject<T> : ScriptableObject where T: SingletonScriptableObject<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T[] assets = Resources.LoadAll<T>("");
                if (assets == null || assets.Length < 1)
                    throw new System.Exception("Couldn't find any singleton scriptable object instance in resources");
                else if (assets.Length > 1)
                    Debug.LogWarning("Multiple instances found");
                instance = assets[0];

            }
            return instance;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = (T)this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
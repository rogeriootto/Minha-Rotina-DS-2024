using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistObject : MonoBehaviour
{
    private static PersistObject instance;

    void Awake()
    {
        // Ensure that there is only one instance of this canvas (Singleton pattern)
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // This will keep the Canvas between scenes
        }
        else
        {
            Destroy(gameObject);  // If another instance exists, destroy the duplicate
        }
    }
}

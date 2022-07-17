using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeRestart : MonoBehaviour
{
    private SafeRestart Instance;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    void Awake()
    {
        if(Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

}

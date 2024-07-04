using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAwake : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

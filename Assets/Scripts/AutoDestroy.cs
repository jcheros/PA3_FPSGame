using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float destroyDelay = 15f;

    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}

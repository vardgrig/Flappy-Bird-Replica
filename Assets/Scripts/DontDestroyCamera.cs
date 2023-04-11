using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyCamera : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}

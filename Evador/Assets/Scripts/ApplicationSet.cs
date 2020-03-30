using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationSet : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60; 
    }
}

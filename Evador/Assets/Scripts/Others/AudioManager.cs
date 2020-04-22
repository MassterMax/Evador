using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager copy;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (copy == null)
            copy = this;
        else
            Destroy(gameObject);
    }
}

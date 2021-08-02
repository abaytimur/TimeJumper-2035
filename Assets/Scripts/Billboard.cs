 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // public Transform cam;

    private void Start()
    {
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position+ Camera.main.transform.forward);
    }
}

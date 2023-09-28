using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NefreteeBow : Bow
{
    

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }
    }
}

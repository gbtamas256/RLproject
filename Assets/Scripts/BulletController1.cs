using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController1 : MonoBehaviour
{
    public float speed = 1.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.forward * speed * Time.fixedDeltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController3 : MonoBehaviour
{
    public float speed = 1.5f;

    private void Start()
    {
        transform.Rotate(0, 135, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.forward * speed * Time.fixedDeltaTime);
    }
}

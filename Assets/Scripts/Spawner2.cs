using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{

    public GameObject BulletPrefab;

    // The number of frames to wait before spawning another Ball
    public int SpawnTime = 100;

    // We will use this to count how many frames have elapsed since the last Ball creation
    private int counter = 25;

    void FixedUpdate()
    {
        // If we can spawn the Bullet
        if (counter > SpawnTime)
        {
            // We generate a random x coordinate for the Ball
            float yPosition = Random.Range(-5, 5);

            // We instantiate a new Bullet at the generated axis while keeping the Y and Z axes constant
            var bullet = Instantiate(BulletPrefab, transform.position + new Vector3(-5, 1.05f, yPosition), Quaternion.identity, gameObject.transform);

            // We make sure that the Bullet is properly scaled
            bullet.transform.localScale = new Vector3(1, 1, 1);

            // We tell the program to destroy the Ball after 10 seconds have passed and reset the counter
            Destroy(bullet, 5);
            counter = 0;
        }
        else
        {
            counter++;
        }

    }
}

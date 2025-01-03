using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceControl : MonoBehaviour
{
    public float yBounceMultiplier = 0.3f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null )
        {
            Vector3 vel = rb.velocity;
            vel.y *= yBounceMultiplier;
            rb.velocity = vel;
        }
    }
}

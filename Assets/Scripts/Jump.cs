using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody rb;
    public float jumpPower = 5f;
    public float downPower = 50f;
    private bool isGround;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                var force = Vector3.up * (jumpPower * 0.5f) * jumpPower;
                rb.AddForce(force);
            }
            else
            {
                var force2 = (Vector3.down * downPower);
                rb.AddForce(force2, ForceMode.Impulse);
            }

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}

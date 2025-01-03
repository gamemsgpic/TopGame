using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTop : MonoBehaviour
{
    private Rigidbody rb;
    public bool isDeath { get; private set; } = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Die()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(Vector3.zero, ForceMode.Impulse);
        rb.AddTorque(Vector3.right * 50f, ForceMode.Impulse);
        isDeath = true;
    }
}

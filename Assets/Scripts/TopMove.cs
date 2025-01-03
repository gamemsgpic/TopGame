using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopMove : MonoBehaviour
{
    private Rigidbody body;
    private Vector3 dir;
    public float speed;

    private void Awake()
    {
        //gameObject.GetComponent<Rigidbody>();   // ���� ������Ʈ���� �����ϰ� �ִ�.
        body = GetComponent<Rigidbody>(); // �Լ��� ã�� �� �Լ��� �ν��Ͻ��� �������ִ� �Լ� �̰� �Ϲ���
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        dir = new Vector3(h, 0f, v);
        body.AddForce(dir * speed);
        
    }
}

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
        //gameObject.GetComponent<Rigidbody>();   // 게임 오브젝트에도 동일하게 있다.
        body = GetComponent<Rigidbody>(); // 함수를 찾아 그 함수의 인스턴스를 리턴해주는 함수 이게 일반적
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class RotateMonster : MonoBehaviour
//{
//    public float angluarSpeed = 10f;
//
//    // Update is called once per frame
//    private void FixedUpdate()
//    {
//        //rb.AddTorque(transform.up * angluarSpeed, ForceMode.Acceleration);
//        // 1번 픽시드 업데이트를 쓴다.
//        transform.Rotate(0f, angluarSpeed * Time.deltaTime, 0f);
//        // 2번
//        //rb.angularVelocity = new Vector3(0f, Mathf.Deg2Rad * angluarSpeed, 0f);
//        // 3번
//        //rb.
//    }
//}

public class RotateMonster : MonoBehaviour
{
    private Rigidbody rb;
    public float angularSpeed = 100f;  // 힘의 크기를 증가
  


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 100;
    }

    private void FixedUpdate()
    {
        // 회전 힘을 더 크게 적용

        rb.AddTorque(transform.up * angularSpeed);

    }
}
// 무게중심 낮추기, 그게 아니면 부딪쳤을 때 반환되는 값에서 Y값에 0.5 곱하기 등 
// 레퍼런스 찾아보자.
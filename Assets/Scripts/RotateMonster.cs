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
//        // 1�� �Ƚõ� ������Ʈ�� ����.
//        transform.Rotate(0f, angluarSpeed * Time.deltaTime, 0f);
//        // 2��
//        //rb.angularVelocity = new Vector3(0f, Mathf.Deg2Rad * angluarSpeed, 0f);
//        // 3��
//        //rb.
//    }
//}

public class RotateMonster : MonoBehaviour
{
    private Rigidbody rb;
    public float angularSpeed = 100f;  // ���� ũ�⸦ ����
  


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 100;
    }

    private void FixedUpdate()
    {
        // ȸ�� ���� �� ũ�� ����

        rb.AddTorque(transform.up * angularSpeed);

    }
}
// �����߽� ���߱�, �װ� �ƴϸ� �ε����� �� ��ȯ�Ǵ� ������ Y���� 0.5 ���ϱ� �� 
// ���۷��� ã�ƺ���.
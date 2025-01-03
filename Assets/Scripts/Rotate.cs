using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Rotate : MonoBehaviour
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

public class Rotate : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerTop pt;
    public float angularSpeed = 100f;  // ���� ũ�⸦ ����
    public Transform uiTransform;
    public Transform uiTransform2;

    public float rota { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pt = GetComponent<PlayerTop>();
        rb.maxAngularVelocity = 100;
    }

    private void FixedUpdate()
    {
        // ȸ�� ���� �� ũ�� ����
        if (!pt.isDeath)
        {
            rb.AddTorque(transform.up * angularSpeed);
            uiTransform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            uiTransform2.rotation = Quaternion.Euler(-90f, 0f, 0f);
            rota = rb.angularVelocity.y;
        }
    }
}
// �����߽� ���߱�, �װ� �ƴϸ� �ε����� �� ��ȯ�Ǵ� ������ Y���� 0.5 ���ϱ� �� 
// ���۷��� ã�ƺ���.
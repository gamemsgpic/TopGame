using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // 1�� ��� ������
        //rb.velocity = transform.forward * speed;
        //Destroy(gameObject, 3f);

    }

    public void Fire(Vector3 dir)
    {
        // 2�� ������
        //transform.forward = dir;
        //rb.velocity = dir * speed;
        //rb.velocity = transform.forward * speed;

        // 3�� ������
        transform.forward = dir;
        rb.AddForce(dir *  speed, ForceMode.Impulse);
        Destroy(rb, 3f);
    }
    //
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        Fire(Vector3.right);
    //    }
    //}
    //
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.name == "Player")
        //{
        //
        //}
        //
        //if (other.gameObject.tag == "Player")
        //{
        //
        //}
        // ��õ ����� ���� ����
        if (other.CompareTag("Player"))
        {
            // 1�� ���
            //other.SendMessage("OnDie");
            // 2�� ��� ��õ 
            var player = other.GetComponent<Player>();
            player.OnDie(false);
        }
    }
    //
    //private void OnTriggerStay(Collider other)
    //{
    //    
    //}
    //
    //private void OnTriggerExit(Collider other)
    //{
    //   // Debug.Log($"Exit: {other.gameObject.name}");
    //
    //}
}

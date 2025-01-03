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
        // 1번 방식 날리기
        //rb.velocity = transform.forward * speed;
        //Destroy(gameObject, 3f);

    }

    public void Fire(Vector3 dir)
    {
        // 2번 날리기
        //transform.forward = dir;
        //rb.velocity = dir * speed;
        //rb.velocity = transform.forward * speed;

        // 3번 날리기
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
        // 추천 비용이 가장 저렴
        if (other.CompareTag("Player"))
        {
            // 1번 방법
            //other.SendMessage("OnDie");
            // 2번 방법 추천 
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

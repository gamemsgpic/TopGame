using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    private Rigidbody rb;
    private Material mt;
    private bool isJump;
    public float speed = 10f;
    public float jumpForce = 1f;
    public float accelation = 20f;
    public bool onDie = true;
    private Vector3 movement;


    private void Awake()
    {
        //gameObject.GetComponent<Rigidbody>();   // 게임 오브젝트에도 동일하게 있다.
        rb = GetComponent<Rigidbody>(); // 함수를 찾아 그 함수의 인스턴스를 리턴해주는 함수 이게 일반적
        mt = GetComponent<Material>();
        isJump = false;
    }

    public void jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        Debug.Log("나 점프");
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        // 1번 이동 방식
        var targetvelocity = movement * speed;
        var delta = targetvelocity - rb.velocity;
        var force = delta * accelation;
        rb.AddForce(force);


        //var force = Vector3.zero;
        //var rote = Quaternion.Euler(0, 0, 0);
        //if (Input.GetKey(KeyCode.D))
        //{
        //    force.x = speed;
        //}
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    force.x = -speed;
        //}
        //else if (Input.GetKey(KeyCode.W))
        //{
        //    force.z = speed;
        //}
        //else if (Input.GetKey(KeyCode.S))
        //{
        //    force.z = -speed;
        //}
        //
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    force.y = jumpForce;
        //}
        //

        //rb.Move(force, rote); // 그 자리로 순간이동 하듯이 포지션 바뀜 아마 로테이션도 바뀌는듯
        //rb.velocity = force;
        //rb.AddForce(force);
    }

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        movement = new Vector3(h, 0f, v);
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnDie(true);
        }

        //    var h = Input.GetAxis("Horizontal");
        //    var v = Input.GetAxis("Vertical");
        //    Vector3 velocity = new Vector3(h, 0f, v);
        //
        //    transform.
        //    += velocity * Time.deltaTime;
    }

    public void OnDie(bool now)
    {
        gameObject.SetActive(now);
        gameManager.OnEndGame();
    }
}

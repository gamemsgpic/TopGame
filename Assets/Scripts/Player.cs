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
        //gameObject.GetComponent<Rigidbody>();   // ���� ������Ʈ���� �����ϰ� �ִ�.
        rb = GetComponent<Rigidbody>(); // �Լ��� ã�� �� �Լ��� �ν��Ͻ��� �������ִ� �Լ� �̰� �Ϲ���
        mt = GetComponent<Material>();
        isJump = false;
    }

    public void jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        Debug.Log("�� ����");
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        // 1�� �̵� ���
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

        //rb.Move(force, rote); // �� �ڸ��� �����̵� �ϵ��� ������ �ٲ� �Ƹ� �����̼ǵ� �ٲ�µ�
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public Bullet bulletPrefep;
    private Transform target;

    public float spawnRate = 1f;
    public float timer = 0f;


    private void Start()
    {
        // 1 �� ����Ƽ���� ����õ
        //var findGO = GameObject.Find("Player");
        //target = findGO.transform;

        // 2 �� player�� �ϳ� �־ Ư���� �����ϴٸ� ��� ����� �� ��
        var findGo = GameObject.FindWithTag("Player");
        target = findGo.transform;

        // 3 �� �� ����õ
        //var plater = FindObjectOfType<Plater>;
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnRate)
        {
            timer = 0f;

            var dir = target.position - transform.position;
            dir.y = 0f;
            dir.Normalize();

            Bullet newGo2 = Instantiate(bulletPrefep);
            newGo2.transform.position = transform.position;
            newGo2.Fire(dir);
        }

        // Test
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    // 1 �� �Ѿ˰���
        //    //Instantiate(bulletPrefep, transform.position, transform.rotation);
        //
        //    // 2 ��
        //    //Bullet newGo = Instantiate(bulletPrefep);
        //    //newGo.transform.position = transform.position;
        //    //newGo.transform.rotation = transform.rotation;
        //
        //    Bullet newGo2 = Instantiate(bulletPrefep);
        //    newGo2.transform.position = transform.position;
        //    newGo2.Fire(transform.forward);
        //}
    }
}

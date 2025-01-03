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
        // 1 번 유니티에서 비추천
        //var findGO = GameObject.Find("Player");
        //target = findGO.transform;

        // 2 번 player가 하나 있어서 특정이 가능하다면 사용 비용이 더 쌈
        var findGo = GameObject.FindWithTag("Player");
        target = findGo.transform;

        // 3 번 초 비추천
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
        //    // 1 번 총알공장
        //    //Instantiate(bulletPrefep, transform.position, transform.rotation);
        //
        //    // 2 번
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

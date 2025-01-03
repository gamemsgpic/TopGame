using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class Move : MonoBehaviour
{
    private Vector3 dir;
    public Transform[] children;
    public float speed = 50;
    public float rotationSpeed = 50;
    // Start is called before the first frame update
    private void Start()
    {
        children = GetComponentsInChildren<Transform>(); // Ʈ�������� �迭�� �ѱ�

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Transform t in children)
            {
                t.localPosition = Random.insideUnitSphere * 10f;
            }
        }
        // �̰� ŵ �ڵ������� �� �� ���� ��
        var v = Input.GetAxis("Vertical");
        transform.position += transform.forward * v * speed * Time.deltaTime;

        var h = Input.GetAxis("Horizontal"); 
        transform.rotation *= Quaternion.Euler(0, h * rotationSpeed * Time.deltaTime, 0);
    }
}

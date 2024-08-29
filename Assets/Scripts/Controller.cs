using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] Vector3 direction;
    void Start()
    {

    }

    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        // ������ ����ȭ
        direction.Normalize();

        // P = P0 + vt
        // Time.deltaTime : ������ �������� �Ϸ�� �� ����� �ð��� �� ������ ��ȯ�ϴ� ��
        transform.position = transform.position + direction * speed * Time.deltaTime;
    }
}

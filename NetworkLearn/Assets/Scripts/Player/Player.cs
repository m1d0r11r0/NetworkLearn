using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] float speed = 15;
    [SerializeField] float maxSpeed = 15;

    // Start is called before the first frame update
    void Start()
    {
    }



    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        if(Input.GetKey(KeyCode.Space))
        {
            TryToppleWall();
        }
    }

    private void PlayerMove()
    {

        float horizontalInput = 0.0f;
        float verticalInput = 0.0f;
        float Speed = 0.1f;

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput += Speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput -= Speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            verticalInput -= Speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput += Speed;
        }

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput).normalized * 0.2f;

        rb.velocity = Vector3.zero;
        if (rb.velocity.magnitude <= maxSpeed)
        {
            rb.AddForce(movement * speed);
        }
    }

    private void TryToppleWall()
    {
        // Raycast ���g�p���đO�����ɕǂƂ̐ڐG�����m
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2.0f))
        {
            // �ǂ�|�������i��: �ǂ�Transform��ύX����j
            Transform wallTransform = hit.transform;
            wallTransform.Rotate(Vector3.right, 90.0f); // 90�x��]�i���̏����j
        }
    }
}

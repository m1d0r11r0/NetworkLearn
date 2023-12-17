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
        // Raycast を使用して前方向に壁との接触を検知
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2.0f))
        {
            // 壁を倒す処理（例: 壁のTransformを変更する）
            Transform wallTransform = hit.transform;
            wallTransform.Rotate(Vector3.right, 90.0f); // 90度回転（仮の処理）
        }
    }
}

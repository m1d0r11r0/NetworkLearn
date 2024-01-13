using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushWall : MonoBehaviour
{
public enum FallDirection
    {
        Forward,
        Backward,
        Left,
        Right,
    }

    //�|������
    private FallDirection currentDirection=FallDirection.Forward;

    //�|��鑬�x
    public float fallSpeed=5f;

    void Update()
    {
        // ���L�[�̓��͂����m
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentDirection = FallDirection.Forward;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentDirection = FallDirection.Backward;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentDirection = FallDirection.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentDirection = FallDirection.Right;
        }

        // �I�u�W�F�N�g��|������
        FallObject();
    }

    void FallObject()
    {
        // �|�������ɉ����ăI�u�W�F�N�g���X����
        switch (currentDirection)
        {
            case FallDirection.Forward:
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-90f, 0f, 0f), Time.deltaTime * fallSpeed);
                break;
            case FallDirection.Backward:
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90f, 0f, 0f), Time.deltaTime * fallSpeed);
                break;
            case FallDirection.Left:
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, -90f), Time.deltaTime * fallSpeed);
                break;
            case FallDirection.Right:
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 90f), Time.deltaTime * fallSpeed);
                break;
        }
    }

}

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

    //倒れる方向
    private FallDirection currentDirection=FallDirection.Forward;

    //倒れる速度
    public float fallSpeed=5f;

    void Update()
    {
        // 矢印キーの入力を検知
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

        // オブジェクトを倒す処理
        FallObject();
    }

    void FallObject()
    {
        // 倒れる方向に応じてオブジェクトを傾ける
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

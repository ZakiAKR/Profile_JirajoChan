//�J�����R���g���[���[�X�N���v�g
//�쐬�ҁF�~�X䝗D
//�㉺120�����E���x����
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /*
    �ぁ( 0,1.0)
    ����(0,-1.0)
    �E��( 1.0,0)
    ����(-1.0,0)
     */
    private float moveX;
    private float moveY;
    
    public void OnMove(InputValue moveValue)
    {
        var movementVector = moveValue.Get<Vector2>();

        moveX = movementVector.x;
        moveY = movementVector.y;

        //Debug.Log($"X={moveX}Y={moveY}");
    }
    void Update()
    {
        if (moveX != 0 || moveY != 0)
        {
            Rotate(moveX, moveY);
        }
    }

    void Rotate(float _inputX, float _inputY)
    {
        //X����]
        var localAngle = transform.localEulerAngles;
        localAngle.x += _inputX;
        transform.localEulerAngles = localAngle;
        //Y����]
        var angle = transform.eulerAngles;
        angle.y += _inputY;
        transform.eulerAngles = angle;
    }
}

//カメラコントローラースクリプト
//作成者：梅森茉優
//上下120°左右限度無し
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /*
    上＝( 0,1.0)
    下＝(0,-1.0)
    右＝( 1.0,0)
    左＝(-1.0,0)
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
        //X軸回転
        var localAngle = transform.localEulerAngles;
        localAngle.x += _inputX;
        transform.localEulerAngles = localAngle;
        //Y軸回転
        var angle = transform.eulerAngles;
        angle.y += _inputY;
        transform.eulerAngles = angle;
    }
}

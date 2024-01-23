using UnityEngine;

// ジョイコンのABXYボタンで移動で移動

public class DebugMove : MonoBehaviour
{
    /// <summary>
    /// 移動スピードを取得
    /// </summary>
    [Tooltip("移動スピードをセット")]
    [SerializeField] float positionSpeed;

    void Update()
    {
        if(Input.GetKey(KeyCode.JoystickButton1))
        {
            transform.position += transform.forward * positionSpeed;
        }
        if(Input.GetKey(KeyCode.JoystickButton2))
        {
            transform.position -= transform.forward * positionSpeed;
        }
        if(Input.GetKey(KeyCode.JoystickButton0))
        {
            transform.position += transform.right * positionSpeed;
        }
        if(Input.GetKey(KeyCode.JoystickButton3))
        {
            transform.position -= transform.right * positionSpeed;
        }
    }
}
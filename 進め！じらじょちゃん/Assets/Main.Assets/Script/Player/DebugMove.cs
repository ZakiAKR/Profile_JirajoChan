using UnityEngine;

// �W���C�R����ABXY�{�^���ňړ��ňړ�

public class DebugMove : MonoBehaviour
{
    /// <summary>
    /// �ړ��X�s�[�h���擾
    /// </summary>
    [Tooltip("�ړ��X�s�[�h���Z�b�g")]
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
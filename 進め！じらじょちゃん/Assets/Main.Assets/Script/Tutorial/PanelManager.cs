using UnityEngine;

// B�{�^������������p�l�������

public class PanelManager : MonoBehaviour
{
    /// <summary>
    /// �G���A�ɓ�������
    /// </summary>
    bool flag = true;

    void OnEnable()
    {
        flag = true;
    }

    void Update()
    {
        if(Input.GetKeyDown (KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.B))
        {
            if(flag)
            {
                gameObject.SetActive(false);
                flag = false;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �t���[�����[�g���̕\��
// �쐬�ҁF�n����

public class FPSDisplay : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// �t���[�����[�g�����擾����ϐ�
    /// </summary>
    float _fps;

    #endregion ---Fields---

    void Update()
    {
        // Time.deltaTime�͑O��̃t���[������̌o�ߎ��ԁi�b�j��\���ϐ�
        _fps = 1.0f / Time.deltaTime;
        //Debug.Log(_fps.ToString("F2"));
    }
}
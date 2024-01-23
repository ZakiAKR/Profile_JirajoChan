using UnityEngine;

// �쐬�ҁF�n����
// mocopi�ł����킯�Ȃ��őO�ɐi�ނ��Ƃ�h�~
// ���̃\�[�X���Ȃ��Ă�mocopi�ł��������đO�ɐi�ނ��Ƃ��o�������߁A�v

public class PlayerAttack : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// ���I�u�W�F�N�g�擾
    /// </summary>
    [Tooltip("���̃I�u�W�F�N�g���A�^�b�`")]
    [SerializeField] GameObject _head;

    /// <summary>
    /// ����I�u�W�F�N�g�擾
    /// </summary>
    [Tooltip("����̃I�u�W�F�N�g���A�^�b�`")]
    [SerializeField] GameObject _lhand;

    /// <summary>
    /// ����I�u�W�F�N�g�擾
    /// </summary>
    [Tooltip("�E��̃I�u�W�F�N�g���A�^�b�`")]
    [SerializeField] GameObject _rhand;

    /// <summary>
    /// �ϋq�̃��W�b�g�{�f�B�擾
    /// </summary>
    [Tooltip("�ϋq�̃I�u�W�F�N�g���A�^�b�`")]
    [SerializeField] Rigidbody _rigidbody;

    /// <summary>
    /// �ϋq�̃��W�b�g�{�f�B��������͈�
    /// </summary>
    [Tooltip("�ϋq�̃��W�b�g�{�f�B�������鐔�l")]
    [SerializeField] float _attackRange;

    /// <summary>
    /// ���Ǝ��Y���̋���
    /// </summary>
    float _distance;

    #endregion ---Fields---

    #region ---Methods---

    void Update()
    {
        // _distance = ����Y���W - �E���Y���W
        _distance = _head.transform.position.y - _rhand.transform.position.y;

        // �ϋq��RigidbodyConstraints��S�ăI�t
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        //Debug.Log(_lhand.transform.position.y);
        //Debug.Log(_rhand.transform.position.y);
        //Debug.Log(_head.transform.position.y);
        //Debug.Log(_distance);

        // ���Ǝ�̋������w�肵�����l��菬����������
        if ( _distance < _attackRange)
        {
            // �ϋq�̉�]��Y�����Œ�
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation
                | RigidbodyConstraints.FreezePositionY;

            //Debug.Log("Position����");
        }
    }
    #endregion ---Methods---
}
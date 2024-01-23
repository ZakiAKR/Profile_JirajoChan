using UnityEngine;

// �ϋq�̋���
// �쐬�ҁF�n����

public class NPCManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// �I�u�W�F�N�g�̍��W���擾
    /// </summary>
    Vector3 _pos;

    /// <summary>
    /// ���x���擾�@���݂�Vector3(0, 0, 0)
    /// </summary>
    Vector3 _velocity = Vector3.zero;

    /// <summary>
    /// _pos�֓��B����܂ŉ��b�̕ϐ��B�l���������قǁA_target �ɑ������B
    /// </summary>
    [SerializeField] float smoothTime;

    /// <summary>
    /// ValueSettingManager�֎Q�Ƃ��邽�߂̕ϐ�
    /// </summary>
    //[SerializeField] ValueSettingManager _setting;

     Animator _audienceAnim;

    #endregion ---Fields---

    #region ---Methods---

    /// <summary>
    /// �������̕ϐ�
    /// </summary>
    void Start()
    {
        // �����ʒu��ۑ�
        _pos = transform.position;
        // �l���Q�Ƃ������̂�ۑ�����
        //smoothTime = _setting.smoothTime;

        _audienceAnim = GetComponent<Animator>();
    }

    void Update()
    {
        // ���݈ʒu�����A���^�C���Ŏ擾
        Vector3 _current = transform.position;
        // SmoothDamp(���݈ʒu, �ړI�n, ���݂̑��x, _target �֓��B����܂ł̂����悻�̎��ԁB�l���������قǁA_target �ɑ������B)
        transform.position = Vector3.SmoothDamp(_current, _pos, ref _velocity, smoothTime);

        if(FadeController.FadeTimeOver())
        {
            _audienceAnim.Play("reaction");
        }
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        _audienceAnim.Play("hit");
    //        Debug.Log("hit");
    //    }
    //}
    #endregion ---Methods---
}
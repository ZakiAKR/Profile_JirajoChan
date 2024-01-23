using UnityEngine;

// �`���[�g���A���̃t�F�[�Y�̊Ǘ��B

public class TutorialManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// �t�F�[�Y�p�l���擾
    /// </summary>
    [Tooltip("�t�F�[�Y�̃p�l�����A�^�b�`")]
    [SerializeField] GameObject[] _phase;

    /// <summary>
    /// ���Ⴊ�ݔ���̃I�u�W�F�N�g�擾
    /// </summary>
    [Tooltip("bodyDownobj���A�^�b�`")]
    [SerializeField] GameObject _bodyDownobj;

    /// <summary>
    /// ������������̃I�u�W�F�N�g���擾
    /// </summary>
    [Tooltip("kakiwakeobj���A�^�b�`")]
    [SerializeField] GameObject _kakiwakeobj;

    /// <summary>
    /// ������������̃I�u�W�F�N�g���擾
    /// </summary>
    [Tooltip("kakiwakeobj2���A�^�b�`")]
    [SerializeField] GameObject _kakiwakeobj_2;

    /// <summary>
    /// �ϋq�I�u�W�F�N�g���擾
    /// </summary>
    [Tooltip("�ϋq�I�u�W�F�N�g���A�^�b�`")]
    [SerializeField] GameObject _mobobj;

    /// <summary>
    /// �t�F�[�h�A�j���[�^�[�擾
    /// </summary>
    [Tooltip("�t�F�[�h�p�l�����A�^�b�`")]
    [SerializeField] Animator _fadeAnimator;

    /// <summary>
    /// �t�F�[�Y�̒l
    /// </summary>
    [HideInInspector] public int _phaseCount;

    /// <summary>
    /// �A�����͖h�~
    /// </summary>
    bool _pushFlag = false;

    #endregion ---Fields---

    #region ---Methods---

    void Update()
    {
        // 0�`3�ɐ���
        _phaseCount = Mathf.Clamp(_phaseCount, 0, 3);

        // phaseCount�̃p�l����\��
        _phase[_phaseCount].SetActive(true);

        // �{�^�����͂Ńt�F�[�Y���i��
        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.A))
        {
            if (_pushFlag == false)
            {
                _phase[_phaseCount].SetActive(false);
                _phaseCount++;
                _pushFlag = true;
            }
        }
        else
        {
            _pushFlag = false;
        }
        switch(_phaseCount)
        {
            case 1:
                _kakiwakeobj.SetActive(true);
                _kakiwakeobj_2.SetActive(true);
                _mobobj.SetActive(true);
                break;
            case 2:
                _kakiwakeobj.SetActive(false);
                _kakiwakeobj_2.SetActive(false);
                _mobobj.SetActive(false);
                _bodyDownobj.SetActive(true);
                break;
            case 3:
                _fadeAnimator.Play("FadeOut");
                break;
                default:
                break;
        }
    }
    #endregion ---Methods---
}
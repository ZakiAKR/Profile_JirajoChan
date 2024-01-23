using UnityEngine;
using TMPro;

//�@���Ⴊ�񂾎��̔���

public class BodyDownManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// ���Ⴊ�ގ��Ԏ擾
    /// </summary>
    [Tooltip("���Ⴊ�ގ��ԕb")]
    [SerializeField] int _clearCount = 3;

    /// <summary>
    /// �o�ߎ��Ԃ�\������Text�^
    /// </summary>
    [Tooltip("�o�ߎ��Ԃ�\������Text")] 
    [SerializeField] TextMeshProUGUI _countTimeText;

    /// <summary>
    /// �p�l���I�u�W�F�N�g�擾
    /// </summary>
    [Tooltip("BodyDownPanel�A�^�b�`")]
    [SerializeField] GameObject _bodyDownPanel;

    /// <summary>
    /// �`���[�g���A�����Ⴊ�݃p�l���擾
    /// </summary>
    [Tooltip("BodyDownPanel2�A�^�b�`")]
    [SerializeField] GameObject _panel;

    /// <summary>
    /// AudioManager�Q�Ƃ��邽�߂̕ϐ�
    /// </summary>
    [SerializeField] AudioManager _audioManager;

    /// <summary>
    /// TutorialManager�Q�Ƃ��邽�߂̕ϐ�
    /// </summary>
    [SerializeField] TutorialManager _tutorialManager;

    /// <summary>
    /// �o�ߎ��Ԃ��i�[
    /// </summary>
    float _count;

    /// <summary>
    /// ���E�ɓ�������������Ƃ�
    /// </summary>
    bool _active = true;

    /// <summary>
    /// ������I����������肷��bool
    /// </summary>
    bool isAudioEnd;

    /// <summary>
    /// SE����x�����Đ�������bool
    /// </summary>
    bool SEflag = true;

    #endregion ---Fields---

    #region ---Methods---

    void OnEnable()
    {
        _audioManager.PlaySESound(SEData.SE.BodyDownVoice);
        _panel.gameObject.SetActive(true);
    }

    void Update()
    {
        // ���E����O��Ă��玞�ԉ��Z
        if (_active)
        {
            _count += Time.deltaTime;
        }
        else
        {
            _count = 0;
        }

        // �o�ߎ��ԕ\��
        _countTimeText.text = _count.ToString("F1");

        // �w�肵���񐔈ȏ㑫���ݏo�����炸����OK�\��
        if (_count > _clearCount)
        {
            _countTimeText.text = "OK";
        }

        // OK�T�E���h��炷
        if (SEflag && _count > _clearCount)
        {
            _audioManager.PlaySESound(SEData.SE.Correct);
            SEflag = false;
            isAudioEnd = true;
        }

        // SE����I�������
        if (_audioManager.CheckPlaySound(_audioManager.seAudioSource) && isAudioEnd)
        {
            _bodyDownPanel.SetActive(false);
            gameObject.SetActive(false);
            _tutorialManager._phaseCount++;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        _active = false;
    }
    void OnTriggerExit(Collider other)
    {
        _active = true;
    }
    #endregion ---Methods---
}
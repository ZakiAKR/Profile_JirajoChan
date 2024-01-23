using UnityEngine;
using TMPro;

// �쐬�ҁF�n����
// �����݁i�����j�t�F�[�Y�̐���

public class WalkManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// �����݂̐����񐔎擾
    /// </summary>
    [Tooltip("�����݂̐�����")]
    [SerializeField] int _clearCount = 3;

    /// <summary>
    /// �����݂����񐔂�\������e�L�X�g�ϐ�
    /// </summary>
    [Tooltip("�����݂����񐔂�\������e�L�X�g")]
    [SerializeField] TextMeshProUGUI _countText;

    /// <summary>
    /// �p�l���I�u�W�F�N�g�擾
    /// </summary>
    [Tooltip("WalkPanel�A�^�b�`")]
    [SerializeField] GameObject _walkPanel;

    /// <summary>
    /// �`���[�g���A���p�l���\��
    /// </summary>
    [Tooltip("Walk�`���[�g���A���p�l�����A�^�b�`")]
    [SerializeField] GameObject _panel;

    /// <summary>
    /// AudioManager�Q�Ƃ��邽�߂̕ϐ�
    /// </summary>
    [SerializeField] AudioManager _audioManager;

    /// <summary>
    /// StandStill�Q�Ƃ��邽�߂̕ϐ�
    /// </summary>
    [SerializeField] StandStillManager _standStill;

    /// <summary>
    /// TutorialManager�Q�Ƃ��邽�߂̕ϐ�
    /// </summary>
    [SerializeField] TutorialManager _tutorialManager;

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
        // �{�C�X�Đ�
        _audioManager.PlaySESound(SEData.SE.WalkVoice);
        _panel.gameObject.SetActive(true);
    }

    void Update()
    {
        // �����݂����񐔂�Text�\��
        _countText.text = _standStill.walkCount.ToString();

        // �w�肵���񐔈ȏ㑫���ݏo�����炸����OK�\��
        if(_standStill.walkCount > _clearCount)
        {
            _countText.text = "OK";
        }

        // OK�T�E���h��炷
        if (SEflag && _standStill.walkCount > _clearCount)
        {
            _audioManager.PlaySESound(SEData.SE.Correct);
            SEflag = false;
            isAudioEnd = true;
        }

        // SE����I�������
        if (_audioManager.CheckPlaySound(_audioManager.seAudioSource) && isAudioEnd)
        {
            _walkPanel.SetActive(false);
            _tutorialManager._phaseCount++;
        }
    }
    #endregion ---Methods---
}
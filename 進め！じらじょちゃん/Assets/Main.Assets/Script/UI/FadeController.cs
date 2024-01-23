using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class FadeController : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// UITimer�擾
    /// </summary>
    [SerializeField] UITimer _timer;

    /// <summary>
    /// �x������AroundGuardsmanController�擾
    /// </summary>
    [Tooltip("����x�������A�^�b�`")]
    [SerializeField] AroundGuardsmanController _controller;

    /// <summary>
    /// �x������NavMeshAgent�擾
    /// </summary>
    [Tooltip("����x�������A�^�b�`")]
    [SerializeField] NavMeshAgent guardsman;

    /// <summary>
    /// AudioManager�擾
    /// </summary>
    [SerializeField] AudioManager _audio;

    /// <summary>
    /// PlayerWalkManager�擾
    /// </summary>
    [SerializeField] PlayerWalkManager _walk;

    /// <summary>
    /// countdownText�擾
    /// </summary>
    [Tooltip("countdownText�A�^�b�`")]
    [SerializeField] TextMeshProUGUI _countdownText;

    /// <summary>
    /// countdownImage�擾
    /// </summary>
    [Tooltip("countdownImage�A�^�b�`")]
    [SerializeField] Image _countdownImage;

    /// <summary>
    /// �t�F�[�h�p�l���擾
    /// </summary
    [Tooltip("�t�F�[�h�p�l���A�^�b�`")]
    [SerializeField] Image _fadePanel;

    /// <summary>
    /// �t�F�[�h�A�j���[�^�[�擾
    /// </summary>
    [Tooltip("�t�F�[�h�A�j���[�^�[�A�^�b�`")]
    [SerializeField] Animator _animator;

    /// <summary>
    /// ���A�j���[�V�����Đ�
    /// </summary>
    [Tooltip("�܂����f���A�^�b�`")]
    [SerializeField] Animator _maoAnim;

    /// <summary>
    /// ���A�j���[�V�����Đ�
    /// </summary>
    [Tooltip("��񃂃f���A�^�b�`")]
    [SerializeField] Animator _ranAnim;

    /// <summary>
    /// �J�E���g�_�E��
    /// </summary>
    static float _countdown = 4f;

    /// <summary>
    /// �o�ߎ���
    /// </summary
    int _count;

    #endregion ---Fields---

    #region ---Methods---

    void Start()
    {
        _timer.enabled = false;
        _fadePanel.enabled = true;
        guardsman.enabled = false;
        _controller.enabled = false;
        _countdown = 4f;

        _walk._isActive = false;

        _audio.PlaySESound(SEData.SE.Buzzer);
    }

    void Update()
    {
        if (_audio.CheckPlaySound(_audio.seAudioSource))
        {
            _animator.Play("FadeIn");
        }
    }

    /// <summary>
    /// �A�j���[�V�����C�x���g�p�̊֐�
    /// </summary>
    public void Fade()
    {
        StartCoroutine("Color_FadeIn");
    }

    /// <summary>
    /// �X�^�[�g�O�̃J�E���g�_�E��
    /// </summary>
    IEnumerator Color_FadeIn()
    {
        _countdownText.gameObject.SetActive(true);
        _countdownImage.gameObject.SetActive(true);

        while (_countdown > 0)
        {
            // �J�E���g�_�E���v�Z�A�\��
            _countdown -= Time.deltaTime;
            _countdownImage.fillAmount = _countdown % 1.0f;
            _count = (int)_countdown;
            _countdownText.text = _count.ToString();

            // �J�E���g�_�E�����I�������
            if (FadeTimeOver())
            {
                _timer.enabled = true;
                guardsman.enabled = true;
                _controller.enabled = true;

                // �A�j���[�V�����Đ�
                _maoAnim.Play("dance");
                _ranAnim.Play("dance");

                _countdownText.gameObject.SetActive(false);
                _countdownImage.gameObject.SetActive(false);

                _audio.PlayBGMSound(BGMData.BGM.Main);

                _walk._isActive = true;

                yield break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// �J�E���g�_�E�����I�������Ԃ�
    /// </summary>
    public static bool FadeTimeOver()
    {
        return _countdown <= 0;
    }
    #endregion ---Methods---
}
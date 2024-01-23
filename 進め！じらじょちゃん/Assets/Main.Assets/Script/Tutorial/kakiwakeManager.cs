using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class kakiwakeManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// �J�E���g�̃e�L�X�g
    /// </summary>
    [Tooltip("�J�E���g����e�L�X�g���A�^�b�`")]
    [SerializeField] TextMeshProUGUI _Text;

    /// <summary>
    /// AudioManager�擾
    /// </summary>
    [SerializeField] AudioManager _audioManager;

    /// <summary>
    /// �G���A�ɓ�������
    /// </summary>
    [SerializeField] TutorialManager _tutorialManager;

    /// <summary>
    /// kakiwakepanel�擾
    /// </summary>
    [Tooltip("kakiwake�p�l�����A�^�b�`")]
    [SerializeField] GameObject _kakiwakePanel;

    /// <summary>
    /// TutoerialSystem�I�u�W�F�N�g�擾
    /// </summary>
    [Tooltip("TutorialSystem�I�u�W�F�N�g���A�^�b�`")]
    [SerializeField] GameObject _tutorialSystem;

    /// <summary>
    /// vcam�擾
    /// </summary>
    [Tooltip("vcam���A�^�b�`")]
    [SerializeField] GameObject _vcam;

    /// <summary>
    /// �p�l��2���擾
    /// </summary>
    [Tooltip("�p�l��2���A�^�b�`")]
    [SerializeField] GameObject _panel;

    /// <summary>
    /// �G���A�ɓ�������N���A�ł���l��
    /// </summary>
    [Tooltip("�G���A�ɓ�������N���A�ł���l��")]
    [SerializeField] int _hitoCount;

    /// <summary>
    /// �G���A�ɓ������l�𐔂��郊�X�g
    /// </summary>
    public static List<GameObject> hitolist = new List<GameObject>();

    /// <summary>
    /// ������I�������
    /// </summary>
    bool isAudioEnd;

    /// <summary>
    /// ��x�������炷
    /// </summary>
    bool SEflag = true;

    #endregion ---Fields---

    #region ---Methods---

    void OnEnable()
    {
        _audioManager.PlaySESound(SEData.SE.KakiwakeVoice);
        _panel.gameObject.SetActive(true);
        _vcam.SetActive(true);
    }

    void Start()
    {
        _tutorialManager = _tutorialSystem.GetComponent<TutorialManager>();
    }

    void Update()
    {
        // Text�\��
        _Text.text = hitolist.Count.ToString();

        if(hitolist.Count > _hitoCount)
        {
            _Text.text = "OK";
        }
        if (SEflag && hitolist.Count > _hitoCount)
        {
            _audioManager.PlaySESound(SEData.SE.Correct);
            SEflag = false;
            isAudioEnd = true;
        }

        // ������I������玟�̃t�F�[�Y��
        if (_audioManager.CheckPlaySound(_audioManager.seAudioSource) && isAudioEnd)
        {
            _kakiwakePanel.SetActive(false);
            gameObject.SetActive(false);
            _tutorialManager._phaseCount++;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // �^�O�FEnemy
        if(other.gameObject.CompareTag("Enemy"))
        {
            // ���X�g�ɒǉ�
            hitolist.Add(other.gameObject);
        }
    }
    #endregion ---Methods---
}
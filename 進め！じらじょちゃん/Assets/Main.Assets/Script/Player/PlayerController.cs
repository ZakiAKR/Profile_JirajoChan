using UnityEngine;
using UnityEngine.AI;

// �쐬�ҁF�n����
// �v���C���[�̓���

public class PlayerController : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// ���C���J�����I�u�W�F�N�g�Q��
    /// </summary>
    [Tooltip("���C���J�����A�^�b�`")]
    [SerializeField] GameObject _mainCamera;

    /// <summary>
    /// �T�u�J�����I�u�W�F�N�g�Q��
    /// </summary>
    [Tooltip("�T�u�J�����A�^�b�`")]
    [SerializeField] GameObject _subCamera;

    /// <summary>
    /// �Q�[���I�[�o�[���[�V����������I�u�W�F�N�g���擾
    /// </summary>
    [Tooltip("�Q�[���I�[�o�[���[�V�����I�u�W�F�N�g�A�^�b�`")]
    [SerializeField] GameObject _gameoverObj;

    // �x������NavMeshAgent�AAroundGuardsmanController���Q�Ƃ���
    [Tooltip("����x�������A�^�b�`")]
    [SerializeField] NavMeshAgent _agent;
    [Tooltip("����x�������A�^�b�`")]
    [SerializeField] AroundGuardsmanController _controller;

    /// <summary>
    /// ValueSettingManager�Q�Ƃ���ϐ�
    /// </summary>
    [SerializeField] ValueSettingManager settingManager;

    /// <summary>
    /// AudioManager�Q�Ƃ���ϐ�
    /// </summary>
    [SerializeField] AudioManager audioManager;

    /// <summary>
    /// ��]�̐��l���擾����ϐ�
    /// </summary>
    float _rot;
    float _vertical;

    /// <summary>
    /// ��]�X�s�[�h���擾����ϐ��������傫���قǑ����Ȃ�
    /// </summary>
    float _rotateSpeed;

    /// <summary>
    /// �O��ړ��X�s�[�h���擾����ϐ��������傫���قǑ����Ȃ�
    /// </summary>
    float _positionSpeed;

    /// <summary>
    /// �J�����̐؂�ւ�����
    /// </summary>
    bool _cameraActive = true;


    #endregion ---Fields---

    #region ---Methods---

    void Start()
    {
        // �l���Q�Ƃ������̂�ۑ�����
        _rotateSpeed = settingManager.PlayerRotateSpeed;
        _positionSpeed = settingManager.JOYSTIC_PlayerMoveSpeed;

        //�T�u�J�������A�N�e�B�u�ɂ���
        _subCamera.SetActive(false);
    }

    void Update()
    {
        // ��]�̐��l�擾
        _rot = Input.GetAxis("Horizontal");
        //_vertical = Input.GetAxis("Vertical");

        // ��]
        transform.Rotate(new Vector3(0, _rot * _rotateSpeed, 0));

        // �J�������㉺�ɉ�]������
        //_mainCamera.transform.Rotate(_vertical * _rotateSpeed, 0, 0);

        //// �O��ړ�
        //// �O
        //if (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.W))
        //{
        //    transform.position += transform.forward * _positionSpeed;

        //    if (audioManager.CheckPlaySound(audioManager.seAudioSource))
        //    {
        //        audioManager.PlaySESound(SEData.SE.Walk);
        //    }
        //}
        //// ���
        //if (Input.GetKey(KeyCode.JoystickButton2) || Input.GetKey(KeyCode.S))
        //{
        //    transform.position -= transform.forward * _positionSpeed;

        //    if (audioManager.CheckPlaySound(audioManager.seAudioSource))
        //    {
        //        audioManager.PlaySESound(SEData.SE.Walk);
        //    }
        //}

        //if (Input.GetKeyUp(KeyCode.JoystickButton1) || Input.GetKeyUp(KeyCode.JoystickButton2))
        //{
        //    if (!audioManager.CheckPlaySound(audioManager.seAudioSource))
        //    {
        //        audioManager.StopSound(audioManager.seAudioSource);
        //    }
        //}

        // �W���C�R���̉E�X�e�B�b�N�������ƃ��C���J�����ƃT�u�J������؂�ւ���
        if (Input.GetKeyDown(KeyCode.JoystickButton11) || Input.GetKeyDown(KeyCode.Space))
        {
            if(_cameraActive)
            {
                _mainCamera.SetActive(false);
                _subCamera.SetActive(true);
                _cameraActive = false;
            }
            else
            {
                _mainCamera.SetActive(true);
                _subCamera.SetActive(false);
                _cameraActive = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Guardman"))
        {
            // �x�����̓������~�߂�
            _controller.enabled = false;
            _agent.enabled = false;

            // ���݂̃I�u�W�F�N�g���\��
            gameObject.SetActive(false);

            // �Q�[���I�[�o�[�I�u�W�F�N�g�����݈ʒu�ɃZ�b�g
            _gameoverObj.transform.position = transform.position;
            // �Q�[���I�[�o�[�I�u�W�F�N�g��\��
            _gameoverObj.SetActive(true);
            //Debug.Log("playerhit");
        }
    }
    #endregion ---Methods---
}
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Navigation��p��������^�x�����̓���
// �쐬�ҁF�n����

public class AroundGuardsmanController : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    ///  ���������n�_�̕ϐ�
    /// </summary>
    int _nextPoint = 0;

    /// <summary>
    ///  ���E�����Ă邩�����ĂȂ������肷��ϐ�
    /// </summary>
    bool _targetFlag = false;

    /// <summary>
    ///  SE����x�����炷���肷��ϐ�
    /// </summary>
    bool _SEflag = true;

    /// <summary>
    ///  NavMeshAgent�擾
    /// </summary>
    NavMeshAgent _agent;

    /// <summary>
    ///  �x�����̒��p�|�C���g���擾
    /// </summary>
    [Tooltip("�x�����̒��p�|�C���g���A�^�b�`")]
    [SerializeField] Transform[] _points;

    /// <summary>
    ///  player�I�u�W�F�N�g���擾
    /// </summary>
    [Tooltip("Player�̃I�u�W�F�N�g�����")]
    [SerializeField] GameObject _target;

    /// <summary>
    ///�@UIImage�擾
    /// </summary>
    [Tooltip("������������UI")]
    [SerializeField] Image _haken;

    // �X�N���v�g�Q�ƕϐ�
    [SerializeField] ValueSettingManager settingManager;
    [SerializeField] AudioManager audioManager;

    #endregion ---Fields---

    #region ---Methods---

    void Start()
    {
        // NavMeshAgent�擾
        _agent = GetComponent<NavMeshAgent>();

        // NavMeshAgent�̒l���Q�Ƃ��ĕۑ�
        _agent.speed = settingManager.guardMoveSpeed;
        _agent.angularSpeed = settingManager.guardAngularSpeed;
        _agent.acceleration = settingManager.guardAcceleration;

        //autoBraking �𖳌��ɂ���ƖڕW�n�_�̊Ԃ��p���I�Ɉړ�
        //�܂�A�G�[�W�F���g�͖ڕW�n�_�ɋ߂Â��Ă����x�𗎂Ƃ��Ȃ�
        _agent.autoBraking = false;
    }

    void Update()
    {
        // �G�[�W�F���g�����ڕW�n�_�ɋ߂Â��Ă����玟�̖ڕW�n�_��I��
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

        // true��������_target�̂Ƃ���Ɍ�����
        if (_targetFlag)
        {
            _agent.destination = _target.transform.position;
        }
        if (!_targetFlag)
        {
            //Debug.Log(_destPoint);
            _agent.destination = _points[_nextPoint].position;
        }
    }

    void GotoNextPoint()
    {
        // �n�_���Ȃɂ��ݒ肳��Ă��Ȃ��Ƃ��ɕԂ�
        if (_points.Length == 0)
        {
            return;
        }

        // �G�[�W�F���g�����ݐݒ肳�ꂽ�ڕW�n�_�ɍs���悤�ɐݒ�
        _agent.destination = _points[_nextPoint].position;

        // �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�K�v�Ȃ�Ώo���n�_�ɂ��ǂ�
        _nextPoint = (_nextPoint + 1) % _points.Length;

        if (_nextPoint == 4)
        {
            _nextPoint = 0;
        }
    }

    // ���E�ɓ�������ǂ������Ă���
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(_SEflag)
            {
                audioManager.PlaySESound(SEData.SE.FoundSecurity);
                //Debug.Log("���E������");
                _targetFlag = true;
                _haken.gameObject.SetActive(true);
                _SEflag = false;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("���E�ł�");
            _targetFlag = false;
            _haken.gameObject.SetActive(false);
            _SEflag = true;
        }
    }
    #endregion ---Methods---
}
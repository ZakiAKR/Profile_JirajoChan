using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Navigationを用いた巡回型警備員の動き
// 作成者：地引翼

public class AroundGuardsmanController : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    ///  次向かう地点の変数
    /// </summary>
    int _nextPoint = 0;

    /// <summary>
    ///  視界入ってるか入ってないか判定する変数
    /// </summary>
    bool _targetFlag = false;

    /// <summary>
    ///  SEを一度だけ鳴らす判定する変数
    /// </summary>
    bool _SEflag = true;

    /// <summary>
    ///  NavMeshAgent取得
    /// </summary>
    NavMeshAgent _agent;

    /// <summary>
    ///  警備員の中継ポイントを取得
    /// </summary>
    [Tooltip("警備員の中継ポイントをアタッチ")]
    [SerializeField] Transform[] _points;

    /// <summary>
    ///  playerオブジェクトを取得
    /// </summary>
    [Tooltip("Playerのオブジェクト入れる")]
    [SerializeField] GameObject _target;

    /// <summary>
    ///　UIImage取得
    /// </summary>
    [Tooltip("見つかった時のUI")]
    [SerializeField] Image _haken;

    // スクリプト参照変数
    [SerializeField] ValueSettingManager settingManager;
    [SerializeField] AudioManager audioManager;

    #endregion ---Fields---

    #region ---Methods---

    void Start()
    {
        // NavMeshAgent取得
        _agent = GetComponent<NavMeshAgent>();

        // NavMeshAgentの値を参照して保存
        _agent.speed = settingManager.guardMoveSpeed;
        _agent.angularSpeed = settingManager.guardAngularSpeed;
        _agent.acceleration = settingManager.guardAcceleration;

        //autoBraking を無効にすると目標地点の間を継続的に移動
        //つまり、エージェントは目標地点に近づいても速度を落とさない
        _agent.autoBraking = false;
    }

    void Update()
    {
        // エージェントが現目標地点に近づいてきたら次の目標地点を選択
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

        // trueだったら_targetのところに向かう
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
        // 地点がなにも設定されていないときに返す
        if (_points.Length == 0)
        {
            return;
        }

        // エージェントが現在設定された目標地点に行くように設定
        _agent.destination = _points[_nextPoint].position;

        // 配列内の次の位置を目標地点に設定し必要ならば出発地点にもどる
        _nextPoint = (_nextPoint + 1) % _points.Length;

        if (_nextPoint == 4)
        {
            _nextPoint = 0;
        }
    }

    // 視界に入ったら追いかけてくる
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(_SEflag)
            {
                audioManager.PlaySESound(SEData.SE.FoundSecurity);
                //Debug.Log("視界入った");
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
            //Debug.Log("視界でた");
            _targetFlag = false;
            _haken.gameObject.SetActive(false);
            _SEflag = true;
        }
    }
    #endregion ---Methods---
}
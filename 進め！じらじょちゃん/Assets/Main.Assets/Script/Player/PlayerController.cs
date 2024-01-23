using UnityEngine;
using UnityEngine.AI;

// 作成者：地引翼
// プレイヤーの動き

public class PlayerController : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// メインカメラオブジェクト参照
    /// </summary>
    [Tooltip("メインカメラアタッチ")]
    [SerializeField] GameObject _mainCamera;

    /// <summary>
    /// サブカメラオブジェクト参照
    /// </summary>
    [Tooltip("サブカメラアタッチ")]
    [SerializeField] GameObject _subCamera;

    /// <summary>
    /// ゲームオーバーモーションをするオブジェクトを取得
    /// </summary>
    [Tooltip("ゲームオーバーモーションオブジェクトアタッチ")]
    [SerializeField] GameObject _gameoverObj;

    // 警備員のNavMeshAgent、AroundGuardsmanControllerを参照する
    [Tooltip("巡回警備員をアタッチ")]
    [SerializeField] NavMeshAgent _agent;
    [Tooltip("巡回警備員をアタッチ")]
    [SerializeField] AroundGuardsmanController _controller;

    /// <summary>
    /// ValueSettingManager参照する変数
    /// </summary>
    [SerializeField] ValueSettingManager settingManager;

    /// <summary>
    /// AudioManager参照する変数
    /// </summary>
    [SerializeField] AudioManager audioManager;

    /// <summary>
    /// 回転の数値を取得する変数
    /// </summary>
    float _rot;
    float _vertical;

    /// <summary>
    /// 回転スピードを取得する変数数字が大きいほど速くなる
    /// </summary>
    float _rotateSpeed;

    /// <summary>
    /// 前後移動スピードを取得する変数数字が大きいほど速くなる
    /// </summary>
    float _positionSpeed;

    /// <summary>
    /// カメラの切り替え判定
    /// </summary>
    bool _cameraActive = true;


    #endregion ---Fields---

    #region ---Methods---

    void Start()
    {
        // 値を参照したものを保存する
        _rotateSpeed = settingManager.PlayerRotateSpeed;
        _positionSpeed = settingManager.JOYSTIC_PlayerMoveSpeed;

        //サブカメラを非アクティブにする
        _subCamera.SetActive(false);
    }

    void Update()
    {
        // 回転の数値取得
        _rot = Input.GetAxis("Horizontal");
        //_vertical = Input.GetAxis("Vertical");

        // 回転
        transform.Rotate(new Vector3(0, _rot * _rotateSpeed, 0));

        // カメラを上下に回転させる
        //_mainCamera.transform.Rotate(_vertical * _rotateSpeed, 0, 0);

        //// 前後移動
        //// 前
        //if (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.W))
        //{
        //    transform.position += transform.forward * _positionSpeed;

        //    if (audioManager.CheckPlaySound(audioManager.seAudioSource))
        //    {
        //        audioManager.PlaySESound(SEData.SE.Walk);
        //    }
        //}
        //// 後ろ
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

        // ジョイコンの右スティックを押すとメインカメラとサブカメラを切り替える
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
            // 警備員の動きを止める
            _controller.enabled = false;
            _agent.enabled = false;

            // 現在のオブジェクトを非表示
            gameObject.SetActive(false);

            // ゲームオーバーオブジェクトを現在位置にセット
            _gameoverObj.transform.position = transform.position;
            // ゲームオーバーオブジェクトを表示
            _gameoverObj.SetActive(true);
            //Debug.Log("playerhit");
        }
    }
    #endregion ---Methods---
}
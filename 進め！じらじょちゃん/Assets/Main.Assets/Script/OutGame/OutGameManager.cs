using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//  ゲームがゲームオーバー・クリアになった時の処理
//  作成者：山﨑晶

public class OutGameManager : MonoBehaviour
{
    #region ---Fields---

    [Header("=== Character ===")]
    /// <summary>
    /// プレイヤーのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _playerObj;

    /// <summary>
    /// 周囲の観客を消す範囲のオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _noActiveArea;

    /// <summary>
    /// プレイヤーの固定位置
    /// </summary>
    private Vector3 _endPos;

    /// <summary>
    /// プレイヤーが固定されたかの判定
    /// </summary>
    private bool _isFixation;

    /// <summary>
    /// 警備員のオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _guardsObj;

    /// <summary>
    /// 警備員のアニメーションのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _guardsAnimatorObj;


    [Header("=== Camera ===")]
    /// <summary>
    /// 注目する注視点のオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _TargetObj;

    /// <summary>
    /// メインカメラ
    /// </summary>
    [SerializeField]
    private GameObject _mainCamera;

    /// <summary>
    /// 左肩カメラ
    /// </summary>
    [SerializeField]
    private GameObject _leftCamera;

    /// <summary>
    /// 右肩カメラ
    /// </summary>
    [SerializeField]
    private GameObject _rightCamera;

    /// <summary>
    /// カメラが動く速さ
    /// </summary>
    private float _cameraSpeed;

    [ Header("=== Text ===")]
    /// <summary>
    /// クリアテキストのオブジェクト
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _clearText;

    /// <summary>
    /// スキップテキストのオブジェクト
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _skipText;

    /// <summary>
    /// 盛り上げテキストのオブジェクト
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _moriageText;

    /// <summary>
    /// ボタンテキストのオブジェクト
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _buttonText;

    /// <summary>
    /// 中断テキストのオブジェクト
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _stopGameText;

    /// <summary>
    /// 盛り上げテキストが表示されたかの判定
    /// </summary>
    private bool _isMoriage;

    [Header("=== Script ===")]
    /// <summary>
    /// ValueSettingTable
    /// </summary>
    [SerializeField]
    private ValueSettingManager _settingManager;

    /// <summary>
    /// system_Audioのスクリプト
    /// </summary>
    [SerializeField]
    private AudioManager _audioSystem;

    /// <summary>
    /// Cameraのスクリプト
    /// </summary>
    [SerializeField]
    private CameratoAudioManager _cameraSystem;

    #endregion ---Fields---

    #region ---Methods---

    private void Awake()
    {
        // ゲームオーバー・ゲームクリアの判定をオフにする
        _settingManager.gameOver = false;
        _settingManager.gameClear = false;
    }

    private void Start()
    {
        // 周囲の観客を消すオブジェクトを非アクティブにする
        _noActiveArea.SetActive(false);

        // スキップテキストを非表示する
        _skipText.enabled = false;

        // クリアテキストを非表示する
        _clearText.enabled = false;

        // ボタンテキストを非表示する
        _buttonText.enabled = false;

        // 盛り上げテキストを非表示する
        _moriageText.enabled = false;

        // 中断テキストを非表示する
        _stopGameText.enabled = true;

        // テキストが表示されたかの判定をオフにする
        _isMoriage = false;

        _cameraSpeed = _settingManager.stageLookCamera;
    }

    private void Update()
    {
        // ゲームオーバー時の処理  
        if (_settingManager.gameOver)
        {
            // BGMを止める
            _audioSystem.StopSound(_audioSystem.bgmAudioSource);

            // プレイヤーと警備員の動きを止める関数の呼び出し
            DontMove_OtherScript();

            // シーンを遷移する
            SceneManager.LoadScene(6);
        }

        // ゲームクリア時の処理
        if (_settingManager.gameClear)
        {
            GameClearFunc();
        }

        if (!_settingManager.gameOver && !_settingManager.gameClear)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.LeftAlt))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    /// <summary>
    /// ゲームクリア時の演出関数
    /// </summary>
    private void GameClearFunc()
    {
        // 位置が固定されてない判定になっていた場合
        if (!_isFixation)
        {
            // 現在のプレイヤーの位置を保存する
            _endPos = _playerObj.transform.position;

            // 位置が固定された判定をオンにする
            _isFixation = true;
        }

        // プレイヤーの位置を最終位置に固定する
        _playerObj.transform.position = _endPos;

        // 周囲の観客を消すオブジェクトをアクティブにする
        _noActiveArea.SetActive(true);

        // プレイヤーと警備員の動きを止める関数の呼び出し
        DontMove_OtherScript();

        // メインカメラ視点を使用している場合
        if (_cameraSystem._normal)
        {
            // メインカメラをステージの注視点に向けて移動させる
            CameraMode(_mainCamera);
        }

        // 左肩カメラ視点もしくは左肩カメラを使用していた場合
        if (_cameraSystem._normalDiffPos || _cameraSystem._stickButton)
        {
            // 左肩カメラをステージの注視点に向けて移動させる
            CameraMode(_leftCamera);
        }

        // 左肩カメラと右肩カメラの両視点を使用している場合
        if (_cameraSystem._switchButton)
        {
            // 左肩カメラが有効だった場合
            if (_leftCamera.GetComponent<Camera>().enabled)
            {
                // 左肩カメラをステージの注視点に向けて移動させる
                CameraMode(_leftCamera);
            }
            else if (_rightCamera.GetComponent<Camera>().enabled) // 右肩カメラが有効だった場合
            {
                // 右肩カメラをステージの注視点に向けて移動させる
                CameraMode(_rightCamera);
            }
        }

        // 盛り上げテキストの表示判定がオフだった場合
        if (!_isMoriage)
        {
            StartCoroutine(text());
        }

        // Aボタンが押された場合　デバッグ用にFキー
        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.F))
        {
            // 歓声を再生する
            _audioSystem.PlaySESound(SEData.SE.Cheer);
        }

        // Bボタンが押された場合　デバッグ用にGキー
        if (Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.G))
        {
            // 叫びを再生する
            _audioSystem.PlaySESound(SEData.SE.Shout);
        }

        // Rボタンを押した場合　デバッグ用にKキー
        if (Input.GetKeyDown(KeyCode.JoystickButton14) || Input.GetKeyDown(KeyCode.K))
        {
            // クリア演出をスキップする
            GameClear();
        }
    }

    /// <summary>
    /// テキストの表示演出関数
    /// </summary>
    /// <returns></returns>
    private IEnumerator text()
    {
        // 中断テキストを非表示にする
        _stopGameText.enabled = false;

        // スキップテキストを表示する
        _skipText.enabled = true;

        // クリアテキストを表示する
        _clearText.enabled = true;

        // ボタンテキストを表示する
        _buttonText.enabled = true;

        // 盛り上げテキストを表示する
        _moriageText.enabled = true;

        // 処理を待つ
        yield return new WaitForSeconds(6);

        // 盛り上げテキストを非表示にする
        _moriageText.enabled = false;

        // 盛り上げテキストの表示判定をオンにする
        _isMoriage = true;
    }

    /// <summary>
    /// ゲームクリア画面に遷移するための関数
    /// </summary>
    public void GameClear()
    {
        // BGMを止める
        _audioSystem.StopSound(_audioSystem.bgmAudioSource);

        // シーンを遷移する
        SceneManager.LoadScene(5);
    }

    /// <summary>
    /// カメラをステージの注視点に向けて移動させる関数
    /// </summary>
    /// <param name="camera"> 動かしたいカメラ </param>
    private void CameraMode(GameObject camera)
    {
        // カメラの回転を移動させる
        camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, Quaternion.LookRotation((_TargetObj.transform.position - camera.transform.position).normalized), _cameraSpeed * Time.deltaTime);
    }

    /// <summary>
    ///  オブジェクトの動きを止める関数
    /// </summary>
    private void DontMove_OtherScript()
    {
        //  プレイヤーのMocopiを止める
        //_playerObj.GetComponent<MocopiAvatar>().enabled = false;

        // プレイヤーのジョイコンを止める
        _playerObj.GetComponent<PlayerController>().enabled = false;

        // プレイヤーの足踏みを止める
        _playerObj.GetComponent<PlayerWalkManager>().enabled = false;

        // 警備員の移動を止める 
        _guardsObj.GetComponent<AroundGuardsmanController>().enabled = false;

        // 警備員の動作を止める
        _guardsObj.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        // 警備員のアニメーションを止める
        _guardsAnimatorObj.GetComponent<Animator>().enabled = false;
    }

    #endregion ---Methods---
}
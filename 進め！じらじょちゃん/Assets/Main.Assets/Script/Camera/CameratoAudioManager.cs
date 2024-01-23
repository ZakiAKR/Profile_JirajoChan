using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 作成者：山﨑晶
// カメラに関するスクリプト

public class CameratoAudioManager : MonoBehaviour
{
    #region ---Fields---

    [Header("=== Object ===")]
    /// <summary>
    /// カメラの回転する動作の中心点
    /// </summary>
    [SerializeField]
    private Transform _playerObj;

    /// <summary>
    /// 対象オブジェクトの_layer
    /// </summary>
    [SerializeField]
    private LayerMask _layer;

    /// <summary>
    /// Ray
    /// </summary>
    private Ray _ray;

    /// <summary>
    /// Rayに当たったオブジェクトを格納するリスト
    /// </summary>
    private List<GameObject> _hitObj=new List<GameObject> ();

    /// <summary>
    /// 前回の保存したオブジェクトを格納するリスト
    /// </summary>
    private GameObject[] _saveObj;

    /// <summary>
    /// Rawの範囲
    /// </summary>
    private float _rawRadio;

    /// <summary>
    /// カメラとプレイヤーの距離
    /// </summary>
    private Vector3 _offset;

    [Header("=== Camera ===")]
    /// <summary>
    /// メインカメラのオブジェクト
    /// </summary>
    [SerializeField]
    private CameraInfo _mainCameraObj;

    /// <summary>
    /// 左肩カメラのオブジェクト
    /// </summary>
    [SerializeField]
    private CameraInfo _leftCameraObj;

    /// <summary>
    /// 右肩カメラのオブジェクト
    /// </summary>
    [SerializeField]
    private CameraInfo _rightCameraObj;

    /// <summary>
    /// Rayを飛ばす場所を指定するオブジェクト
    /// </summary>
    private CameraInfo _cameraInfoObj;

    [Header("=== Camera Function ===")]
    /// <summary>
    /// じらじょちゃんの真後ろから追跡する機能
    /// </summary>
    [SerializeField]
    public bool _normal = true;

    /// <summary>
    /// じらじょちゃんの肩らへんから追跡する機能
    /// </summary>
    [SerializeField]
    public bool _normalDiffPos = false;

    /// <summary>
    /// スイッチで視点の場所が切り替わる機能
    /// </summary>
    [SerializeField]
    public bool _switchButton = false;

    /// <summary>
    /// スティック移動で視点移動できる機能
    /// </summary>
    [SerializeField]
    public bool _stickButton = false;

    [Header("=== Canvas ===")]
    /// <summary>
    /// リアクションUIのCanvas
    /// </summary>
    [SerializeField]
    private Canvas _riactionCanvas;

    /// <summary>
    /// 終了UIのCanvas
    /// </summary>
    [SerializeField]
    private Canvas _finishCanvas;

    /// <summary>
    /// プレイヤーUIのCanvas
    /// </summary>
    [SerializeField]
    private Canvas _situationCanvas;

    [Header("=== Object Table ===")]
    /// <summary>
    /// ValueSettingTable
    /// </summary>
    [SerializeField]
    private ValueSettingManager _settingSystem;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        // Rawの範囲の値を参照して保存する
        _rawRadio = _settingSystem.cameraHitRadio;

        // オブジェクトとコンポーネントを初期化する
        _mainCameraObj = new CameraInfo(_mainCameraObj.cameraObj);
        _leftCameraObj = new CameraInfo(_leftCameraObj.cameraObj);
        _rightCameraObj = new CameraInfo(_rightCameraObj.cameraObj);

        // カメラ機能がNomalだった場合
        if (_normal)
        {
            // カメラのアクティブの設定と距離の測定をする
            CameraInit(_mainCameraObj.cameraObj, true, false, false);
        }

        // カメラ機能がNomalDiffPos / StickButtonだった場合
        if (_normalDiffPos || _stickButton)
        {
            // カメラのアクティブの設定と距離の測定をする
            CameraInit(_leftCameraObj.cameraObj, false, true, false);
        }

        // カメラ機能がSwitchButtonだった場合
        if (_switchButton)
        {
            // カメラのアクティブの設定と距離の測定をする
            CameraInit(_leftCameraObj.cameraObj, false, true, true,false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // カメラ機能がNomalだった場合
        if (_normal)
        {
            _cameraInfoObj = new CameraInfo(_mainCameraObj.cameraObj);
        }

        // カメラ機能がNomalDiffPosだった場合
        if (_normalDiffPos)
        {
            // canvasのカメラ設定を左肩カメラに設定する
            _riactionCanvas.worldCamera = _leftCameraObj.cameraComp;
            _finishCanvas.worldCamera = _leftCameraObj.cameraComp;
            _situationCanvas.worldCamera = _leftCameraObj.cameraComp;

            _cameraInfoObj = new CameraInfo(_leftCameraObj.cameraObj);
        }

        // カメラの機能がSwitchButtonだった場合
        if (_switchButton)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton0) && !_rightCameraObj.cameraComp.enabled)
            {
                // 右肩カメラの設定をする
                SwitchButtonCamera(false, true, _rightCameraObj);
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton3) && !_leftCameraObj.cameraComp.enabled)
            {
                // 左肩カメラの設定をする
                SwitchButtonCamera(true, false, _leftCameraObj);
            }
        }

        // Rayに当たったオブジェクトの表示非表示をする
        RayFunc(_cameraInfoObj);
    }

    /// <summary>
    /// カメラのアクティブと距離の初期化する関数
    /// </summary>
    /// <param name="offset"> 距離を保存する変数 </param>
    /// <param name="cameraObj"> 距離を測定するカメラのオブジェクト </param>
    /// <param name="main"> メインカメラのアクティブ </param>
    /// <param name="left"> 左肩カメラのアクティブ </param>
    /// <param name="right"> 右肩カメラのアクティブ </param>
    /// <param name="switchCamera"> SwitchButton機能用のカメラコンポーネントのアクティブ </param>
    private void CameraInit(GameObject cameraObj, bool main, bool left, bool right,bool switchCamera=true)
    {
        // カメラのカメラコンポーネントのオンオフを設定する
        _mainCameraObj.cameraComp.enabled = main;
        _leftCameraObj.cameraComp.enabled = left;
        if (!switchCamera)
        {
            _rightCameraObj.cameraComp.enabled = switchCamera;
        }
        else
        {
            _rightCameraObj.cameraComp.enabled = right;
        }

        // カメラのアクティブを設定する
        _mainCameraObj.cameraObj.SetActive(main);
        _leftCameraObj.cameraObj.SetActive(left);
        _rightCameraObj.cameraObj.SetActive(right);

        // カメラとプレイヤーの距離を計算する
        _offset = cameraObj.transform.position - _playerObj.position;
    }

    /// <summary>
    /// SwitchButtonの時のカメラとcanvasの設定とRayの設定をする関数
    /// </summary>
    /// <param name="left"> 左肩カメラのアクティブ </param>
    /// <param name="right"> 右肩カメラのアクティブ </param>
    /// <param name="cameraObj"> canvasに設定したいカメラのオブジェクト </param>
    /// <param name="pos"> _offsetの正規化 </param>
    private void SwitchButtonCamera(bool left,bool right,CameraInfo cameraObj)
    {
        // 左肩カメラのCameraコンポーネントと右肩カメラのCameraコンポーネントのアクティブを設定する
        _leftCameraObj.cameraComp.enabled = left;
        _rightCameraObj.cameraComp.enabled = right;

        // canvasのカメラ設定をする
        _riactionCanvas.worldCamera = cameraObj.cameraComp;
        _finishCanvas.worldCamera = cameraObj.cameraComp;
        _situationCanvas.worldCamera = cameraObj.cameraComp;

        // カメラとプレイヤーの距離を計算する
        _offset = cameraObj.cameraObj.transform.position - _playerObj.position;

        // Rayを飛ばすカメラオブジェクトを設定する
        _cameraInfoObj = new CameraInfo(cameraObj.cameraObj);
    }

    /// <summary>
    /// カメラから出るRayに当たったオブジェクトを表示非表示する関数
    /// </summary>
    /// <param name="cameraObj"> Rayを飛ばすカメラのオブジェクト </param>
    private void RayFunc(CameraInfo cameraObj)
    {
        // ２点間のベクトルを正規化する
        Vector3 positionVector = _offset.normalized;

        // _rayをカメラからプレイヤーに飛ばす
        _ray = new Ray(cameraObj.cameraObj.transform.position, positionVector);

        // 球体のRayを生成する
        RaycastHit[] _hits = Physics.SphereCastAll(_ray, _rawRadio, positionVector.magnitude, _layer);

        // 前回のリストを保存する
        _saveObj = _hitObj.ToArray();

        // リストを初期化する
        _hitObj.Clear();

        // 遮蔽物は一時的にすべて描画機能を無効にする。
        foreach (RaycastHit _hit in _hits)
        {
            // 遮蔽物の Renderer コンポーネントを無効にする
            GameObject _renderer = _hit.collider.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;

            // オブジェクトが存在していた場合
            if (_renderer != null)
            {
                // 当たったオブジェクトをリストに追加する
                _hitObj.Add(_renderer);

                //当たったオブジェクトを非表示にする
                _renderer.SetActive(false);
            }
        }

        // 前回まで対象で、今回対象でなくなったものは、表示を元に戻す。
        foreach (GameObject _renderer in _saveObj.Except(_hitObj))
        {
            // オブジェクトが存在していた場合
            if (_renderer != null)
            {
                // オブジェクトを表示する
                _renderer.SetActive(true);
            }
        }
    }

    #endregion ---Methods---

    #region ---Struct---

    /// <summary>
    /// カメラオブジェクトの構造体
    /// </summary>
    [System.Serializable]
    private struct CameraInfo
    {
        /// <summary>
        /// カメラのオブジェクト
        /// </summary>
        public GameObject cameraObj;

        /// <summary>
        /// カメラのCameraコンポーネント
        /// </summary>
        public Camera cameraComp;

        /// <summary>
        /// カメラのオブジェクトとコンポーネントを設定するコンストラクタ
        /// </summary>
        /// <param name="obj"></param>
        public CameraInfo(GameObject obj)
        {
            // カメラのオブジェクトを設定する
            this.cameraObj= obj;
            // カメラのCameraコンポーネントを設定する
            this.cameraComp = obj.GetComponent<Camera>();
        }
    }

    #endregion ---Struct---
}

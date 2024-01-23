using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

// 作成者：山﨑晶
// タイトル画面のUI演出をするソースコード

public class TitleUIManager : MonoBehaviour
{
    #region ---Fields---

    [Header("=== Video ===")]
    /// <summary>
    /// タイトルスタートを再生するオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _titleStartObj;

    /// <summary>
    /// タイトルスタートのVideoPlayer
    /// </summary>
    private VideoPlayer _titleStartVideo;

    /// <summary>
    /// タイトルロゴを再生するオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _titleLogoObj;

    /// <summary>
    /// タイトルロゴのVideoPlayer
    /// </summary>
    private VideoPlayer _titleLogoVideo;

    /// <summary>
    /// タイトルの画像
    /// </summary>
    [SerializeField]
    private Image _titleImage;

    [Header("=== Button ===")]
    /// <summary>
    /// スタートボタンのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _startButtonObj;

    /// <summary>
    /// スタートボタンの選択中の画像
    /// </summary>
    [SerializeField]
    private Image[] _startButtonImage = new Image[2];

    /// <summary>
    /// アイドル紹介ボタンのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _idolButtonObj;

    /// <summary>
    /// アイドル紹介ボタンの選択中の画像
    /// </summary>
    [SerializeField]
    private Image _idolButtonImage;

    /// <summary>
    /// クレジットボタンのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _creditButtonObj;

    /// <summary>
    /// クレジットボタン画像のRect Transform
    /// </summary>
    [SerializeField]
    private RectTransform _creditButtonImage;

    /// <summary>
    /// 退場ボタンのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _endButtonObj;

    /// <summary>
    /// 退場ボタン画像のRect Transform
    /// </summary>
    [SerializeField]
    private RectTransform _endButtonImage;

    private Vector3 _buttonScaleIniti = new Vector3(1, 1, 1);

    /// <summary>
    /// クレジットボタンと退場ボタンのScaleを変更する値
    /// </summary>
    [SerializeField]
    private Vector3 _buttonScaleChange = new Vector3(1.4f, 1.4f, 1.4f);

    [Header("=== Camera Move ===")]
    /// <summary>
    /// メインカメラのオブジェクト
    /// </summary>
    [SerializeField]
    private Camera _mainCamera;

    /// <summary>
    /// メインカメラが動く速さ
    /// </summary>
    [SerializeField]
    private float _cameraMoveSpeed = 1f;

    /// <summary>
    /// メインカメラの初期位置
    /// </summary>
    private Vector3 _startPostion = new Vector3(0, 0, 0);

    /// <summary>
    /// メインカメラの移動先
    /// </summary>
    [SerializeField]
    private Vector3 _endPosition = new Vector3(0, 0, 0);

    /// <summary>
    /// メインカメラの初期位置と移動先の距離
    /// </summary>
    private float _distance = 0f;

    /// <summary>
    /// 現在のゲーム内時間
    /// </summary>
    private float _time = 0;

    [Header("=== Script ===")]
    /// <summary>
    /// system_Audioのスクリプト
    /// </summary>
    [SerializeField]
    private AudioManager _audioSystem;

    /// <summary>
    /// UI演出の現在の順番
    /// </summary>
    private int _uiCounter = 0;

    /// <summary>
    /// 選択されてるオブジェクト
    /// </summary>
    private GameObject _buttonObj;

    #endregion ---Fields---

    #region ---Methods---

    private void Start()
    {
        // ui_TitleStartVideoからVideoPlayerコンポーネントを取得
        _titleStartVideo = _titleStartObj.GetComponent<VideoPlayer>();

        // ui_TitleLogoVideoからVideoPlayerコンポーネントを取得
        _titleLogoVideo = _titleLogoObj.GetComponent<VideoPlayer>();

        // ui_TitleStartVideoのループ設定をオフにする
        _titleStartVideo.isLooping = false;

        // ui_TitleLogoVideoのループ設定をオンにする
        _titleLogoVideo.isLooping = true;

        // タイトルスタートを表示
        _titleStartObj.SetActive(true);

        // タイトルロゴを非表示
        _titleLogoObj.SetActive(false);

        // ui_TitleStartVideoを再生する
        _titleStartVideo.Play();

        // ui_TitleLogoVideoを止めておく
        _titleLogoVideo.Stop();

        // スタートボタンの選択中画像を表示しておく
        _startButtonImage[0].enabled = true;
        _startButtonImage[1].enabled = true;

        // アイドル紹介ボタンの選択中画像を非表示にしておく
        _idolButtonImage.enabled = false;

        // クレジットボタン画像のScaleを初期Scaleに設定する
        _creditButtonImage.transform.localScale = _buttonScaleIniti;

        // 退場ボタン画像のScaleを初期Scaleに設定する
        _endButtonImage.transform.localScale = _buttonScaleIniti;

        // スタートボタンを非アクティブにする
        _startButtonObj.SetActive(false);

        // アイドル紹介ボタンを非アクティブにする
        _idolButtonObj.SetActive(false);

        // クレジットボタンを非アクティブにする
        _creditButtonObj.SetActive(false);

        // 退場ボタンを非アクティブにする
        _endButtonObj.SetActive(false);

        // カメラの初期位置を保存する
        _startPostion = _mainCamera.transform.position;

        // メインカメラの初期位置と移動先位置の距離を計算する
        _distance = Vector3.Distance(_startPostion, _endPosition);

        // 初期に選択状態にしておくボタンを設定する
        EventSystem.current.SetSelectedGameObject(_startButtonObj);
    }

    private void Update()
    {
        // 再生してからUI演出の処理を稼働させるための処理
        if (_titleStartVideo.isPlaying && _uiCounter == 0)
        {
            _audioSystem.ChangeBGMVolume(1);

            // タイトルのBGMを再生する
            _audioSystem.PlayBGMSound(BGMData.BGM.Title);

            // UI演出の値を１にする
            _uiCounter = 1;
        }

        // _uiCounterの値によって処理を変える
        switch (_uiCounter)
        {
            // 再生されてるタイトルスタートが終わった後の演出
            case (int)UIdirecton.StartLogo:
                ActiveVideo();
                break;

            // ボタンを表示する演出
            case (int)UIdirecton.Button:
                OnActiveButton();
                break;

            // 選択されているボタンの演出
            case (int)UIdirecton.Select:
                SelectButton();
                break;

            // スタートボタンのAボタンが押された後にカメラを動かす演出
            case (int)UIdirecton.ClickAButton:
                CameraMove(3);
                break;

            // スタートボタンのYボタンが押された後にカメラを動かす演出
            case (int)UIdirecton.ClickYButton:
                CameraMove(4);
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// VideoPlayerの演出関数
    /// </summary>
    private void ActiveVideo()
    {
        // タイトルスタートが流れ終わった場合
        if (!_titleStartVideo.isPlaying)
        {
            // タイトル画像を表示する
            _titleImage.enabled = true;

            // タイトルスタートを停止する
            _titleStartVideo.Stop();

            // タイトルロゴを表示
            _titleLogoObj.SetActive(true);

            // タイトルロゴを再生する
            _titleLogoVideo.Play();

            // タイトルスタートを非表示
            _titleStartObj.SetActive(false);

            // UI演出を進める
            _uiCounter = 2;
        }
    }

    /// <summary>
    /// 選択されているときのボタン演出の関数
    /// </summary>
    private void OnActiveButton()
    {
        // スタートボタンをアクティブにする
        _startButtonObj.SetActive(true);

        // アイドル紹介ボタンをアクティブにする
        _idolButtonObj.SetActive(true);

        // クレジットボタンをアクティブにする
        _creditButtonObj.SetActive(true);

        // 退場ボタンをアクティブにする
        _endButtonObj.SetActive(true);

        // タイトル画像を非表示にする
        _titleImage.enabled = false;

        // UI演出を進める
        _uiCounter = 3;
    }

    /// <summary>
    /// ボタンが選択されているときのボタン演出の関数
    /// </summary>
    /// <param name="_buttonObj"> 現在選択されているボタン </param>
    private void SelectButton()
    {
        // 現在、選択されているボタンの情報を保存する
        _buttonObj = EventSystem.current.currentSelectedGameObject;
        //Debug.Log("_buttonobj : " + _buttonObj);

        // 選択されているボタンがスタートボタンな場合
        if (_buttonObj == _startButtonObj)
        {
            // スタートボタンの選択中の画像を表示
            _startButtonImage[0].enabled = true;
            _startButtonImage[1].enabled = true;

            // アイドル紹介ボタンの選択中の画像を非表示
            _idolButtonImage.enabled = false;

            // AボタンもしくはAキーが押された場合
            if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.A))
            {
                //Debug.Log("Input");
                _audioSystem.PlaySESound(SEData.SE.ClickButton);

                // タイトルロゴを非表示
                _titleLogoObj.SetActive(false);

                // スタートボタンをアクティブにする
                _startButtonObj.SetActive(false);

                // アイドル紹介ボタンをアクティブにする
                _idolButtonObj.SetActive(false);

                // クレジットボタンをアクティブにする
                _creditButtonObj.SetActive(false);

                // 退場ボタンをアクティブにする
                _endButtonObj.SetActive(false);

                // 現在の時間を保存する
                _time = Time.time;

                // UI演出を進める
                _uiCounter = 4;
            }

            // YボタンもしくはYキーが押された場合
            if (Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.Y))
            {
                _audioSystem.PlaySESound(SEData.SE.ClickButton);

                // タイトルロゴを非表示
                _titleLogoObj.SetActive(false);

                // スタートボタンをアクティブにする
                _startButtonObj.SetActive(false);

                // アイドル紹介ボタンをアクティブにする
                _idolButtonObj.SetActive(false);

                // クレジットボタンをアクティブにする
                _creditButtonObj.SetActive(false);

                // 退場ボタンをアクティブにする
                _endButtonObj.SetActive(false);

                // 現在の時間を保存
                _time = Time.time;

                // UI演出を進める
                _uiCounter = 5;
            }
        }
        // 選択されているボタンがアイドル紹介ボタンな場合
        if (_buttonObj == _idolButtonObj)
        {
            // スタートボタンの選択中の画像を非表示
            _startButtonImage[0].enabled = false;
            _startButtonImage[1].enabled = false;

            // アイドル紹介ボタンの選択中の画像を表示
            _idolButtonImage.enabled = true;

            // クレジットボタンのScaleを初期Scaleに固定
            _creditButtonObj.transform.localScale = _buttonScaleIniti;
        }
        // 選択されているボタンがクレジットボタンな場合
        if (_buttonObj == _creditButtonObj)
        {
            // アイドル紹介ボタンの選択中の画像を非表示
            _idolButtonImage.enabled = false;

            // クレジットボタンのScaleを変更
            _creditButtonObj.transform.localScale = _buttonScaleChange;

            // 退場ボタンのScaleを初期Scaleに固定
            _endButtonImage.transform.localScale = _buttonScaleIniti;
        }
        if (_buttonObj == _endButtonObj)
        {
            // アイドル紹介ボタンの選択中の画像を非表示
            _idolButtonImage.enabled = false;

            // クレジットボタンを初期Scaleに固定
            _creditButtonObj.transform.localScale = _buttonScaleIniti;

            // 退場ボタンのScaleのScaleを変更
            _endButtonImage.transform.localScale = _buttonScaleChange;
        }
    }

    /// <summary>
    /// スタートボタンが押された後のカメラを動かす演出の関数
    /// </summary>
    /// <param name="sceneNum"> 遷移したいシーンの番号 </param>
    private void CameraMove(int sceneNum)
    {
        // BGMを止める
        _audioSystem.StopSound(_audioSystem.bgmAudioSource);

        // SEが鳴っていない場合
        if (_audioSystem.CheckPlaySound(_audioSystem.seAudioSource))
        {
            // 歩いている音を再生する
            _audioSystem.PlaySESound(SEData.SE.Walk);
        }

        // カメラを移動する位置を設定する
        float _postionValue = ((Time.time - _time) / _distance) * _cameraMoveSpeed;

        // カメラを移動させる
        _mainCamera.transform.position = Vector3.Lerp(_startPostion, _endPosition, _postionValue);

        // カメラが指定の場所に移動した場合
        if (_endPosition == _mainCamera.transform.position)
        {
            // SEを止める
            _audioSystem.StopSound(_audioSystem.seAudioSource);

            // メインゲームに遷移する
            SceneManager.LoadScene(sceneNum);
        }
    }

    /// <summary>
    /// ボタンが押されたときの関数
    /// </summary>
    /// <param name="sceneNum"> 遷移したいシーンの番号 </param>
    public void OnClikButton(int sceneNum)
    {
        // クリック音を再生する
        _audioSystem.PlaySESound(SEData.SE.ClickButton);

        // ui_titleLogoVideoを止める
        _titleLogoVideo.Stop();

        // タイトルロゴを非表示
        _titleLogoObj.SetActive(false);

        // スタートボタンをアクティブにする
        _startButtonObj.SetActive(false);

        // アイドル紹介ボタンをアクティブにする
        _idolButtonObj.SetActive(false);

        // クレジットボタンをアクティブにする
        _creditButtonObj.SetActive(false);

        // 退場ボタンをアクティブにする
        _endButtonObj.SetActive(false);

        // 指定したシーンに遷移する
        SceneManager.LoadScene(sceneNum);
    }

    #endregion ---Methods---

    #region ---Enum---

    /// <summary>
    /// UIの演出ラベル
    /// </summary>
    private enum UIdirecton
    {
        StartLogo = 1,
        Button,
        Select,
        ClickAButton,
        ClickYButton,
    }

    #endregion ---Enum---
}
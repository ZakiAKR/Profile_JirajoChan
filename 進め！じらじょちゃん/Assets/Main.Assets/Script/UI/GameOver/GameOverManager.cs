using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

// 作成者：山﨑晶 
// ゲームオーバーのUI演出処理

public class GameOverManager : MonoBehaviour
{
    #region ---Fields---

    [Header("=== Video ===")]
    /// <summary>
    /// ゲームオーバーのVideo
    /// </summary>
    [SerializeField]
    private VideoPlayer _gameOverVideo;

    [Header("=== Button ===")]
    /// <summary>
    /// リトライボタンのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _moreButtonObj;

    /// <summary>
    /// リトライボタンの画像のRect Transform
    /// </summary>
    [SerializeField]
    private RectTransform _moreImageScale;

    /// <summary>
    /// リトライボタンの選択中のオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _modeSelectObj;

    /// <summary>
    /// リトライボタンの選択中の画像
    /// </summary>
    private Image _moreSelectImage;

    /// <summary>
    /// リトライボタンの選択中の画像のRect Transform
    /// </summary>
    private RectTransform _moreScale;

    /// <summary>
    /// タイトルボタンのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _backButtonObj;

    /// <summary>
    /// タイトルボタンの画像のRect Transform
    /// </summary>
    [SerializeField]
    private RectTransform _backScale;

    /// <summary>
    /// タイトルボタンの選択中の画像のオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _backSelectObj;

    /// <summary>
    /// タイトルボタンの選択中の画像
    /// </summary>
    private Image _backSelectImage;

    /// <summary>
    /// タイトルボタンの選択中の画像のRect Transform
    /// </summary>
    private RectTransform _backSelectScale;

    [Space(1)]
    /// <summary>
    /// ボタンを表示するまでの時間
    /// </summary>
    [SerializeField]
    private float _buttonActiveTime = 3f;

    /// <summary>
    /// ボタンの画像の初期サイズ
    /// </summary>
    private Vector3 _buttonScale = new Vector3(1, 1, 1);

    /// <summary>
    /// ボタンの画像の拡大サイズ
    /// </summary>
    private float _changeScale = 1.1f;

    [Header("=== Script ===")]
    /// <summary>
    /// system_Audioのスクリプト
    /// </summary>
    [SerializeField]
    private AudioManager _audioSystem;

    /// <summary>
    /// 選択中のボタン情報
    /// </summary>
    private GameObject _buttonObj;

    /// <summary>
    /// ボタンを押したかの判定
    /// </summary>
    private bool _isClick = false;

    /// <summary>
    /// シーンの番号
    /// </summary>
    private int _SceneNum;

    /// <summary>
    /// 時間を保存する値
    /// </summary>
    private float _time;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        // リトライボタンの選択中の画像オブジェクトからImageコンポーネントを取得
        _moreSelectImage=_modeSelectObj.GetComponent<Image>();
        // リトライボタンの選択中の画像オブジェクトからRectTrandoformコンポーネントを取得
        _moreScale = _modeSelectObj.GetComponent<RectTransform>();

        // タイトルボタンの選択中の画像オブジェクトからImageコンポーネントを取得
        _backSelectImage=_backSelectObj.GetComponent<Image>();
        // タイトルボタンの選択中の画像オブジェクトからRectTransformコンポーネントを取得
        _backSelectScale = _backSelectObj.GetComponent<RectTransform>();

        // リトライボタンの選択中の画像をアクティブにしておく
        _moreSelectImage.enabled = true;

        // タイトルボタンの選択中の画像を非アクティブにしておく
        _backSelectImage.enabled = false;

        // リトライボタンを非表示にする
        _moreButtonObj.SetActive(false);

        // タイトルボタンを非表示にしておく
        _backButtonObj.SetActive(false);

        // ボタンを押した判定をオフにする
        _isClick = false;

        // 時間を０にする
        _time = 0;

        // シーンの番号をタイトルの番号にする
        _SceneNum = 0;

        // ui_GameOverVideoを再生
        _gameOverVideo.Play();

        // 初期に選択状態にするオブジェクトを設定する
        EventSystem.current.SetSelectedGameObject(_moreButtonObj);
    }

    // Update is called once per frame
    void Update()
    {
        // 時間を計測
        _time += Time.deltaTime;

        // 時間が_buttonActiveTimeより長くなった場合
        if (_time >= _buttonActiveTime)
        {
            // リトライボタンもしくはタイトルボタンが非表示担っていた場合
            if (!_moreButtonObj.activeSelf || !_backButtonObj.activeSelf)
            {
                // リトライボタンを表示する
                _moreButtonObj.SetActive(true);

                // タイトルボタンを表示する
                _backButtonObj.SetActive(true);
            }

            // 現在、選択されているボタンの情報を保存する
            _buttonObj = EventSystem.current.currentSelectedGameObject;
            // Debug.Log("_buttonObj : " + _buttonObj);

            // _buttonObjに保存されている情報がリトライボタンと同じだった場合、Trueの結果が与えられる。タイトルボタンだった場合、falseの結果が与えられる。
            // リトライボタンの選択中の画像を表示する
            _moreSelectImage.enabled = _buttonObj == _moreButtonObj ? true : false;

            // タイトルボタンの選択中の画像を非表示にする
            _backSelectImage.enabled = _buttonObj == _moreButtonObj ? false : true;

            // リトライボタンの画像のサイズを拡大する
            _moreScale.transform.localScale = _buttonObj == _moreButtonObj ? new Vector3(_changeScale, _changeScale, _changeScale) : _buttonScale;
            // リトライボタンの選択中の画像のサイズを拡大する
            _moreImageScale.transform.localScale = _buttonObj == _moreButtonObj ? new Vector3(_changeScale, _changeScale, _changeScale) : _buttonScale;

            // タイトルボタンの画像のサイズを初期サイズに設定する
            _backScale.transform.localScale = _buttonObj == _moreButtonObj ? _buttonScale : new Vector3(_changeScale, _changeScale, _changeScale);
            // 
            _backSelectScale.transform.localScale = _buttonObj == _moreButtonObj ? _buttonScale : new Vector3(_changeScale, _changeScale, _changeScale);
        }

        // ボタンを押された判定がオンになり、再生していたSEが鳴り終わった場合
        if (_isClick&& _audioSystem.CheckPlaySound(_audioSystem.seAudioSource))
        {
            SceneManager.LoadScene(_SceneNum);
        }
    }

    /// <summary>
    /// ボタンが押されたときの関数
    /// </summary>
    /// <param name="SceneNum"> 遷移したいシーンの番号 </param>
    public void OnClickButton(int SceneNum)
    {
        // ボタンが押された判定をオンにする
        _isClick = true;

        // 遷移したいシーンの番号を設定する
        _SceneNum = SceneNum;

        // BGMを止める
        _audioSystem.StopSound(_audioSystem.bgmAudioSource);

        // クリック音を再生する
        _audioSystem.PlaySESound(SEData.SE.ClickButton);

        // ui_GameOverVideoを止める
        _gameOverVideo.Stop();
    }

    #endregion ---Methods---
}
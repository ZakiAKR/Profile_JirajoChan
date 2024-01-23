using UnityEngine;

// 作成者：山﨑晶
// 足踏みしているかを判定するソースコード

public class StandStillManager : MonoBehaviour
{
    #region --- Fields ---

    [Header("=== Object ===")]
    /// <summary>
    /// 右足のオブジェクトを取得する変数 
    /// </summary>
    [SerializeField]
    private GameObject _footObj;

    [SerializeField]
    private Transform _stageFloor;

    /// <summary>
    /// 右足の構造体を宣言と初期化
    /// </summary>
    private float _footPos;

    /// <summary>
    /// 歩く判定をする距離の差の値
    /// </summary>
    private float _distanceValue = 0.1f;

    /// <summary>
    /// 進む力を保存する変数　
    /// </summary>
    private float _powerSource;

    /// <summary>
    /// 歩く力を保存する変数
    /// </summary>
    [HideInInspector]
    public float moveWalkPower = 0f;

    /// <summary>
    /// 足が動いているかの判定
    /// </summary>
    private  bool _moveFoot;

    /// <summary>
    /// フレーム数
    /// </summary>
    private float _frameCount;

    /// <summary>
    /// 歩いている時間
    /// </summary>
    private float _activeWalk;

    /// <summary>
    /// 足踏みしている回数
    /// </summary>
    [HideInInspector]
    public int walkCount;

    /// <summary>
    /// 足踏みのカウントダウンを計測する判定
    /// </summary>
    private bool _isCount = true;

    [Header("=== Script ===")]
    /// <summary>
    /// 値を管理するアセットから値を参照する変数
    /// </summary>
    [SerializeField]
    private ValueSettingManager _settingSystem;

    /// <summary>
    /// system_Audioのスクリプト
    /// </summary>
    [SerializeField]
    private AudioManager _audioSystem;

    #endregion --- Fields ---

    #region --- Methods ---

    // Start is called before the first frame update
    void Start()
    {
        // 床から離れた距離の値を参照して保存する
        _distanceValue = _settingSystem.walkBorder;

        // 進む力の値を参照して保存する
        _powerSource = _settingSystem.movePower;

        // 歩いている時間の値を参照して保存する
        _activeWalk = _settingSystem.activeMoveTime;
    }

    // Update is called once per frame
    void Update()
    {
        WalkPower();
    }

    /// <summary>
    /// 歩くための動力を保存する関数
    /// </summary>
    private void WalkPower()
    {
        // 二点の距離を保存する
        _footPos = _footObj.transform.position.y - _stageFloor.position.y;
        Debug.Log("StandStill  distancce : " + _footPos);

        // 距離が_distanceValueより短かった場合
        if (_moveFoot && _footPos < _distanceValue)
        {
            // 歩く力を０にする
            moveWalkPower = 0;

            // フレーム数を０にする
            _frameCount = 0;

            // 足踏みを計測する判定をオンにする
            _isCount = true;

            // 足踏みをした判定をオフにする
            _moveFoot = false;
        }

        // 距離が_distanceValueより長かった場合
        if (!_moveFoot && _footPos >= _distanceValue)
        {
            // フレーム数を計算
            _frameCount++;

            // SEが鳴っていない場合
            if (_audioSystem.CheckPlaySound(_audioSystem.seAudioSource))
            {
                // 歩く音を再生する
                _audioSystem.PlaySESound(SEData.SE.WalkMini);
            }

            // 歩く力を加える
            moveWalkPower = _powerSource;

            // 足踏みの測定をする判定がオンだった場合
            if (_isCount)
            {
                // 足踏みの回数を増やす
                walkCount++;

                // 足踏みの測定をする判定をオフにする
                _isCount = false;
            }

            // フレーム数が_activeWalkより多かった場合
            if (_frameCount > _activeWalk)
            {

                // 足踏みをした判定にする
                _moveFoot = true;
            }
        }
    }

    #endregion --- Methods ---
}

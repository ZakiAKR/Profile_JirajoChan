using UnityEngine;

// 作成者：山﨑晶
// プレイヤーのMocopiを使用した移動のソースコード

public class PlayerWalkManager : MonoBehaviour
{
    #region ---Fields---

    [Header("=== Camera ===")]
    /// <summary>
    /// プレイヤーのカメラを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _playerCamera;

    [ Header("=== Script ===")]
    /// <summary>
    /// ValueSettingTable
    /// </summary>
    [SerializeField]
    private ValueSettingManager _settingManager;

    /// <summary>
    /// Col.RightToeBaseのスクリプト
    /// </summary>
    [SerializeField]
    private StandStillManager _movePower;

    /// <summary>
    /// Rigidbodyを保存する変数
    /// </summary>
    private Rigidbody _rb;

    /// <summary>
    /// プレイヤーのスタート位置
    /// </summary>
    private Vector3 _startPos;

    /// <summary>
    /// プレイヤーが初期位置から移動していいかの判定
    /// </summary>
    [HideInInspector]
    public bool _isActive;

    #endregion ---Fields---

    #region ---Methods---

    private void Awake()
    {
        // プレイヤーの現在の位置を保存する
        _startPos = this.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        // Rigidbodyを取得する
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // 歩く力の値を参照して保存する
        float _moveSpeed = _movePower.moveWalkPower;

        // プレイヤーが動いてはダメな判定だった場合
        if (!_isActive)
        {
            // プレイヤーを初期位置で固定する
            this.transform.position = _startPos;
        }

        // プレイヤーの正面の方向を取得する
        Vector3 moveForward = transform.forward * _moveSpeed;

        // プレイヤーを移動させる
        _rb.AddForce(moveForward, ForceMode.Impulse);
    }

    #endregion ---Methods---
}
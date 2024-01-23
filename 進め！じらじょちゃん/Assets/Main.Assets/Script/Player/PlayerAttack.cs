using UnityEngine;

// 作成者：地引翼
// mocopiでかきわけないで前に進むことを防止
// このソースがなくてもmocopiでかき分けて前に進むことが出来たため、没

public class PlayerAttack : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// 頭オブジェクト取得
    /// </summary>
    [Tooltip("頭のオブジェクトをアタッチ")]
    [SerializeField] GameObject _head;

    /// <summary>
    /// 左手オブジェクト取得
    /// </summary>
    [Tooltip("左手のオブジェクトをアタッチ")]
    [SerializeField] GameObject _lhand;

    /// <summary>
    /// 左手オブジェクト取得
    /// </summary>
    [Tooltip("右手のオブジェクトをアタッチ")]
    [SerializeField] GameObject _rhand;

    /// <summary>
    /// 観客のリジットボディ取得
    /// </summary>
    [Tooltip("観客のオブジェクトをアタッチ")]
    [SerializeField] Rigidbody _rigidbody;

    /// <summary>
    /// 観客のリジットボディ解除する範囲
    /// </summary>
    [Tooltip("観客のリジットボディ解除する数値")]
    [SerializeField] float _attackRange;

    /// <summary>
    /// 頭と手のY軸の距離
    /// </summary>
    float _distance;

    #endregion ---Fields---

    #region ---Methods---

    void Update()
    {
        // _distance = 頭のY座標 - 右手のY座標
        _distance = _head.transform.position.y - _rhand.transform.position.y;

        // 観客のRigidbodyConstraintsを全てオフ
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        //Debug.Log(_lhand.transform.position.y);
        //Debug.Log(_rhand.transform.position.y);
        //Debug.Log(_head.transform.position.y);
        //Debug.Log(_distance);

        // 頭と手の距離が指定した数値より小さかったら
        if ( _distance < _attackRange)
        {
            // 観客の回転とY軸を固定
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation
                | RigidbodyConstraints.FreezePositionY;

            //Debug.Log("Position解除");
        }
    }
    #endregion ---Methods---
}
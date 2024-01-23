using UnityEngine;

// 観客の挙動
// 作成者：地引翼

public class NPCManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// オブジェクトの座標を取得
    /// </summary>
    Vector3 _pos;

    /// <summary>
    /// 速度を取得　現在はVector3(0, 0, 0)
    /// </summary>
    Vector3 _velocity = Vector3.zero;

    /// <summary>
    /// _posへ到達するまで何秒の変数。値が小さいほど、_target に速く到達
    /// </summary>
    [SerializeField] float smoothTime;

    /// <summary>
    /// ValueSettingManagerへ参照するための変数
    /// </summary>
    //[SerializeField] ValueSettingManager _setting;

     Animator _audienceAnim;

    #endregion ---Fields---

    #region ---Methods---

    /// <summary>
    /// 初期化の変数
    /// </summary>
    void Start()
    {
        // 初期位置を保存
        _pos = transform.position;
        // 値を参照したものを保存する
        //smoothTime = _setting.smoothTime;

        _audienceAnim = GetComponent<Animator>();
    }

    void Update()
    {
        // 現在位置をリアルタイムで取得
        Vector3 _current = transform.position;
        // SmoothDamp(現在位置, 目的地, 現在の速度, _target へ到達するまでのおおよその時間。値が小さいほど、_target に速く到達)
        transform.position = Vector3.SmoothDamp(_current, _pos, ref _velocity, smoothTime);

        if(FadeController.FadeTimeOver())
        {
            _audienceAnim.Play("reaction");
        }
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        _audienceAnim.Play("hit");
    //        Debug.Log("hit");
    //    }
    //}
    #endregion ---Methods---
}
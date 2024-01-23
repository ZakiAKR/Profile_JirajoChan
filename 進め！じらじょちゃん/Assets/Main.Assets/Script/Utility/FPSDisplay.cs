using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// フレームレート数の表示
// 作成者：地引翼

public class FPSDisplay : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// フレームレート数を取得する変数
    /// </summary>
    float _fps;

    #endregion ---Fields---

    void Update()
    {
        // Time.deltaTimeは前回のフレームからの経過時間（秒）を表す変数
        _fps = 1.0f / Time.deltaTime;
        //Debug.Log(_fps.ToString("F2"));
    }
}
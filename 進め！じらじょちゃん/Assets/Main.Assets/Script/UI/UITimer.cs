using UnityEngine;
using UnityEngine.UI;

//　作成者地引翼
//　時間制限UI

public class UITimer : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// Sliderオブジェクト変数
    /// </summary>
    [Tooltip("時間制限のUIをアタッチ")]
    [SerializeField] Slider timeSlider;

    // スクリプト参照変数
    [SerializeField]　ValueSettingManager settingManager;
    [SerializeField] OutGameManager gameManager;

    /// <summary>
    /// 時間制限変数
    /// </summary>
    float maxTime;

    #endregion ---Fields---

    #region ---Methods---

    void Start()
    {
        maxTime = settingManager.GameLimitTime;

        timeSlider = GetComponent<Slider>();

        //スライダーの最大値の設定
        timeSlider.maxValue = maxTime;
    }

    void Update()
    {
        //スライダーの現在値の設定
        timeSlider.value += Time.deltaTime;

        if (timeSlider.value == maxTime&& !settingManager.gameClear)
        {
            // ゲームオーバーの判定をtrueにする
            settingManager.gameOver = true;
            //Debug.Log("時間です");
        }
        else if (timeSlider.value == maxTime && settingManager.gameClear)
        {
            gameManager.GameClear();
        }
    }
    #endregion ---Methods---
}
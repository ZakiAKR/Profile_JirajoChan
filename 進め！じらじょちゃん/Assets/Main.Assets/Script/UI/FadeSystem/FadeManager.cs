using UnityEngine;
using UnityEngine.UI;

// 作成者：山﨑晶  
// フェードイン・フェードアウトのソースコード

public class FadeManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// フェードアウトが終わったかの判定を保存する変数
    /// </summary>
    public static bool fadeOut;

    /// <summary>
    /// フェードインが終わったかの判定を保存する変数
    /// </summary>
    public static bool fadeIn;

    #endregion ---Fields---

    #region ---Methods---

    /// <summary>
    /// フェードアウト
    /// </summary>
    /// <param name="fade"> フェードの設定 </param>
    public void FadeOut(FadeSetting fade)
    {
        float fadeColor = fade.fadeImage.color.a;

        // フェードの画像を表示する    
        fade.fadeImage.enabled = true;

        // 透明度を設定する
        fadeColor += fade.fadeSpeed * Time.deltaTime;

        // フェードの画像に透明度を設定する
        fade.fadeImage.color = new Color(fade.fadeImage.color.r, fade.fadeImage.color.g, fade.fadeImage.color.b, fadeColor);

        // 特定の透明度になった場合  
        if (fadeColor >= fade.fadeAlpha)
        {
            // 透明度を固定
            fadeColor = fade.fadeAlpha;

            // フェードアウトを終了の判定にする
            fadeOut = true;
        }
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    /// <param name="fade"> フェードの設定 </param>
    public void FadeIn(FadeSetting fade)
    {
        float fadeColor = fade.fadeImage.color.a;

        // フェードの画像を表示する 
        fade.fadeImage.enabled = true;

        // 透明度を設定する
        fadeColor -= fade.fadeSpeed * Time.deltaTime;

        // フェードの画像に透明度を設定する
        fade.fadeImage.color = new Color(fade.fadeImage.color.r, fade.fadeImage.color.g, fade.fadeImage.color.b, fadeColor);

        // 特定の透明度になった場合
        if (fadeColor <= fade.fadeAlpha)
        {
            // 透明度を固定する
            fadeColor = fade.fadeAlpha;

            // フォードの画像を非表示にする    
            fade.fadeImage.enabled = false;

            // フェードインを終了の判定にする
            fadeIn = true;
        }
    }

    #endregion ---Methods---

    #region ---Struct---

    /// <summary>
    /// フェードを設定する構造体 
    /// </summary>
    [System.Serializable]
    public struct FadeSetting
    {
        /// <summary>
        /// フェードを使用する場面の説明 
        /// </summary>
        public string fadeName;

        /// <summary>
        /// フェードをする画像
        /// </summary>
        public Image fadeImage;

        /// <summary>
        /// フェードの速さ
        /// </summary>
        [Range(0f, 10f)]
        public float fadeSpeed;

        /// <summary>
        /// フェードの透明度
        /// </summary>
        [Range(0f, 1f)]
        public float fadeAlpha;
    }

    #endregion ---Struct---
}
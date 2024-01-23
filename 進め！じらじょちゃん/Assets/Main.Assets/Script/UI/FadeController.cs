using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class FadeController : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// UITimer取得
    /// </summary>
    [SerializeField] UITimer _timer;

    /// <summary>
    /// 警備員のAroundGuardsmanController取得
    /// </summary>
    [Tooltip("巡回警備員をアタッチ")]
    [SerializeField] AroundGuardsmanController _controller;

    /// <summary>
    /// 警備員のNavMeshAgent取得
    /// </summary>
    [Tooltip("巡回警備員をアタッチ")]
    [SerializeField] NavMeshAgent guardsman;

    /// <summary>
    /// AudioManager取得
    /// </summary>
    [SerializeField] AudioManager _audio;

    /// <summary>
    /// PlayerWalkManager取得
    /// </summary>
    [SerializeField] PlayerWalkManager _walk;

    /// <summary>
    /// countdownText取得
    /// </summary>
    [Tooltip("countdownTextアタッチ")]
    [SerializeField] TextMeshProUGUI _countdownText;

    /// <summary>
    /// countdownImage取得
    /// </summary>
    [Tooltip("countdownImageアタッチ")]
    [SerializeField] Image _countdownImage;

    /// <summary>
    /// フェードパネル取得
    /// </summary
    [Tooltip("フェードパネルアタッチ")]
    [SerializeField] Image _fadePanel;

    /// <summary>
    /// フェードアニメーター取得
    /// </summary>
    [Tooltip("フェードアニメーターアタッチ")]
    [SerializeField] Animator _animator;

    /// <summary>
    /// らんアニメーション再生
    /// </summary>
    [Tooltip("まおモデルアタッチ")]
    [SerializeField] Animator _maoAnim;

    /// <summary>
    /// らんアニメーション再生
    /// </summary>
    [Tooltip("らんモデルアタッチ")]
    [SerializeField] Animator _ranAnim;

    /// <summary>
    /// カウントダウン
    /// </summary>
    static float _countdown = 4f;

    /// <summary>
    /// 経過時間
    /// </summary
    int _count;

    #endregion ---Fields---

    #region ---Methods---

    void Start()
    {
        _timer.enabled = false;
        _fadePanel.enabled = true;
        guardsman.enabled = false;
        _controller.enabled = false;
        _countdown = 4f;

        _walk._isActive = false;

        _audio.PlaySESound(SEData.SE.Buzzer);
    }

    void Update()
    {
        if (_audio.CheckPlaySound(_audio.seAudioSource))
        {
            _animator.Play("FadeIn");
        }
    }

    /// <summary>
    /// アニメーションイベント用の関数
    /// </summary>
    public void Fade()
    {
        StartCoroutine("Color_FadeIn");
    }

    /// <summary>
    /// スタート前のカウントダウン
    /// </summary>
    IEnumerator Color_FadeIn()
    {
        _countdownText.gameObject.SetActive(true);
        _countdownImage.gameObject.SetActive(true);

        while (_countdown > 0)
        {
            // カウントダウン計算、表示
            _countdown -= Time.deltaTime;
            _countdownImage.fillAmount = _countdown % 1.0f;
            _count = (int)_countdown;
            _countdownText.text = _count.ToString();

            // カウントダウンが終わったら
            if (FadeTimeOver())
            {
                _timer.enabled = true;
                guardsman.enabled = true;
                _controller.enabled = true;

                // アニメーション再生
                _maoAnim.Play("dance");
                _ranAnim.Play("dance");

                _countdownText.gameObject.SetActive(false);
                _countdownImage.gameObject.SetActive(false);

                _audio.PlayBGMSound(BGMData.BGM.Main);

                _walk._isActive = true;

                yield break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// カウントダウンが終わったら返す
    /// </summary>
    public static bool FadeTimeOver()
    {
        return _countdown <= 0;
    }
    #endregion ---Methods---
}
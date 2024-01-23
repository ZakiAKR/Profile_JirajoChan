using UnityEngine;

// チュートリアルのフェーズの管理。

public class TutorialManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// フェーズパネル取得
    /// </summary>
    [Tooltip("フェーズのパネルをアタッチ")]
    [SerializeField] GameObject[] _phase;

    /// <summary>
    /// しゃがみ判定のオブジェクト取得
    /// </summary>
    [Tooltip("bodyDownobjをアタッチ")]
    [SerializeField] GameObject _bodyDownobj;

    /// <summary>
    /// かき分け判定のオブジェクトを取得
    /// </summary>
    [Tooltip("kakiwakeobjをアタッチ")]
    [SerializeField] GameObject _kakiwakeobj;

    /// <summary>
    /// かき分け判定のオブジェクトを取得
    /// </summary>
    [Tooltip("kakiwakeobj2をアタッチ")]
    [SerializeField] GameObject _kakiwakeobj_2;

    /// <summary>
    /// 観客オブジェクトを取得
    /// </summary>
    [Tooltip("観客オブジェクトをアタッチ")]
    [SerializeField] GameObject _mobobj;

    /// <summary>
    /// フェードアニメーター取得
    /// </summary>
    [Tooltip("フェードパネルをアタッチ")]
    [SerializeField] Animator _fadeAnimator;

    /// <summary>
    /// フェーズの値
    /// </summary>
    [HideInInspector] public int _phaseCount;

    /// <summary>
    /// 連続入力防止
    /// </summary>
    bool _pushFlag = false;

    #endregion ---Fields---

    #region ---Methods---

    void Update()
    {
        // 0～3に制限
        _phaseCount = Mathf.Clamp(_phaseCount, 0, 3);

        // phaseCountのパネルを表示
        _phase[_phaseCount].SetActive(true);

        // ボタン入力でフェーズが進む
        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.A))
        {
            if (_pushFlag == false)
            {
                _phase[_phaseCount].SetActive(false);
                _phaseCount++;
                _pushFlag = true;
            }
        }
        else
        {
            _pushFlag = false;
        }
        switch(_phaseCount)
        {
            case 1:
                _kakiwakeobj.SetActive(true);
                _kakiwakeobj_2.SetActive(true);
                _mobobj.SetActive(true);
                break;
            case 2:
                _kakiwakeobj.SetActive(false);
                _kakiwakeobj_2.SetActive(false);
                _mobobj.SetActive(false);
                _bodyDownobj.SetActive(true);
                break;
            case 3:
                _fadeAnimator.Play("FadeOut");
                break;
                default:
                break;
        }
    }
    #endregion ---Methods---
}
using UnityEngine;
using TMPro;

// 作成者：地引翼
// 足踏み（歩く）フェーズの制御

public class WalkManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// 足踏みの成功回数取得
    /// </summary>
    [Tooltip("足踏みの成功回数")]
    [SerializeField] int _clearCount = 3;

    /// <summary>
    /// 足踏みした回数を表示するテキスト変数
    /// </summary>
    [Tooltip("足踏みした回数を表示するテキスト")]
    [SerializeField] TextMeshProUGUI _countText;

    /// <summary>
    /// パネルオブジェクト取得
    /// </summary>
    [Tooltip("WalkPanelアタッチ")]
    [SerializeField] GameObject _walkPanel;

    /// <summary>
    /// チュートリアルパネル表示
    /// </summary>
    [Tooltip("Walkチュートリアルパネルをアタッチ")]
    [SerializeField] GameObject _panel;

    /// <summary>
    /// AudioManager参照するための変数
    /// </summary>
    [SerializeField] AudioManager _audioManager;

    /// <summary>
    /// StandStill参照するための変数
    /// </summary>
    [SerializeField] StandStillManager _standStill;

    /// <summary>
    /// TutorialManager参照するための変数
    /// </summary>
    [SerializeField] TutorialManager _tutorialManager;

    /// <summary>
    /// 音が鳴り終わったか判定するbool
    /// </summary>
    bool isAudioEnd;

    /// <summary>
    /// SEを一度だけ再生させるbool
    /// </summary>
    bool SEflag = true;


    #endregion ---Fields---

    #region ---Methods---

    void OnEnable()
    {
        // ボイス再生
        _audioManager.PlaySESound(SEData.SE.WalkVoice);
        _panel.gameObject.SetActive(true);
    }

    void Update()
    {
        // 足踏みした回数をText表示
        _countText.text = _standStill.walkCount.ToString();

        // 指定した回数以上足踏み出来たらずっとOK表示
        if(_standStill.walkCount > _clearCount)
        {
            _countText.text = "OK";
        }

        // OKサウンドを鳴らす
        if (SEflag && _standStill.walkCount > _clearCount)
        {
            _audioManager.PlaySESound(SEData.SE.Correct);
            SEflag = false;
            isAudioEnd = true;
        }

        // SEが鳴り終わったら
        if (_audioManager.CheckPlaySound(_audioManager.seAudioSource) && isAudioEnd)
        {
            _walkPanel.SetActive(false);
            _tutorialManager._phaseCount++;
        }
    }
    #endregion ---Methods---
}
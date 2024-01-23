using UnityEngine;

//  作成者：山﨑晶   
//  BGM、SEを制御するソースコード

public class AudioManager:MonoBehaviour
{
    #region ---Fields---

    [Header("=== Audio Table ===")]
    /// <summary>
    /// BGM Audio Source
    /// </summary>
    public AudioSource bgmAudioSource;

    /// <summary>
    /// SE Audio Source
    /// </summary>
    public AudioSource seAudioSource;

    /// <summary>
    /// BGM、SEの全体の音量
    /// </summary>
    private float _masterVolume;

    /// <summary>
    /// SEの全体の音量
    /// </summary>
    private float _seMasterVolume;

    /// <summary>
    /// BGMの全体の音量
    /// </summary>
    private float _bgmMasterVolume;

    [Header("=== Object Table ===")]
    /// <summary>
    /// 値を参照するために取得する変数
    /// </summary>
    [SerializeField]
    private ValueSettingManager _settingSystem;

    #endregion ---Fields---

    #region ---Methods---

    /// <summary>
    /// 初期化の変数
    /// </summary>
    private void Start()
    {
        // 値を参照したものを保存する
        _masterVolume = _settingSystem.masterVolume;
        _seMasterVolume = _settingSystem.seMasterVolume;
        _bgmMasterVolume = _settingSystem.bgmMasterVolume;
    }

    /// <summary>
    /// BGMを再生する関数
    /// </summary>
    /// <param name="bgm"> BGMのタイトル(列挙型) </param>
    public void PlayBGMSound(BGMData.BGM bgm)
    {
        // 音声データから該当するデータを保存する   
        BGMData data = _settingSystem.bgmSoundDatas.Find(data => data.bgm == bgm);

        // AudioClipをAudioSourceに設定する
        bgmAudioSource.clip = data.audioClip;

        // BGMの音量を設定する
        bgmAudioSource.volume = data.volume * _bgmMasterVolume * _masterVolume;

        // 再生する
        bgmAudioSource.Play();
    }

    /// <summary>
    /// SEを再生する関数
    /// </summary>
    /// <param name="se"> SEのタイトル(列挙型) </param>
    public void PlaySESound(SEData.SE se)
    {
        // 音声データから該当するデータを保存する
        SEData data = _settingSystem.seSoundDatas.Find(data => data.se == se);

        // SEの音量を設定する
        seAudioSource.volume = data.volume * _seMasterVolume * _masterVolume;

        // SEを再生する
        seAudioSource.PlayOneShot(data.audioClip);
    }

    /// <summary>
    /// 音が再生しているかを調べる関数
    /// </summary>
    /// <param name="audioSource"> 調べたいAudioSource </param>
    /// <returns> 音が再生中だったらfalse / 音が止まっていたらtrue </returns>
    public bool CheckPlaySound(AudioSource audioSource)
    {
        if (!audioSource.isPlaying)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    ///  再生中の音を止める関数
    /// </summary>
    /// <param name="audioSource"> 止めたいAudioSource </param>
    public void StopSound(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    /// <summary>
    /// BGMの音量を変更する関数
    /// </summary>
    /// <param name="soundVolume"> 変更したい音量 </param>
    public void ChangeBGMVolume(float soundVolume)
    {
        bgmAudioSource.volume = soundVolume * _bgmMasterVolume * _masterVolume;
    }

    /// <summary>
    /// SEの音量を変更する関数
    /// </summary>
    /// <param name="soundVolume"> 変更したい音量 </param>
    public void ChangeSEVolume(float soundVolume)
    {
        seAudioSource.volume = soundVolume * _seMasterVolume * _masterVolume;
    }

    #endregion ---Methods---
}
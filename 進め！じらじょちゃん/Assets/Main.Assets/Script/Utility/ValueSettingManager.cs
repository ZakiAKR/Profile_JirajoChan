using System;
using System.Collections.Generic;
using UnityEngine;

// 作成者：山﨑晶
// メインゲームに使用する値をInspector上で編集できるTable作成

[CreateAssetMenu]
public class ValueSettingManager : ScriptableObject
{
    #region ---Fields---

    // === Player ===
    [Header("=== PLAYER MOCOPI ===")]
    /// <summary>
    /// 足踏みで動かしたときのプレイヤーの歩く力の値
    /// </summary>
    [Range(1, 100), Tooltip("足踏みで動かしたときのプレイヤーの歩く力の値")]
    public int movePower = 1;

    /// <summary>
    /// 足踏みを認識する境目の値
    /// </summary>
    [Range(0f, 5f), Tooltip("足踏みを認識する境目の値")]
    public float walkBorder = 0.2f;

    /// <summary>
    /// しゃがみを認識する境目の値
    /// </summary>
    [Range(0f, 10f), Tooltip("しゃがみを認識する境目の値")]
    public float downBorder = 0.7f;

    /// <summary>
    /// 足踏みをしたときの歩いている時間の値
    /// </summary>
    [Range(0, 100), Tooltip("足踏みをしたときの歩いている時間の値")]
    public int activeMoveTime = 4;

    /// <summary>
    /// プレイヤーが回転するスピード
    /// </summary>
    [Header("=== PLAYER JOYSTIC ===")]
    [Range(0f, 10f), Tooltip("プレイヤーが回転するスピード")]
    public float PlayerRotateSpeed = 0.5f;

    /// <summary>
    /// プレイヤーが移動するスピード
    /// </summary>
    [Range(0f, 10f), Tooltip("プレイヤーが移動するスピード")]
    public float JOYSTIC_PlayerMoveSpeed = 0.5f;

    [Header("=== PLAYER CAMERA ===")]
    /// <summary>
    /// カメラの観客を非表示にする範囲
    /// </summary>
    [Range(0f, 100f), Tooltip("カメラの観客を非表示にする範囲")]
    public float cameraHitRadio = 1.0f;

    /// <summary>
    /// ゲームクリア後にステージを見上げる速さ
    /// </summary>
    [Range(0f,10f),Tooltip("ゲームクリア時にステージを見上げる速さ")]
    public float stageLookCamera = 1.0f;

    // === NPC ===
    [Header("=== NPC ===")]
    /// <summary>
    /// 目的地に到達するまでの時間
    /// </summary>
    [Range(0f, 10f), Tooltip("_posへ到達するまで何秒の変数。値が小さいほど、_target に速く到達")]
    public float smoothTime = 1.0f;

    // === GuardMan ===
    [Header("=== GUARDSMAN ===")]
    /// <summary>
    /// 警備員のSpeed
    /// </summary>
    [Range(0f, 100f), Tooltip("警備員のNavMeshAgent->Speedを設定する")]
    public float guardMoveSpeed = 2f;

    /// <summary>
    /// 警備員のAngularSpeed
    /// </summary>
    [Range(0f, 1000f), Tooltip("警備員のNavMeshAgent->AngularSpeedを設定する")]
    public float guardAngularSpeed = 120f;

    /// <summary>
    /// 警備員のAcceleration
    /// </summary>
    [Range(0f, 100f), Tooltip("警備員のNavMeshAgent->Accelerationを設定する")]
    public float guardAcceleration = 8f;

    // === Audio ===
    [Header("=== AUDIO ===")]
    /// <summary>
    /// BGM/SEを含む全体の音量
    /// </summary>
    [Range(0f, 1f), Tooltip("BGM/SEを含む全体の音量")]
    public float masterVolume = 1;

    /// <summary>
    /// BGMの全体の音量
    /// </summary>
    [Range(0f, 1f), Tooltip("BGMの全体の音量")]
    public float bgmMasterVolume = 1;

    /// <summary>
    /// SEの全体の音量
    /// </summary>
    [Range(0f, 1f), Tooltip("SEの全体の音量")]
    public float seMasterVolume = 1;

    /// <summary>
    /// BGMの音声データ
    /// </summary>
    [Tooltip("BGMの音声データ")]
    public List<BGMData> bgmSoundDatas;

    /// <summary>
    /// SEの音声データ
    /// </summary>
    [Tooltip("SEの音声データ")]
    public List<SEData> seSoundDatas;

    // === MainGameUI ===
    [Header("=== UI TIMER ===")]
    /// <summary>
    /// メインゲームの制限時間
    /// </summary>
    [Range(0f, 180f), Tooltip("制限時間")]
    public float GameLimitTime = 128f;

    // === InGame ===
    [Header("=== IN GAME ===")]
    /// <summary>
    /// ゲームオーバーの判定
    /// </summary>
    public bool gameOver = false;

    /// <summary>
    /// ゲームクリアの判定
    /// </summary>
    //[HideInInspector]
    public bool gameClear = false;

    #endregion ---Fields---
}

#region ---Class---

/// <summary>
/// BGMの音声データクラス
/// </summary>
[Serializable]
public class BGMData
{
    /// <summary>
    /// BGMラベル
    /// </summary>
    public enum BGM
    {
        Title,
        Tutorial,
        Main,
        ClearEnd,
        OverEnd,
    }

    /// <summary>
    /// BGMラベルの宣言
    /// </summary>
    public BGM bgm;

    /// <summary>
    /// BGMのAudioClip
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// BGMの音量
    /// </summary>
    [Range(0f, 1f)]
    public float volume = 1;
}

/// <summary>
/// SEの音声データクラス
/// </summary>
[Serializable]
public class SEData
{
    /// <summary>
    /// SEラベル
    /// </summary>
    public enum SE
    {
        Audience,
        Shutters,
        Buzzer,
        ClickButton,
        FoundSecurity,
        Correct,
        Squwat,
        Walk,
        WalkMini,
        WalkVoice,
        BodyDownVoice,
        KakiwakeVoice,
        Cheer,
        Shout,
        MaoShout1,
        MaoShout2,
        MaoShout3,
        RanShout1,
        RanShout2,
        RanShout3,
    }

    /// <summary>
    /// SEラベルの宣言
    /// </summary>
    public SE se;

    /// <summary>
    /// SEのAudioClip
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// SEの音量
    /// </summary>
    [Range(0f, 1f)]
    public float volume = 1;
}

#endregion ---Class---
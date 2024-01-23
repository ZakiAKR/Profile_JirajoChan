using UnityEngine;
using UnityEngine.SceneManagement;

// 作成者：山崎晶
// アイドル紹介画面のボタンを押してタイトル画面に戻るソース

public class IdolSystemManager : MonoBehaviour
{
    /// <summary>
    /// system_Audioのスクリプト
    /// </summary>
    [SerializeField]
    private AudioManager _audioSystem;

    /// <summary>
    /// 画面を遷移する判定
    /// </summary>
    private bool _isTrans;

    private void Start()
    {
        // 遷移する判定をオフにする
        _isTrans = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // Aボタンを押した場合
        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space))
        {
            // クリック音を再生
            _audioSystem.PlaySESound(SEData.SE.ClickButton);

            // BGMを止める
            _audioSystem.StopSound(_audioSystem.bgmAudioSource);

            // 遷移する判定をオンにする
            _isTrans = true;
        }

        // 遷移する判定がオンで、音が鳴り終わった場合
        if (_isTrans && _audioSystem.CheckPlaySound(_audioSystem.seAudioSource))
        {
            // タイトル画面に遷移する
            SceneManager.LoadScene(0);
        }
    }
}

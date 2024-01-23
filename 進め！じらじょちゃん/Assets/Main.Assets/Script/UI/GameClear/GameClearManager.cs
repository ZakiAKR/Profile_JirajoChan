using UnityEngine;
using UnityEngine.SceneManagement;

// 作成者：山﨑晶
// ゲームクリア画面のUI演出のソースコード

public class GameClearManager : MonoBehaviour
{
    /// <summary>
    /// 遷移するまでの時間
    /// </summary>
    [SerializeField]
    private float _transTime = 600f;

    /// <summary>
    /// 時間を保存する値
    /// </summary>
    private float time;

    private void Update()
    {
        // 時間を測定
        time += Time.deltaTime;

        // 時間が_transTimeより長くなった場合
        if (time >= _transTime)
        {
            // タイトル画面に遷移する
            SceneManager.LoadScene(0);
        }

        // ジョイコンのRボタンとAボタンが押された場合
        if (Input.GetKey(KeyCode.JoystickButton14))
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                // タイトル画面に遷移する
                SceneManager.LoadScene(0);
            }
        }
    }
}
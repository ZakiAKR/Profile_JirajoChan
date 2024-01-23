using UnityEngine;

// アニメーション終了時に実行される

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] ValueSettingManager _settingManager;

    // _settingManager.gameOverをtrueにしてGameOverSceneに遷移
    public void GoGameOver()
    {
        _settingManager.gameOver = true;
    }
}
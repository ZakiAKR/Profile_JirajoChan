using UnityEngine;

// �A�j���[�V�����I�����Ɏ��s�����

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] ValueSettingManager _settingManager;

    // _settingManager.gameOver��true�ɂ���GameOverScene�ɑJ��
    public void GoGameOver()
    {
        _settingManager.gameOver = true;
    }
}
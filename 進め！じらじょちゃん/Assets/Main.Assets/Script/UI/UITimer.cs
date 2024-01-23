using UnityEngine;
using UnityEngine.UI;

//�@�쐬�Ғn����
//�@���Ԑ���UI

public class UITimer : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// Slider�I�u�W�F�N�g�ϐ�
    /// </summary>
    [Tooltip("���Ԑ�����UI���A�^�b�`")]
    [SerializeField] Slider timeSlider;

    // �X�N���v�g�Q�ƕϐ�
    [SerializeField]�@ValueSettingManager settingManager;
    [SerializeField] OutGameManager gameManager;

    /// <summary>
    /// ���Ԑ����ϐ�
    /// </summary>
    float maxTime;

    #endregion ---Fields---

    #region ---Methods---

    void Start()
    {
        maxTime = settingManager.GameLimitTime;

        timeSlider = GetComponent<Slider>();

        //�X���C�_�[�̍ő�l�̐ݒ�
        timeSlider.maxValue = maxTime;
    }

    void Update()
    {
        //�X���C�_�[�̌��ݒl�̐ݒ�
        timeSlider.value += Time.deltaTime;

        if (timeSlider.value == maxTime&& !settingManager.gameClear)
        {
            // �Q�[���I�[�o�[�̔����true�ɂ���
            settingManager.gameOver = true;
            //Debug.Log("���Ԃł�");
        }
        else if (timeSlider.value == maxTime && settingManager.gameClear)
        {
            gameManager.GameClear();
        }
    }
    #endregion ---Methods---
}
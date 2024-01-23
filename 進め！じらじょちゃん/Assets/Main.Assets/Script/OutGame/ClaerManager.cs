using UnityEngine;

// 作成者：山﨑晶 
// ステージの最善にあるゲートと接触したときにゲームクリアにする処理

public class ClaerManager : MonoBehaviour
{
    // 値を参照するために取得する変数
    [SerializeField]
    private ValueSettingManager _settingSystem;

    //  クリア判定と当たった場合
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //  ゲームクリアの判定をする
            _settingSystem.gameClear = true;
        }
    }
}
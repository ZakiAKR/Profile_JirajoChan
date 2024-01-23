using UnityEngine;

// 作成者：山﨑晶
// ゲームクリア後に周辺の観客を消す処理

public class ClearAreaManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 触れているオブジェクトがアクティブ　かつ　観客のタグがついている場合
        if (other.gameObject.activeSelf&&other.gameObject.CompareTag("Audience"))
        {
            // 表示をオフにする
            other.gameObject.SetActive(false);
        }
    }
}

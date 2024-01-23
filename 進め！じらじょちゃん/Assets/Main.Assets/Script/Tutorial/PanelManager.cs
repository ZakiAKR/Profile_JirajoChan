using UnityEngine;

// Bボタンを押したらパネルを閉じる

public class PanelManager : MonoBehaviour
{
    /// <summary>
    /// エリアに入ったら
    /// </summary>
    bool flag = true;

    void OnEnable()
    {
        flag = true;
    }

    void Update()
    {
        if(Input.GetKeyDown (KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.B))
        {
            if(flag)
            {
                gameObject.SetActive(false);
                flag = false;
            }
        }
    }
}
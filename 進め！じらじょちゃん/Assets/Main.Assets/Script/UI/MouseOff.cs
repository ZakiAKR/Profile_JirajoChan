using UnityEngine;

// 作成者：山﨑晶
// ゲーム中にマウスカーソルを非表示にするソース

public class MouseOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // マウスを非表示にする
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // SLボタンもしくはエスケープキーを押した場合
        if (Input.GetKeyDown(KeyCode.JoystickButton5) ||Input.GetKeyDown(KeyCode.Escape))
        {
            // マウスが非表示の場合
            if (!Cursor.visible)
            {
                // マウスを表示する
                Cursor.visible = true;

                // カーソルを自由に動かせるようにする
                Cursor.lockState = CursorLockMode.None;
            }

            // マウスが表示されていた場合 
            if (Cursor.visible)
            {
                // 非表示にする
                Cursor.visible = false;
            }
        }
    }
}

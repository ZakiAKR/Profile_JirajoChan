using UnityEngine;
using UnityEngine.SceneManagement;

// 作成者：山﨑晶 
// シーン遷移のソースコード

public class TranstionScenes : MonoBehaviour
{
    // シーン遷移
    public void Trans_Scene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // リトライ
    public void Trans_Retry()
    {
        SceneManager.LoadScene(2);
    }

    // ゲーム終了   
    public void Trans_EndGame()
    {
        Application.Quit();
    }
}

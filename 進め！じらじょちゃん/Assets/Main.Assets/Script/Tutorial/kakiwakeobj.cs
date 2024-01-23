using UnityEngine;

// コライダーに入ってきたらリストに追加する

public class kakiwakeobj : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // タグ：Enemy
        if (other.gameObject.tag == "Enemy")
        {
            kakiwakeManager.hitolist.Add(other.gameObject);
        }
    }
}
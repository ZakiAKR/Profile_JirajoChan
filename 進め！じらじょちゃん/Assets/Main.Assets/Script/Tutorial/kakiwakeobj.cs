using UnityEngine;

// �R���C�_�[�ɓ����Ă����烊�X�g�ɒǉ�����

public class kakiwakeobj : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // �^�O�FEnemy
        if (other.gameObject.tag == "Enemy")
        {
            kakiwakeManager.hitolist.Add(other.gameObject);
        }
    }
}
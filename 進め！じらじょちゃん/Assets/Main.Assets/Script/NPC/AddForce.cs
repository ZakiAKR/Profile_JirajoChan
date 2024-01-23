using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ϋq��e����΂����߂̃X�N���v�g�B�d�l�ύX�̂��ߖ�����

public class AddForce : MonoBehaviour
{
    Rigidbody _rb;

    [SerializeField] Vector3 force = new Vector3(10.0f, 10.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Debug.Log("aaaaa");
            _rb.AddForce(force, ForceMode.Impulse);
        }
    }
}

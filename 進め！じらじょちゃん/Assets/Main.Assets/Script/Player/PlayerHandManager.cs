using UnityEngine;

public class PlayerHandManager : MonoBehaviour
{
    /// <summary>
    /// ��ɓ����������̉��擾
    /// </summary>
    [Tooltip("��ɓ����������̉��}��")]
    [SerializeField] AudioClip _hit;

    AudioSource _audioSource;

    bool _SEflag = true;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Audience")
        {
            if (_SEflag)
            {
                _audioSource.PlayOneShot(_hit);
                //Debug.Log("��������");
                _SEflag = false;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Audience"))
        {
            _SEflag = true;
            //Debug.Log("�o��");
        }
    }
}
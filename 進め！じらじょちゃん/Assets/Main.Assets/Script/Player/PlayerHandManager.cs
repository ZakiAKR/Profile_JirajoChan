using UnityEngine;

public class PlayerHandManager : MonoBehaviour
{
    /// <summary>
    /// è‚É“–‚½‚Á‚½‚Ì‰¹æ“¾
    /// </summary>
    [Tooltip("è‚É“–‚½‚Á‚½‚Ì‰¹‘}“ü")]
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
                //Debug.Log("“–‚½‚Á‚½");
                _SEflag = false;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Audience"))
        {
            _SEflag = true;
            //Debug.Log("o‚½");
        }
    }
}
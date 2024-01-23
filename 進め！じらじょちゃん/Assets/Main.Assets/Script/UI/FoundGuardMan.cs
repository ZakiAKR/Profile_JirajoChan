using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoundGuardMan : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject guardman;

    [SerializeField]
    private float radius;

    private RectTransform pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = this.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}

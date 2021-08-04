using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private Text tex;
    private AI ai;

    // Start is called before the first frame update
    void Start()
    {
        tex = GetComponent<Text>();
        GameObject _ai = GameObject.FindWithTag("Player");
        ai = _ai.GetComponent<AI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

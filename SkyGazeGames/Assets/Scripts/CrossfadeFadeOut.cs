using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossfadeFadeOut : MonoBehaviour
{
    CanvasGroup cg;
    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cg.alpha > 0)
        {
            cg.alpha -= Time.deltaTime;
        }
    }
}

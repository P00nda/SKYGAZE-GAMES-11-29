using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTrigger : MonoBehaviour
{
    public string sceneNameToLoad;

    CanvasGroup crossfadeImage;
    bool fading = false;
    bool doneFading = false;

    private void Start()
    {
        fading = false;
        doneFading = false;
        crossfadeImage = GameObject.Find("LevelLoader/Crossfade/Image").GetComponent<CanvasGroup>();
        crossfadeImage.alpha = 0;
    }

    private void Update()
    {
        if (fading)
        {
            crossfadeImage.alpha += Time.deltaTime;

            if(crossfadeImage.alpha >= 1)
            {
                fading = false;
                doneFading = true;
                SceneManager.LoadScene(sceneNameToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            fading = true;
        }
    }

}

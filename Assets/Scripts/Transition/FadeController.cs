using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Animator anim;
    public SceneLoadManager sceneLoadManager;

    private bool isPlaying = false;

    private void Awake()
    {
        sprite.enabled = false;
        isPlaying = false;
    }

    public void PlayFadeAnim()
    {
        if(isPlaying)
        {
            return;
        }

        isPlaying = true;
        sprite.enabled = true;

        anim.Play("Fade", 0, 0f);
    }

    public void ReloadCurScene()
    {
        sceneLoadManager.DoReload();
    }

    public void PlayFinished()
    {
        sprite.enabled = false;
        isPlaying = false;
    }
}

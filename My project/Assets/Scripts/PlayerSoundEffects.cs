using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour
{
    [SerializeField] AudioSource src;
    [SerializeField] AudioClip sfxJump, sfxSlide,sfxDa�o,sfxCollect;

    public void Jump()
    {
        src.clip = sfxJump;
        src.Play();
    }

    public void Collect()
    {
        src.clip = sfxCollect;
        src.Play();
    }

    public void Slide()
    {
        src.clip = sfxSlide;
        src.Play();
    }

    public void Da�o()
    {
        src.clip = sfxDa�o;
        src.Play();
    }

}

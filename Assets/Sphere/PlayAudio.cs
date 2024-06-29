using System.Collections;
using System.Collections.Generic;
using UnityEngine; // 需要在Unity中添加AudioSource组件
using UnityEngine.UI;

public class PlayAudio : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource组件
    public AudioClip audioClip; // 音频资源

    public Slider slider;

    private void Update()
    {
        audioSource.volume = slider.value; // 控制音量
    }
    public void ClickBtnPlay()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void ClickBtnPause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }
}

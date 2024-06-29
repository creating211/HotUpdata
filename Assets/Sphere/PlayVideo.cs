using UnityEngine;
using UnityEngine.UI; // 需要在Unity中添加Video Player 组件
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public void ClickBtnPlay()
    {
        if (!videoPlayer.isPlaying)
        {
            videoPlayer.Play(); // 播放视频
        }
    }

    public void ClickBtnPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();  // 暂停视频
        }
    }
}

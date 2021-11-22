using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineController : MonoBehaviour
{
    public List<PlayableDirector> playableDirectors;
    //public List<TimelineAsset> timeLines;

    public void PlayFromDirectors(int index)
    {
        
        playableDirectors[index].Play();
    }


}

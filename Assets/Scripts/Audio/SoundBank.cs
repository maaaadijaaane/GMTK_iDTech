using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundBank", menuName = "Custom/Sound Bank")]
public class SoundBank : ScriptableObject
{
    public List<AudioClip> blockHits;

    public AudioClip levelBGM;
    public AudioClip checkpointBase;
    public AudioClip checkpointStrings;
    public AudioClip checkpointFlute;
    public AudioClip checkpointHarp;
    public AudioClip checkpointSpook;
}

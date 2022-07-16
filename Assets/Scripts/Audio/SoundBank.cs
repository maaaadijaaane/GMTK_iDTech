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

    public GameObject levelBGM;
    public GameObject checkpointBase;
    public GameObject checkpointFlute;
    public GameObject checkpointHarp;
    public GameObject checkpointSpook;
}

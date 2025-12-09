using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetSettings", menuName = "AssetSettings")]
public class AssetSettings : ScriptableObject
{
    public AudioClip BackgroundMusic;

	public Sprite[] SoundBtnsSprites;
}

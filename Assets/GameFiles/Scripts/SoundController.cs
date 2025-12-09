using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    private void Awake()
    {
        instance = this;

        _isMuted = false;
    }

    private void Start()
    {
        if (GameFlowController.instance.AssetSettings != null)
        {
            GetComponent<AudioSource>().clip = GameFlowController.instance.AssetSettings.BackgroundMusic;
        }
        
        GetComponent<AudioSource>().Play();
    }

    public static SoundController instance;

    [SerializeField] private GameObject _soundBtn;
    private bool _isMuted;

    [SerializeField] private AudioMixer _audioMixer;

    
    public void SoundBtnPress()
    {
        if (_isMuted)
        {
            UnMuteMaster();
            _soundBtn.GetComponent<Image>().sprite = GameFlowController.instance.AssetSettings.SoundBtnsSprites[0];
        }

        else
        {
            MuteMaster();
            _soundBtn.GetComponent<Image>().sprite = GameFlowController.instance.AssetSettings.SoundBtnsSprites[1];
        }

        _isMuted = !_isMuted;
    }

    
    public void MuteMaster()
    {
        _audioMixer.SetFloat("MyMasterVolume", -80f);
    }

    
    public void UnMuteMaster()
    {
        _audioMixer.SetFloat("MyMasterVolume", -5f);
    }
}

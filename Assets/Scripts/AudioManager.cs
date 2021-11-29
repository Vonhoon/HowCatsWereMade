using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //오디오 매니저 - 사운드 추가는 퍼블릭 메소드로 하고 외부에선 게임매니저 인스턴스를 통해 참조: GameManager.Instance.GetComponent<AudioManager>()
    public List<AudioClip> audios;
    public AudioClip clear;
    public AudioSource audioPlayer;
    public List<AudioClip> sFXs;
    public List<AudioClip> voices;
    private int pickNo;
    private int dropNo;
    private int talkNo;
    private bool fading;
    private float timer;

    private void Update()
    {
        if(fading)
        {
            timer += 0.3f * Time.deltaTime;
            audioPlayer.volume = Mathf.Lerp(audioPlayer.volume, 0, timer);
            if(timer > 1)
            {                
                fading = false;
            }
        }
    }

    public void PlayAudio(int number) //bgm, 현재 스테이지 번호를 받아서 해당 인덱스 클립을 재생함
    {
        fading = false;
        audioPlayer.clip = audios[number - 1];
        audioPlayer.volume = 0.5f;
        audioPlayer.loop = true;
        audioPlayer.Play();
    }

    public void StopBGM()
    {
        fading = true;
    }

    public void StageClear()
    {
        fading = false;
        audioPlayer.volume = 0.5f;
        audioPlayer.loop = false;
        audioPlayer.clip = clear;
        audioPlayer.Play();
    }

    public void Pick()
    {
        pickNo = Random.Range(0, 2);
        audioPlayer.PlayOneShot(sFXs[pickNo], 0.6f);
    }

    public void Drop()
    {
        dropNo = Random.Range(2, 4);
        audioPlayer.PlayOneShot(sFXs[dropNo], 0.6f);
    }

    public void Talk()
    {
        talkNo = Random.Range(0, voices.Count);
        audioPlayer.PlayOneShot(voices[talkNo], 0.4f);
    }    
}

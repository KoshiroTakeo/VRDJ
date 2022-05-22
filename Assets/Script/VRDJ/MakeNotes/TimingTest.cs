using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingTest : MonoBehaviour
{
    private AudioSource NotesSE;        //音楽データ

    private void Start()
    {
        NotesSE = GameObject.Find("MusicSE").GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)        //コライダー内にノーツが入った時
    {
        Debug.Log("タップ");

        NotesSE.Play();

    }
}

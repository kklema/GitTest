using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class AlarmZone : MonoBehaviour
{
    private AudioSource _audioAlarm;
    private AlarmLamp _colorAlarm;
    private Collider2D _enemyCollider;

    private bool _coroutineIslooped;

    private void Awake()
    {
        _enemyCollider = GameObject.FindObjectOfType<Enemy>().GetComponent<Collider2D>();
    }

    private void Start()
    {
        _audioAlarm = GetComponent<AudioSource>();
        _colorAlarm = GetComponentInChildren<AlarmLamp>();
        _audioAlarm.volume = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == _enemyCollider)
        {
            _audioAlarm.Play();
            _coroutineIslooped = true;
            StartCoroutine(ChangeAlarmVolume());
            _colorAlarm.StartChangeColor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == _enemyCollider)
        {
            _audioAlarm.Stop();
            _coroutineIslooped = false;
            StopCoroutine(ChangeAlarmVolume());
            _audioAlarm.volume = 0;
            _colorAlarm.StopChangeColor();
        }
    }

    private IEnumerator ChangeAlarmVolume()
    {
        while (_coroutineIslooped)
        {
            float maxVolume = 1f;
            float minVolume = 0f;
            float speedOfVolumeChange = 1f;
            float passedTime = 0;

            while (speedOfVolumeChange > passedTime)
            {
                Debug.Log("Корутина звука включена");

                passedTime += Time.deltaTime;
                float lerpPercentage = passedTime / speedOfVolumeChange;

                if (_audioAlarm.isPlaying)
                {
                    _audioAlarm.volume = Mathf.Lerp(minVolume, maxVolume, lerpPercentage);

                    if (_audioAlarm.volume == maxVolume)
                    {
                        _audioAlarm.volume = minVolume;
                    }

                    yield return null;
                }
            }
        }
    }
}
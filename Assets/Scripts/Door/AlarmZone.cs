using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class AlarmZone : MonoBehaviour
{
    [SerializeField] private float _speedChange;

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
        _speedChange = 1f;

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
            if (_audioAlarm.isPlaying)
            {
                _audioAlarm.volume = Mathf.PingPong(Time.time, _speedChange);

                yield return null;
            }
        }
    }
}
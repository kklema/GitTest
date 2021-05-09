using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _spawnTime;
    [SerializeField] private Transform[] _spawnPoint;

    private bool _isActive = true;

    private void Start()
    {
        CreateNewObject();
    }

    private void CreateNewObject()
    {
        StartCoroutine(MakeSpawn());
    }

    private IEnumerator MakeSpawn()
    {
        var waitForSeconds = new WaitForSeconds(_spawnTime);

        while (_isActive)
        {
            for (int i = 0; i < _spawnPoint.Length; i++)
            {
                GameObject newObject = Instantiate(_prefab, _spawnPoint[i].transform.position, _spawnPoint[i].transform.rotation);

                if (i > _spawnPoint.Length)
                {
                    i = 0;
                }

                yield return waitForSeconds;
            }
        }
    }
}
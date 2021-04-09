using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _virusPrefab;
    [SerializeField] private float _delay = 2f;
    private bool _spawningON = true;
    void Start()
    {
        // LOOK THAT UP AGAIN
        StartCoroutine(SpawnSystem());
    }

    public void onPlayerDeath()
    {
        _spawningON = false;
    }

    IEnumerator SpawnSystem()
    {
        // forever wait for 2 seconds delay
        // spawn a new virus
        while (_spawningON)
        {
            Instantiate(_virusPrefab,
                new Vector3(x: Random.Range(-10f, 10f), y: 7f, z: 0f),
                Quaternion.identity, this.transform);
            yield return new WaitForSeconds(_delay);
        }
        Destroy(this.gameObject);
    }
}

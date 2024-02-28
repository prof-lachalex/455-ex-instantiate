using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabToSpawn;
    GameObject _spawnPoint;

    private int _nbObjects;
    // Start is called before the first frame update
    void Start()
    {
        _nbObjects = 0;
        _spawnPoint = GameObject.Find("SpawnPoint");
        SpawnOneObject();
    }

    private void SpawnOneObject()
    {
        if (_spawnPoint == null)
        {
            Debug.LogError("SpawnPoint non spécifié");
            return;
        }
        if(_prefabToSpawn == null)
        {
            Debug.LogError("Aucun prefab spécifié");
            return;
        }

        _nbObjects++;
        GameObject.Instantiate(_prefabToSpawn, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

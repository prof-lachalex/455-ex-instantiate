using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabToSpawn;
    List<Transform> _spawnPoints;

    private int _nbObjects;
    TextMeshProUGUI _objectCounterText;

    private float _timer;
    [SerializeField]
    private float _spawnTimerInterval;

    // Start is called before the first frame update
    void Start()
    {
        _nbObjects = 0;
        _timer = 0;
        _spawnPoints = new List<Transform>( GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>() );
        if (_spawnPoints.Count == 0)
        {
            Debug.LogError("SceneManager: Aucun Spawn point");
        }

        _objectCounterText = GameObject.Find("ObjectCounterText").GetComponent<TextMeshProUGUI>();
        if(_objectCounterText == null)
        {
            Debug.LogError("SceneManager: TMPro pour le compteur mal ou pas spécifié");
        }
        UpdateObjectCounter();
    }

    private void SpawnOneObject()
    {
        if (_spawnPoints == null || _spawnPoints.Count == 0)
        {
            Debug.LogError("SceneManager::SpawnOneObject Aucun Spawn point");
            return;
        }
        if(_prefabToSpawn == null)
        {
            Debug.LogError("Aucun prefab spécifié");
            return;
        }

        _nbObjects++;
        UpdateObjectCounter();
        int spawnIndex = Random.Range(0, _spawnPoints.Count);

        GameObject.Instantiate(_prefabToSpawn, _spawnPoints[spawnIndex].transform.position, _spawnPoints[spawnIndex].transform.rotation);
    }

    private void UpdateObjectCounter()
    {
        _objectCounterText.text = _nbObjects.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnTimerInterval)
        {
            _timer = 0;
            SpawnOneObject();
        }
    }
}

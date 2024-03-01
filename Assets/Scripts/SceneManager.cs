using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _prefabsToSpawn;
    List<Transform> _spawnPoints;

    private List<int> _nbObjects;
    private List<TextMeshProUGUI> _objectCounterTexts;

    private float _timer;
    [SerializeField]
    private float _spawnTimerInterval;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 0;
        _spawnPoints = new List<Transform>( GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>() );
        if (_spawnPoints.Count == 0)
        {
            Debug.LogError("SceneManager: Aucun Spawn point");
        }

        _objectCounterTexts = new List<TextMeshProUGUI>(GameObject.Find("ObjectCounterCanvas").GetComponentsInChildren<TextMeshProUGUI>());
        if(_objectCounterTexts == null || _objectCounterTexts.Count == 0)
        {
            Debug.LogError("SceneManager: Conteneur des objets TMPro pour les compteurs non spécifié");
        }

        if(_objectCounterTexts.Count != _prefabsToSpawn.Length )
        {
            Debug.LogError("SceneManager: Incohérence entre le nombre de Prefabs et de Compteurs");
        }

        // On peut utiliser _objectCounterTexts.Count car on a validé plus haut que c'est le même nombre que nb Prefabs
        _nbObjects = new List<int>();
        for(int i = 0; i < _objectCounterTexts.Count; ++i) 
            _nbObjects.Add(0);

        // Initialiser tous les compteurs (texte) à zéro
        for(int i = 0; i < _prefabsToSpawn.Length; ++i)
            UpdateObjectCounter(i);
    }

    private void SpawnOneObject()
    {
        if (_spawnPoints == null || _spawnPoints.Count == 0)
        {
            Debug.LogError("SceneManager::SpawnOneObject Aucun Spawn point");
            return;
        }
        if(_prefabsToSpawn == null || _prefabsToSpawn.Length == 0)
        {
            Debug.LogError("Aucun prefab spécifié");
            return;
        }

        // Choix du point de Spawn
        int spawnPointIndex = Random.Range(0, _spawnPoints.Count);
        // Choix de l'objet à Spawn
        int spawnObjectIndex = Random.Range(0, _prefabsToSpawn.Length);

        GameObject.Instantiate(_prefabsToSpawn[spawnObjectIndex], _spawnPoints[spawnPointIndex].transform.position, _spawnPoints[spawnPointIndex].transform.rotation);
        ++_nbObjects[spawnObjectIndex];

        UpdateObjectCounter(spawnObjectIndex);
    }

    private void UpdateObjectCounter(int pObjectIndex)
    {
        if(pObjectIndex >= _objectCounterTexts.Count || pObjectIndex < 0) 
        {
            Debug.LogError("pObjectIndex n'est pas conforme au nombre d'objets");
        }

        _objectCounterTexts[pObjectIndex].text = _nbObjects[pObjectIndex].ToString();
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

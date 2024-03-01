using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    private int _prefabID;
    public int prefabID
    {
        get => _prefabID;
        set => _prefabID = value;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name.Equals("DestroyingPlatform"))
        {
            Destroy(gameObject.transform.parent.gameObject);
            SceneManager.instance.RemoveOneObject(_prefabID);
        }
    }
}

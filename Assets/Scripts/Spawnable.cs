using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name.Equals("DestroyingPlatform"))
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}

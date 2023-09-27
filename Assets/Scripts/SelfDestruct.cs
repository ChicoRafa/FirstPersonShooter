using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the erasing of the decals once some time has passed
/// </summary>
public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //destroy decal
        Destroy(gameObject, 15f);
    }
}

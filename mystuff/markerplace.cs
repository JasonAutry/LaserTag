using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox;

public class markerplace : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject marker;
    // Start is called before the first frame update
    void Start()
    {
        float x = player.transform.position.x;
        float z = player.transform.position.z;
        float y = player.transform.position.y;
        
        Instantiate(marker, new Vector3(x - 10, y, z - 10), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(marker);
    }
}

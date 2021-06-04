using System.Collections;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using System.Collections.Generic;


public class markFactory : Singleton<markFactory>
{/*
    [SerializeField] private GameObject mark;
    [SerializeField] private GameObject player;
    [SerializeField] private AbstractMap map;
    private List<GameObject> spawned;

    // Start is called before the first frame update
    void Start()
    {
        spawned = new List<GameObject>();

        for(int i=0;i<1;i++)
        {
            InstantiateMark(i); 
        }
    }

    private void InstantiateMark(int q)
    {
        
        var instance = Instantiate(mark);
        instance.transform.localPosition = map.GeoToWorldPosition(Conversions.StringToLatLon("32.7312222,-97.1125615"), true);
        spawned.Add(instance);
    }
    // Update is called once per frame
    void Update()
    {

        spawned[0].transform.localPosition = map.GeoToWorldPosition(Conversions.StringToLatLon("32.7312222,-97.1125615"), true);
    }
    */
}

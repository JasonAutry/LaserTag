using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;

public class testingfirebase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://lasers-combat-evolved.firebaseio.com/");
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
            List<string> playerlist = new List<string>();
            reference.Child("games/gameidnumber/team1").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    System.Console.WriteLine("task was a failure");
// return "nothing";
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot levelSnapshot = task.Result;
                    
                    foreach (var player in levelSnapshot.Children) // rules
                    {
                        Debug.LogFormat("Key = {0}", player.Key);  // "Key = rules"
                        foreach (var location in player.Children)         //levels
                        {
                            Debug.LogFormat("Key = {0}", location.Key); //"Key = levelNumber"
                            if (location.Key == "long,lat")
                            {
                                playerlist.Add(location.Value.ToString());
                            }
                        }  // levels
                    } //rules
//# return listoflocations;
                }
            });
            System.Console.WriteLine("q finished adding q");
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine("q qqqqq q");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

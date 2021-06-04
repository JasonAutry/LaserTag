namespace   jason
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using Mapbox.Utils;
    using Mapbox.Unity.Map;
    using Mapbox.Unity.MeshGeneration.Factories;
    using Mapbox.Unity.Utilities;
    using System.Collections.Generic;
    using Firebase;
    using System.Threading;
    using System.Threading.Tasks;
    using Firebase.Database;
    using Firebase.Unity.Editor;
    using System;
    using Mapbox.Unity.Location;

    public class SpawnOnMap123 : MonoBehaviour
    {
        [SerializeField]
        AbstractMap _map;


        string[] _locationStrings;
        Vector2d _locations;

        [SerializeField]
        float _spawnScale = .4f;

        [SerializeField]
        GameObject _markerPrefab;
        [SerializeField] Text text1;
        [SerializeField] Text ScoreBotLeft;
        [SerializeField] Text text2;
        List<GameObject> _spawnedObjects = new List<GameObject>();
        List<string> playerlist2 = new List<string>();
        DatabaseReference basereference;
        DatabaseReference myreference;
        bool _isInitialized;

        ILocationProvider _locationProvider;
        ILocationProvider LocationProvider
        {
            get
            {
                if (_locationProvider == null)
                {
                    _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
                }

                return _locationProvider;
            }
        }
        void Start()
        {

             myreference = GameStatus.Instance.getRef().Child(GameStatus.Instance.getCurRoom().ToString());

            // DatabaseReference InstanceData = GameStatus.Instance.DatabaseRef.Child(GameStatus.Instance.getCurRoom().ToString()) ; 
            // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://lasers-combat-evolved.firebaseio.com/");
            // basereference = FirebaseDatabase.DefaultInstance.RootReference;
            // myreference = basereference.Child("games/gameidnumber");
            myreference.ValueChanged += HandleValueChanged; 
            
        }

        private void Update()
        {
            
            Vector2d currentlocation = LocationProvider.CurrentLocation.LatitudeLongitude;
            //Debug.LogFormat(currentlocation.x.ToString()+","+ currentlocation.y.ToString());
            // look at curTeam/curPlayer/LongLat
          
           myreference.Child(GameStatus.Instance.getTeam().ToString()+"/"+GameStatus.Instance.getPlayerIndex().ToString()+"/long,lat").SetValueAsync(currentlocation.x.ToString()+","+ currentlocation.y.ToString());
            // myreference.Child("1/1"+"/long,lat").SetValueAsync(currentlocation.x.ToString()+","+ currentlocation.y.ToString());


        }
        void HandleValueChanged(object sender, ValueChangedEventArgs args)
        {
            int x1 = 0;
            int x2 = 0;
           
            

    
            if (args.DatabaseError != null)
            {
                Debug.LogError(args.DatabaseError.Message);
                return;

            }

            foreach (GameObject o in _spawnedObjects)
            {
                Destroy(o);
            }
            foreach (DataSnapshot d in args.Snapshot.Child(GameStatus.Instance.getTeam().ToString()).Children)
            {
                string location1 = (string)(d.Child("long,lat").Value);
                //Debug.LogFormat(d.Child("score").Value.ToString());
                x1 += Int32.Parse(d.Child("scores").Value.ToString());
                if (Int32.Parse(d.Child("scores").Value.ToString()) > 0)
                {
                    var marker = Instantiate(_markerPrefab, _map.GeoToWorldPosition(Conversions.StringToLatLon(location1), true), Quaternion.identity);
                    _spawnedObjects.Add(marker);
                }
            }

            int enemyTeam = 0 ;
            if ( GameStatus.Instance.getTeam() == 1 ) 
            {
                enemyTeam = 2; 
            }
            else 
            {
                enemyTeam = 1; 
            }
            foreach (DataSnapshot d in args.Snapshot.Child(enemyTeam.ToString()).Children)
            {

                x2 += Int32.Parse(d.Child("scores").Value.ToString());

            }
            text1.text = x1.ToString();
            text2.text = x2.ToString();
            string s1 = args.Snapshot.Child(GameStatus.Instance.getTeam().ToString()).Child(GameStatus.Instance.getPlayerIndex().ToString()).Child("scores").Value.ToString();
            ScoreBotLeft.text = s1;
            if ( x2 == 0)
            {
                // testing go next scene
                // flag up
                myreference.Child("GameState").SetValueAsync(0);
                myreference.Child("Winner").SetValueAsync(GameStatus.Instance.getTeam());
                //mark game as done
            }
             if (x1 == 0)
            {
                // testing go next scene
                // flag up

                myreference.Child("GameState").SetValueAsync(0);
                myreference.Child("Winner").SetValueAsync(enemyTeam);
                //mark game as done
            }


            if (args.Snapshot.Child("GameState").Value.ToString() == "0")
            {
                //close game
                SceneManager.LoadScene(6);
            }
        }
    }
}


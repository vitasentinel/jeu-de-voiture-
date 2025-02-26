using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if MCR_ADS
using UnityEngine.Advertisements;
#endif 

namespace MCRCreator
{
    public class InitializeAds : MonoBehaviour
    {
        public static InitializeAds instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
        public string gameId = "0000000";

        public bool b_EnableAds = false;
        public bool testMode = true;


        void Awake()
        {
            //Check if instance already exists
            if (instance == null)
                //if not, set instance to this
                instance = this;

            //If instance already exists and it's not this:
            else if (instance != this)
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);
        }

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(gameObject);

            #if MCR_ADS
            if (b_EnableAds)
                Advertisement.Initialize(gameId, testMode);
            #endif
        }
    }
}


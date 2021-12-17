using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    float spawnCD = 0.8f;
    float spawnCDremaining = 5;

    [System.Serializable]
    public class WaveComponent {
        public GameObject enemyPrefabs;
        public int num;
        [System.NonSerialized]
        public int spawned = 0;
    };

    public WaveComponent[] waveComps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool didSpawn = false;
        spawnCDremaining -= Time.deltaTime;
        if (spawnCDremaining < 0)
        {
            spawnCDremaining = spawnCD;

            foreach(WaveComponent wc in waveComps)
            {
                if (wc.spawned < wc.num)
                {
                    wc.spawned++;
                    Instantiate(wc.enemyPrefabs, this.transform.position, this.transform.rotation);
                    didSpawn = true;
                    break;
                }
            }
            if (didSpawn == false)
            {

                if (transform.parent.childCount > 1)
                {
                    transform.parent.GetChild(1).gameObject.SetActive(true);
                }
                else
                {

                }
                Destroy(gameObject);
            }
        }
    }
}

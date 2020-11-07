using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Complete
{
    public class EnemySpawner : NetworkBehaviour
    {
        public GameObject enemyPrefab;
        public int numberOfEnemies;

        private GameObject[] enemies;


        public override void OnStartServer()
        {
            enemies = new GameObject[numberOfEnemies];
            for (int i = 0; i < numberOfEnemies; i++)
            {
                var spawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), 0.0f, Random.Range(-8.0f, 8.0f));
                var spawnRotation = Quaternion.Euler(0.0f, Random.Range(0, 180), 0.0f);
                var enemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);
                enemies[i] = enemy;
                NetworkServer.Spawn(enemy);
            }
        }
    }
}
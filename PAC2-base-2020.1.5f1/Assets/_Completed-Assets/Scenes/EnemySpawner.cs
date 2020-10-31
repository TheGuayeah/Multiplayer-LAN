using Complete;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies;

    private GameManager gameManager;
    private GameObject[] enemies;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    public override void OnStartServer()
    {
        enemies = new GameObject[numberOfEnemies];
        for (int i = 0; i < numberOfEnemies; i++)
        {
            var spawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), 0.0f, Random.Range(-8.0f, 8.0f));
            var spawnRotation = Quaternion.Euler(0.0f, Random.Range(0, 180), 0.0f);
            var enemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);
            enemies[i] = enemy;
            AddToTankList(enemy);
            NetworkServer.Spawn(enemy);
        }
    }

    public override void OnStartClient()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            AddToTankList(enemies[i]);
        }
    }

    private void AddToTankList(GameObject npc)
    {
        TankManager tank = new TankManager();
        // Hacemos referencia al objeto del tanque
        tank.m_Instance = npc;

        // Cambiamos el color del tanque según si es el del usuario o el de otro jugador
        tank.m_PlayerColor = isLocalPlayer ? gameManager.m_PlayerColor : gameManager.m_AiColor;

        // Configuramos los componentes del tanque
        tank.Setup();

        // Añadimos el tanque completamente configurado a la lista del GameManager
        List<TankManager> tempTanks = gameManager.m_Tanks.ToList();
        tempTanks.Add(tank);
        gameManager.m_Tanks = tempTanks.ToArray();

        // Reconfiguramos la lista de objetivos de la cámara
        gameManager.SetCameraTargets();
    }
}
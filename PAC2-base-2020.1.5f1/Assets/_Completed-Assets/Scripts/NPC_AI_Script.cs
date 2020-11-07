using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Complete
{
    public class NPC_AI_Script : NetworkBehaviour
    {
        public float m_ShootingDistance;
        public float m_TerrainXDistance;
        public float m_TerrainZDistance;

        private NavMeshAgent m_navAgent;
        private TankShooting m_tankShootingScript;
        private float m_time;
        private GameManager gameManager;
        private TankManager m_tank;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        }

        void Start()
        {
            m_navAgent = GetComponent<NavMeshAgent>();
            m_tankShootingScript = GetComponent<TankShooting>();
            transform.position = new Vector3(Random.Range(-m_TerrainXDistance, m_TerrainXDistance), 0, Random.Range(-m_TerrainZDistance, m_TerrainZDistance));
            AddToTankList();
        }

        void Update()
        {
            SetPlayerObjective();
        }

        private void SetPlayerObjective()
        {
            var maxDist = Mathf.Infinity;
            GameObject closeTank = null;
            float distance = Mathf.Infinity;
            //GameObject[] tanksInGame = GameObject.FindGameObjectsWithTag("Player");
            foreach (TankManager tank in gameManager.m_Tanks)
            {
                if (tank.m_Instance != gameObject)
                {
                    distance = Vector3.Distance(transform.position, tank.m_Instance.transform.position);
                    if (distance < maxDist)
                    {
                        closeTank = tank.m_Instance.gameObject;
                        maxDist = distance;
                    }
                }
            }
            if (closeTank != null)
            {
                m_navAgent.destination = closeTank.transform.position;
                transform.LookAt(closeTank.transform);
                m_navAgent.stoppingDistance = m_ShootingDistance;
                if (maxDist <= m_ShootingDistance)
                {
                    m_tankShootingScript.enabled = true;
                    m_time += Time.deltaTime;
                    if (m_time >= 2f)
                    {
                        m_tankShootingScript.NPC_Fire();
                        m_time = 0;
                    }
                }
                else m_tankShootingScript.enabled = false;
            }
        }

        private void AddToTankList()
        {
            TankManager tank = new TankManager();
            // Hacemos referencia al objeto del tanque
            tank.m_Instance = gameObject;

            // Cambiamos el color del tanque según si es el del usuario o el de otro jugador
            tank.m_PlayerColor = gameManager.m_AiColor;

            // Configuramos los componentes del tanque
            tank.Setup();

            m_tank = tank;

            // Añadimos el tanque completamente configurado a la lista del GameManager
            List<TankManager> tempTanks = gameManager.m_Tanks.ToList();
            tempTanks.Add(tank);
            gameManager.m_Tanks = tempTanks.ToArray();

            // Reconfiguramos la lista de objetivos de la cámara
            gameManager.SetCameraTargets();
        }

        public void RemoveFromTankList()
        {
            // Eliminamos el tanque completamente configurado a la lista del GameManager
            List<TankManager> tempTanks = gameManager.m_Tanks.ToList();
            tempTanks.Remove(m_tank);
            gameManager.m_Tanks = tempTanks.ToArray();

            // Reconfiguramos la lista de objetivos de la cámara
            gameManager.SetCameraTargets();
        }
    }
}

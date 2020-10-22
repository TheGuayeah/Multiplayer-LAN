using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

namespace Complete
{
    public class TankController : NetworkBehaviour
    {
        private GameManager gameManager;

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        }

        // Start is called before the first frame update
        void Start()
        {
            AddToTankList();
        }

        // Update is called once per frame
        void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }
            else
            {

            }
        }

        /// <summary>
        /// Añadir a la lista Tanks del GameManager para que puedan ser identificados por todos los jugadores
        /// </summary>
        private void AddToTankList()
        {
            TankManager tank = new TankManager();
            // Hacemos referencia al objeto del tanque
            tank.m_Instance = gameObject;

            // Cambiamos el color del tanque según si es el del usuario o el de otro jugador
            tank.m_PlayerColor = isLocalPlayer ? gameManager.m_PlayerColor : gameManager.m_EnemyColor;

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace Complete
{
    public class TankController : NetworkBehaviour
    {
        private GameManager gameManager;
        private InputAction m_FireAction;
        private TankShooting m_tankShooting;
        private TankMovement m_tankMovement;
        private InputAction m_MoveAction;
        private InputAction m_TurnAction;

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
            m_tankShooting = FindObjectOfType<TankShooting>().GetComponent<TankShooting>();
            m_tankMovement = FindObjectOfType<TankMovement>().GetComponent<TankMovement>();
        }

        // Start is called before the first frame update
        void Start()
        {
            AddToTankList();
            if (isLocalPlayer)
            {
                // Unity 2020 New Input System
                // Get a reference to the MultiplayerEventSystem for this player
                EventSystem ev = GameObject.Find("EventSystem").GetComponent<EventSystem>();

                // Find the Action Map for the Tank actions and enable it
                InputActionMap playerActionMap = ev.GetComponent<PlayerInput>().actions.FindActionMap("Tank");
                playerActionMap.Enable();

                // Find the 'Move' action
                m_MoveAction = playerActionMap.FindAction("MoveTank");

                // Find the 'Turn' action
                m_TurnAction = playerActionMap.FindAction("TurnTank");

                // Enable and hook up the events
                m_MoveAction.Enable();
                m_TurnAction.Enable();
                m_MoveAction.performed += m_tankMovement.OnTankMove;
                m_TurnAction.performed += m_tankMovement.OnTankTurn;

                // Find the 'Fire' action
                m_FireAction = playerActionMap.FindAction("Fire");

                m_FireAction.Enable();
                m_FireAction.performed += m_tankShooting.OnFire;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isLocalPlayer)
            {
                
            }
            else
            {
                return;
            }
        }

        private void FixedUpdate()
        {
            if (isLocalPlayer)
            {
                m_tankMovement.Move();
                m_tankMovement.Turn();
            }
            else
            {
                return;
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

using Complete;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class NPC_AI_Script : MonoBehaviour
{
    public float m_ShootingDistance;
    public float m_TerrainXDistance;
    public float m_TerrainZDistance;
    
    private NavMeshAgent m_navAgent;
    private TankShooting m_tankShootingScript;
    private float m_time;
    void Start()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
        m_tankShootingScript = GetComponent<TankShooting>();
        transform.position = new Vector3(Random.Range(-m_TerrainXDistance, m_TerrainXDistance), 0, Random.Range(-m_TerrainZDistance, m_TerrainZDistance));
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
        GameObject[] tanksInGame = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject tank in tanksInGame)
        {
            if (tank.name != transform.name)
            {
                distance = Vector3.Distance(transform.position, tank.transform.position);
                if (distance < maxDist)
                {
                    closeTank = tank;
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
                    m_tankShootingScript.CmdFire();
                    m_time = 0;
                }
            }
            else m_tankShootingScript.enabled = false;
        }
    }
}

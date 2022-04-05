using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    //NavMesh and Basic Movement
    public NavMeshAgent navMeshAgent;
    public float startWaitTime = 4;
    public float timeToRotate = 2;
    public float speedWalk = 6;
    public float speedRun = 9;

    //enemy view angles
    public float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;

    //Enemy Waypoints
    public Transform[] waypoints;
    int m_CurrentWaypointIndex;

    //bools
    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;

    float m_waitTime;
    float m_TimeToRotate;
    bool m_PlayerInRange;
    bool m_PlayerNear;
    bool m_IsPatrol;
    bool m_CaughtPlayer;

    //distracted
    public bool distracted;
    public Transform distractionPoint;
    bool doOnce;
    bool distractSpeed = false;

    Vector3 previous;
    public float velocity;


    

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_PlayerInRange = false;
        m_waitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;
        distracted = false;

        m_CurrentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(waypoints
            [m_CurrentWaypointIndex].position);

    }
        // Update is called once per frame
    void Update()
    {
        EnvironmentView(); 

        if(!m_IsPatrol)
        {
            Chasing();
            Debug.Log("CHASING");
        }
        else
        {
            //if ()
            //{
                Debug.Log("PATROL");
                Patroling();
            //}
        }
    }


    public void FixedUpdate()
    {
        velocity = ((transform.position - previous).magnitude) / Time.fixedDeltaTime;
        previous = transform.position;

        //print(velocity);

    }


    //private void MoveToDistraction()
    //{
    //    //m_PlayerNear = false;
    //    Move(speedWalk);

    //    if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
    //    {
    //        //m_waitTime = startWaitTime;
    //        //m_TimeToRotate = timeToRotate;
    //        Stop();
    //        Debug.Log("Is No Longer Distracted");
    //        distracted = false;
    //        Debug.Log(distracted);
    //        //Chasing();
    //    }
    //}


    private void Chasing()
    {
        m_PlayerNear = false;
        playerLastPosition = Vector3.zero;

        if(!m_CaughtPlayer)
        {
            Move(speedRun);
            navMeshAgent.SetDestination(m_PlayerPosition);
        }
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if(m_waitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                m_IsPatrol = true;
                m_PlayerNear = false;
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_waitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position)>= 2.5f)
                {
                    Stop();
                    m_waitTime -= Time.deltaTime;
                }
            }
        }
    }

    
    private void Patroling()
    {
        Debug.Log(m_PlayerNear);
        if (m_PlayerNear)
        {
            if (m_TimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;

            if (distracted)
            {
                navMeshAgent.SetDestination(distractionPoint.position);
                distractSpeed = true;
            }
            else if (!distracted)
            {
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                distractSpeed = false;
            }

            if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if(m_waitTime <= 0)
                {
                    distracted = false;
                    NextPoint();
                    //Change here
                    Move(speedWalk);
                    m_waitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_waitTime -= Time.deltaTime;
                }
            }
        }
    }


        void Move(float speed)
        {
            Debug.Log("Move");
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = speed;
        }

        void Stop()
        {
            Debug.Log("Stop");
            navMeshAgent.isStopped = true;
            navMeshAgent.speed = 0;
        }

        public void NextPoint()
        {
            Debug.Log("NextPoint");
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }

        void CaughtPlayer()
        {
            m_CaughtPlayer = true;
        }

        void LookingPlayer(Vector3 player)
        {
            Debug.Log("LookingPlayer");
            navMeshAgent.SetDestination(player);
            if (Vector3.Distance(transform.position, player) <= 0.3)
            {
                if (m_waitTime <= 0)
                {
                    m_PlayerNear = false;
                    Move(speedWalk);
                    navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                    m_waitTime = startWaitTime;
                    m_TimeToRotate = timeToRotate;
                }
                else
                {
                    Stop();
                    m_waitTime -= Time.deltaTime;
                }
            }
        }

        void EnvironmentView()
        {
            Debug.Log("EnvironmentView");
            Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
            

            for (int i = 0; i < playerInRange.Length; i++)
            {
                Transform player = playerInRange[i].transform;
                Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
                {
                    float dstToPlayer = Vector3.Distance(transform.position, player.position);
                   if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                    {
                        m_PlayerInRange = true;
                        m_IsPatrol = false;

                    }
                    else
                    {
                        m_PlayerInRange = false;
                    }

                }
                if (Vector3.Distance(transform.position, player.position) > viewRadius)
                {
                    m_PlayerInRange = false;
                }
                if (m_PlayerInRange)
                {
                    m_PlayerPosition = player.transform.position;
                }
            }
        }
}

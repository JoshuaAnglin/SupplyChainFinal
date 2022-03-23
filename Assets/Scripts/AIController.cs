using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AIController : MonoBehaviour,idamage
{
    Vector3 startingPosition;
    Vector3 rand;
    Transform player;
    Transform health;

    float patrolRange = 4f;     // How far you want the enemy to patrol
    float movingTime = 0f;      // Activates when the enemy gets to a point within the patrol range
    int stayFor;                // How long the enemy should stay at its designated point before it moves to another position

    NavMeshAgent NMA;

    // (btw, the speed is declared through the 'NavMeshAgent' component's speed script)

    [SerializeField]
    Component healthbar;
    [SerializeField]
    Text tex;
    [SerializeField]
    int health2 = 70;
    [SerializeField]
    int minhealth = 0;
    [SerializeField]
    int maxhealth = 100;
    [SerializeField]
    GameObject mmm;


    RaycastHit obj;
    [SerializeField]
    float hitrange = 6.0f;
    float lasthit = 0.0f;
    [SerializeField]
    int damage = -10;
    [SerializeField]
    float hitcooldown = 1.0f;

    float gothit = 0.0f;
    float regen = 0;
    float find = 0;
    bool alive = true;
    enum enemyStatus
    {
        Patrol,
        PlayerSighted,
        OtherSighted,
        Dead
    }

    enemyStatus status;

    void Awake()
    {
        startingPosition = transform.position;
        player = FindObjectOfType<BasicStats>().transform;
        NMA = GetComponent<NavMeshAgent>();
        RandomLocation();

        health = transform.GetChild(0);
        stayFor = UnityEngine.Random.Range(5, 11);
    }

    void Start()
    {
        updatehealth();
        tex.text =health2 + "%";
        health.GetChild(0).transform.rotation = new Quaternion(0, 180, 0, 0);
        transform.GetChild(1).GetChild(3).transform.gameObject.SetActive(false);
        alive = true;
    }

    void Update()
    {
        health.LookAt(player);
        status = enemyStatus.Patrol;
        //raycast searching for player
        for (int n = 0; n < 9; n++)
        {
            Vector3 place = transform.forward * n + transform.right * (9 - n);
            float dist = Vector3.Distance(transform.position, transform.position + place);
            //Debug.DrawRay(transform.position, (place) * (5 / dist), Color.yellow, 5.0f);
            if (Physics.Raycast(transform.position, place, out obj, 50 / dist))
            {
                if (obj.transform.GetComponent<idamage>() != null)
                {
                    status = enemyStatus.PlayerSighted;
                    find = Time.time;
                }
            }
        }
        for (int n = 0; n < 9; n++)
        {
            Vector3 place = transform.forward * n - transform.right * (9 - n);
            float dist = Vector3.Distance(transform.position, transform.position + place);
            //Debug.DrawRay(transform.position, (place) * (5 / dist), Color.yellow, 1.0f);
            if (Physics.Raycast(transform.position, place, out obj, 50 / dist))
            {
                if (obj.transform.GetComponent<idamage>() != null)
                {
                    status = enemyStatus.PlayerSighted;
                    find = Time.time;
                }
            }
        }
        if (Physics.Raycast(transform.position, transform.forward, out obj, 5))
        {
            if (obj.transform.GetComponent<idamage>() != null)
            {
                status = enemyStatus.PlayerSighted;
                find = Time.time;
            }
        }

        //raycast searching for player

        //checking local enemies enenmystatus
        if (Distance(transform.position, player.position) < 7 && find + 2 > Time.time) status = enemyStatus.PlayerSighted;
        if (status != enemyStatus.PlayerSighted)
        {
            for (int n = 0; n < mmm.transform.childCount; n++)
            {
                GameObject aaa = mmm.transform.GetChild(n).gameObject;
                if (Vector3.Distance(transform.position, aaa.transform.position) < 15)
                {
                    if (aaa.GetComponent<AIController>().change() == "playersighted")
                        status = enemyStatus.OtherSighted;
                }
            }
        }
        //checking local enemies enenmystatus
        if (health2 <= 0)
        {
            alive = false;
            status = enemyStatus.Dead;
        }
        if (alive == false)
            status = enemyStatus.Dead;

        if (status != enemyStatus.Dead)
            transform.GetChild(1).GetChild(4).transform.Rotate((0-transform.GetChild(1).GetChild(4).transform.rotation.eulerAngles.x)*Time.deltaTime, 0, 0);
        // ENEMY STATE ACTIONS
        switch (status)
        {
            case enemyStatus.Patrol:
                StartCoroutine(MoveAround());
                transform.GetChild(0).GetChild(0).GetChild(2).transform.gameObject.SetActive(false);
                break;

            case enemyStatus.PlayerSighted:
                NMA.destination = player.position;
                transform.GetChild(0).GetChild(0).GetChild(2).transform.gameObject.SetActive(true);
                transform.GetChild(1).GetChild(5).transform.Rotate(0, 180*Time.deltaTime, 0);
                transform.GetChild(1).GetChild(8).transform.Rotate(0, 180 * Time.deltaTime, 0);
                if (Physics.Raycast(transform.position, transform.forward, out obj, hitrange))
                {
                    if (obj.transform.GetComponent<idamage>() != null)
                    {
                        idamage att = obj.transform.GetComponent<idamage>();
                        if (Time.time > lasthit + hitcooldown)
                        {
                            att.addhealth(damage);
                            lasthit = Time.time;
                        }
                    }
                }
                break;
            case enemyStatus.OtherSighted:
                NMA.destination = player.position;
                transform.GetChild(0).GetChild(0).GetChild(2).transform.gameObject.SetActive(true);
                transform.GetChild(1).GetChild(5).transform.Rotate(0, 180 * Time.deltaTime, 0);
                transform.GetChild(1).GetChild(8).transform.Rotate(0, 180 * Time.deltaTime, 0);
                if (Physics.Raycast(transform.position, transform.forward, out obj, hitrange))
                {
                    if (obj.transform.GetComponent<idamage>() != null)
                    {
                        idamage att = obj.transform.GetComponent<idamage>();
                        if (Time.time > lasthit + hitcooldown)
                        {
                            att.addhealth(damage);
                            lasthit = Time.time;
                        }
                    }
                }
                break;
            case enemyStatus.Dead:
                transform.GetChild(0).GetChild(0).GetChild(2).transform.gameObject.SetActive(false);
                if (transform.GetChild(1).GetChild(4).transform.rotation.eulerAngles.x < 45)
                    transform.GetChild(1).GetChild(4).transform.Rotate(45 * Time.deltaTime, 0, 0);
                if (Time.time > regen + 0.5f)
                {
                    health2 += 2;
                    regen = Time.time;
                }
                if (health2 >= maxhealth)
                {
                    alive = true; tex.text = health2 + "%";
                }
                updatehealth();
                break;
        }
    }

    // RETURNS POSITIVE FLOAT OF FIRST AND SECOND
    float Distance(Vector3 first, Vector3 second)
    {
        return Mathf.Abs(Vector3.Distance(first, second));
    }

    // GENERATES RANDOM LOCATION WITHIN THE PATROL AREA
    void RandomLocation()
    {
        rand = new Vector3(UnityEngine.Random.Range(startingPosition.x - patrolRange, startingPosition.x + patrolRange), transform.position.y, UnityEngine.Random.Range(startingPosition.z - patrolRange, startingPosition.z + patrolRange));
    }

    // BEFORE THE 'MOVINGTIME' VARIABLE IS ACTIVATED, THE ENEMY GOES BACK TO WHATEVER POSITION 'RAND' WAS (WORKS WHEN THE ENEMY IS CHANGING POSITIONS
    // & WHEN THE ENEMY GOES BACK TO IT'S POSITION AFTER CHASING THE PLAYER)
    IEnumerator MoveAround()
    {
        Vector3 distance = startingPosition - transform.position;

        if (Vector3.Distance(transform.position, rand) > 0.4f)
        {
            //Debug.Log("Go to: " + rand);
            NMA.destination = rand;
            movingTime += 0;
            yield return null;
        }
        else
        {
            //Debug.Log(movingTime);
            if (movingTime > stayFor)
            {
                movingTime = 0;
                stayFor = UnityEngine.Random.Range(5, 11);
                RandomLocation();
            }
            movingTime += Time.deltaTime;
        }
    }
    public void addhealth(int amount)
    {
        if (status == enemyStatus.Patrol) amount = -maxhealth/2;
        if (status == enemyStatus.Dead) amount = 0;
        if (Time.time > gothit + 0.5f && amount != 0)
        {
            health2 += amount;
            if (health2 < minhealth) health2 = minhealth;
            if (health2 > maxhealth) health2 = maxhealth;
            tex.text = health2 + "%";
            updatehealth();
            gothit = Time.time;
            transform.position = transform.position + player.transform.forward * 2;
        }
    }
    void updatehealth()
    {
        healthbar.transform.localScale = new Vector3((float)(health2 - minhealth) / (maxhealth - minhealth), 1.0f);
    }
    public String change()
    {
        if (status == enemyStatus.PlayerSighted)
            return "playersighted";
        else return "";
    }
}

// OLD SCRIPT

/*[SerializeField] float chaseDistance = 5f;
    [SerializeField] float enemySpeed = 1.2f;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 1f;

    GameObject player;
    Vector3 guardPosition;

    int currentWaypointIndex = 0;

    // Calculates the how far way the enemy is from the player at any point
    public float DistanceToPlayer()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        guardPosition = transform.position;
    }

    private void Update()
    {
        // Checks if the player is within the chaseDistance from the player
        if (DistanceToPlayer() < chaseDistance)
        {
            transform.Translate((player.transform.position - transform.position) * enemySpeed * Time.deltaTime, Space.World);
        }

    }


    private void OnDrawGizmosSelected()
    {
        // Draws a gizmo around a selcted enemy to show the chaseDistance radius more clearly
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }


    private void PatrolBehaviour()
    {
        Vector3 nextPosition = guardPosition;

        if (patrolPath != null)
        {
            if (AtWaypoint())
            {
                CycleWaypoint();
            }
            nextPosition = GetCurrentWayPoint();
        }
    }

    private Vector3 GetCurrentWayPoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        Debug.Log(currentWaypointIndex);
    }

    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
        return distanceToWaypoint < waypointTolerance;
    } */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.VisualScripting;
using System.Runtime.InteropServices.WindowsRuntime;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    private Animator anim;
    LevelGold levelGold;
    GetChased getchased;
    SzeneManager szeneManager;
    PasueMenu pauseMenu;
    MsuicManager musicManager;
    PlayerMovement playerMovement;
    public float walkSpeed = 3;
    public float runSpeed = 5;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public Transform[] patrolPoints;
    public int targetPoint;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    [SerializeField] private GameObject endScreenLose;
    private CharacterController playerController;
    private GameObject playerObj;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool chased = false;
    private bool chaseSound = true;

    // FOV

    public float radius;
    [Range(0,360)]
    public float angle;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    // AudioClips

    [SerializeField] private AudioClip[] chaseClips;
    [SerializeField] private AudioClip[] attackClips;
    [SerializeField] private GameObject StepClip;
    [SerializeField] private GameObject RunClip;


    private void Awake()
    {
        
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FOVRoutine());
        targetPoint = 0;
        anim = GetComponent<Animator>();
        levelGold = GameObject.Find("GM").GetComponent<LevelGold>();
        getchased = GameObject.Find("PostProcessing").GetComponent<GetChased>();
        szeneManager = GameObject.Find("PlayerData").GetComponent<SzeneManager>();
        
    }

    private void Start()
    {
        player = GameObject.Find("Player(Clone)").transform;
        playerObj = GameObject.Find("Player(Clone)");
        playerMovement = GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>();
        playerController = playerObj.GetComponent<CharacterController>();
        pauseMenu = GameObject.Find("GM").GetComponent<PasueMenu>();
        musicManager = GameObject.Find("MusicManager").GetComponent<MsuicManager>();
    }

    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        
        if (!playerInSightRange && !playerInAttackRange && !chased) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
        if (!playerInSightRange && !playerInAttackRange && chased) StartCoroutine(TurnAround());


    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    StartCoroutine(CheckSphere());;

                
            }
           
        }
       
    }

private IEnumerator CheckSphere()
{
    while (true)
    {
        // Check, ob der Spieler im Sichtbereich ist
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        // Wenn der Spieler nicht mehr im Sichtbereich ist, beende die Coroutine
        if (!playerInSightRange)
        {
            yield break;
        }

        // Warte eine Frame, um die Schleife erneut auszuführen
        yield return null;
    }
}


    private void Patroling()
    {
        agent.speed = walkSpeed + szeneManager.raid * 2;
        anim.SetBool("Chasing", false);
        if (!walkPointSet) 
        {
            SearchWalkPoint();
        }

        else if(walkPointSet)
        agent.SetDestination(walkPoint);
        

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        if (distanceToWalkPoint.magnitude < 1f)
        {
        walkPointSet = false;
        targetPoint++;
        if(targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
        }
        RunClip.SetActive(false);
        StepClip.SetActive(true);

    }

        private IEnumerator TurnAround()
    {
        agent.SetDestination(transform.position);
        anim.SetBool("Chasing", false);
        anim.SetBool("Turning", true);
        getchased.DontChase();
        chaseSound = true;
        RunClip.SetActive(false);
        yield return new WaitForSeconds(5f); 
        anim.SetBool("Turning", false);
        chased = false;
        
    }

    private void SearchWalkPoint()
    {

        walkPoint = new Vector3(patrolPoints[targetPoint].position.x , transform.position.y, patrolPoints[targetPoint].position.z);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        agent.speed = runSpeed + szeneManager.raid * 4;
        chased = true;
        anim.SetBool("Chasing", true);
        anim.SetBool("Turning", false);
        getchased.Chase();
        if(chaseSound)
        {
            chaseSound = false;
            VoicesManager.instance.PlayRandomVoicesFXClip(chaseClips, transform, 1f);
        }
        StepClip.SetActive(false);
        RunClip.SetActive(true);
    }

    public void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            pauseMenu.DisablePauseToggle();
            musicManager.EndAllMusic();
            musicManager.PlayLoseSound();
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            StartCoroutine(DisableController());
            playerMovement.EnableBlackscreen();
            endScreenLose.SetActive(true);
            StartCoroutine(ExecuteAfterDelay());
            VoicesManager.instance.PlayRandomVoicesFXClip(attackClips, transform, 1f);
        }
    }

     private IEnumerator DisableController()
    {
        yield return new WaitForSeconds(0f);
        playerController.enabled = false;
    }
    private IEnumerator ExecuteAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        levelGold.LoseGame();
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);       
    }
}

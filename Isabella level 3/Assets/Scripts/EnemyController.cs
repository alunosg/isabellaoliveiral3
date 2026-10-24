using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private enum EnemyStates {Idle, Chase, Attack, Hit, Death }
    private EnemyStates state = EnemyStates.Idle;

    public NavMeshAgent nav;
    public Animator anim;
    public float chaseDistance = 20f;
    public float attackDistance = 2f;
    public float attackDuration = 2f;

    private PlayerController player;
    private float distance = 999;

    private bool locked = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }    
   
    void Update()
    {
       if (locked) return;

       distance = Vector3.Distance(transform.position, player.transform.position); 

       switch(state)
       {
        case EnemyStates.Idle:
            IdleUpdate();
            break;
        case EnemyStates.Chase:
            ChaseUpdate();
            break;
        case EnemyStates.Attack:
            AttackUpdate();
            break;
        case EnemyStates.Hit:
            HitUpdate();
            break;
        case EnemyStates.Death:
            DeathUpdate();
            break;
       }
    }

    void IdleUpdate()
    {
        if(distance < chaseDistance)
        {
            state = EnemyStates.Chase;
            nav.isStopped = false;
            anim.SetBool("IsRunning", true);
        }
    }    

   void ChaseUpdate()
   {
       if(distance > chaseDistance)
        {
            state = EnemyStates.Idle;
            nav.isStopped = true;
            anim.SetBool("IsRunning", false);
        }
        else if(distance < attackDistance)
        {
            EnterAttack();
        }
        else
        {
            nav.SetDestination(player.transform.position);        
        }
   }

   void AttackUpdate()
   {
        if(distance > chaseDistance)
        {
            state = EnemyStates.Idle;
            nav.isStopped = false;
            anim.SetBool("IsRunning", false);                   
        }
        else if(distance > attackDistance)
        {
            state = EnemyStates.Chase;
            nav.isStopped = false;
            anim.SetBool("IsRunning", true);
        }
        else    
        {
           EnterAttack();
        }        
   }

   void HitUpdate()
   {
       
   }

   void DeathUpdate()
   {

   }

     void Unlock() => locked = false;

     void EnterAttack()
     {
        locked = true;
        nav.isStopped = true;
        anim.SetTrigger("Attack");
        anim.SetBool("IsRunning", false);
        state = EnemyStates.Attack;

        CancelInvoke("Unlock");
        Invoke("Unlock", attackDuration);
     }
}

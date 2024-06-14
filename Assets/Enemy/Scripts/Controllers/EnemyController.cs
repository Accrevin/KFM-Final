using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 100f;

    Transform target;
    NavMeshAgent agent;

    [SerializeField]
    private Animator animator;
    float velocity = 0.0f;
    public float acceleration = 0.1f;
    public float deceleration = 0.5f;
    int velocityHash;
    int stoppingDistanceHash;
   
    Vector3 playerPositionInRange = new Vector3();

    //public ThirdPersonCharacter character;


    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.Instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        velocityHash = Animator.StringToHash("Velocity");
        stoppingDistanceHash = Animator.StringToHash("StoppingDistance");
        //agent.updatePosition = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        bool stoopingDistanceBool = false;
        Debug.Log(velocity);
        if (distance <= lookRadius && velocity <= 1.0f)
        {
            Debug.Log(velocity+"::::::::::::");
            agent.SetDestination(target.position);
            playerPositionInRange = target.position;
            FaceTarget(playerPositionInRange);
            velocity += Time.deltaTime * acceleration;

            if (distance <= agent.stoppingDistance)
            {
                stoopingDistanceBool = true;
                velocity = 0.0f;
            }
        

        }
        else if (distance > lookRadius && Vector3.Distance(playerPositionInRange, transform.position) <= agent.stoppingDistance)
        {

            velocity = 0.0f;
        }
        if (velocity > 1.0f)
        {
            velocity = 1.0f;
        }

        animator.SetBool(stoppingDistanceHash, stoopingDistanceBool);
    
        animator.SetFloat(velocityHash, velocity);
   

    }

    void FaceTarget(Vector3 playerPositionInRange)
    {
        //Vector3 direction = (target.position - transform.position).normalized;
        Vector3 direction = (playerPositionInRange - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}

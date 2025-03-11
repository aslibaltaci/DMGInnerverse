using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AIGuide : MonoBehaviour
{
    public GameObject player;
    public Transform targetLocation;
    public float speed = 2f;
    public float maxDistance = 7f;
    public float followDistance = 3f;
    public float stopDistance = 1.5f;
    public float wanderRadius = 1.5f;
    public float wanderSpeed = 1.5f;
    public float jitterIntensity = 0.5f;

    private bool returningToPlayer = false;
    private bool isWandering = false;
    private Vector2 wanderTarget;
    private PlayableDirector timeline;
    private Vector2 jitterOffset;

    void Start()
    {
        timeline = FindObjectOfType<PlayableDirector>();
        StartCoroutine(UpdateJitter()); 
    }

    void Update()
    {
        if (timeline != null && timeline.state == PlayState.Playing)
        {
            FollowPlayerDuringCutscene();
            return;
        }

        HandleNormalMovement();
    }

    void HandleNormalMovement()
    {
        float playerDistance = Vector2.Distance(transform.position, player.transform.position);
        float targetDistance = Vector2.Distance(transform.position, targetLocation.position);

        if (playerDistance > maxDistance)
        {
            returningToPlayer = true;
            isWandering = false;
        }

        if (returningToPlayer && playerDistance < followDistance)
        {
            returningToPlayer = false;
        }

        bool shouldWander = (!returningToPlayer && playerDistance < followDistance) || (targetDistance < stopDistance);

        if (shouldWander)
        {
            if (!isWandering)
            {
                StartCoroutine(WanderAroundPoint(player.transform.position));
            }
            return;
        }

        Vector2 moveTarget = returningToPlayer ? player.transform.position : targetLocation.position;

        if (targetDistance > stopDistance)
        {
            MoveTowards(moveTarget, speed);
        }
    }

    void FollowPlayerDuringCutscene()
    {
        if (player == null) return;

        float playerDistance = Vector2.Distance(transform.position, player.transform.position);

        if (playerDistance > followDistance)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
        else
        {
            Vector2 jitterPosition = (Vector2)player.transform.position + jitterOffset;
            transform.position = Vector2.Lerp(transform.position, jitterPosition, Time.deltaTime * wanderSpeed);
        }
    }

    void MoveTowards(Vector2 target, float moveSpeed)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    private IEnumerator WanderAroundPoint(Vector2 centerPoint)
    {
        isWandering = true;

        while (Vector2.Distance(transform.position, centerPoint) < followDistance || Vector2.Distance(transform.position, targetLocation.position) < stopDistance)
        {
            Vector2 randomOffset = Random.insideUnitCircle * wanderRadius;
            wanderTarget = centerPoint + randomOffset;

            while (Vector2.Distance(transform.position, wanderTarget) > 0.1f)
            {
                MoveTowards(wanderTarget, wanderSpeed);
                yield return null;
            }
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }

        isWandering = false;
    }

    private IEnumerator UpdateJitter()
    {
        while (true)
        {
            jitterOffset = Random.insideUnitCircle * jitterIntensity;
            yield return new WaitForSeconds(0.2f);
        }
    }
}

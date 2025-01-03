using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMove : LivingEntity
{
    public LayerMask targetLayers;
    public LivingEntity target;
    public float findTarget = 10f;

    private NavMeshAgent agent;
    private Coroutine coUpdatePath;

    public bool HasTarget
    {
        get
        {
            return target != null && !target.IsDead;
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.enabled = true;

        var cols = GetComponents<Collider>();
        foreach (var col in cols)
        {
            col.enabled = true;
        }
    }

    private void Update()
    {
        if (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            return;
        }

        Vector3 newDestination = GetRandomPointOnNavMesh();
        if (agent.isOnNavMesh && agent.enabled)
        {
            agent.SetDestination(newDestination);
        }
    }

    private Vector3 GetRandomPointOnNavMesh()
    {
        float radius = 20f;
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        Debug.LogWarning("Failed to find a valid point on the NavMesh.");
        return transform.position;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (agent && !agent.isOnNavMesh)
        {
            Debug.LogWarning("Agent is not on the NavMesh. Please check the placement.");
            return;
        }

        coUpdatePath = StartCoroutine(CoUpdatePath());
    }

    protected void OnDisable()
    {
        if (coUpdatePath != null)
        {
            StopCoroutine(coUpdatePath);
            coUpdatePath = null;
        }

        if (agent != null && agent.enabled)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        target = null;
    }

    private IEnumerator CoUpdatePath()
    {
        while (true)
        {
            if (!HasTarget)
            {
                agent.isStopped = true;
                target = FindTarget();
            }

            if (HasTarget && agent.enabled && agent.isOnNavMesh)
            {
                agent.isStopped = false;
                agent.SetDestination(target.transform.position);
            }

            yield return new WaitForSeconds(0.25f);
        }
    }

    public LivingEntity FindTarget()
    {
        var start = transform.position - Vector3.down * 2f;
        var end = transform.position + Vector3.up * 2f;
        var cols = Physics.OverlapCapsule(start, end, findTarget, targetLayers.value);
        foreach (var col in cols)
        {
            var livingEntity = col.GetComponent<LivingEntity>();
            if (livingEntity != null && !livingEntity.IsDead)
            {
                return livingEntity;
            }
        }
        return null;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        base.Die();

        if (coUpdatePath != null)
        {
            StopCoroutine(coUpdatePath);
            coUpdatePath = null;
        }

        if (agent.enabled)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        var cols = GetComponents<Collider>();
        foreach (var col in cols)
        {
            col.enabled = false;
        }
    }
}

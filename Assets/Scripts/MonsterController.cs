using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterController : LivingEntity
{
    // Rigidbody와 NavMeshAgent 컴포넌트
    private Rigidbody rb;
    private NavMeshAgent agent;

    // 체력과 회전 상태를 보여주는 슬라이더
    public Slider healthSlider;
    public Slider rotaSlider;

    // 타겟 탐지 관련
    public LayerMask targetLayers;
    public LivingEntity target;
    public float findTarget = 10f;

    // 회전 관련
    private RotateMonster rotate;
    private Coroutine coUpdatePath; // NavMesh 경로 갱신을 위한 코루틴
    private float currentRota; // 현재 회전 속도

    // 몬스터 상태
    public float monsterDamage = 20f; // 몬스터 데미지
    public float monsterAP { get; set; } // 몬스터 각속도
    public bool isDeath; // 사망 여부

    // 타겟 존재 여부 확인
    public bool HasTarget
    {
        get
        {
            return target != null && !target.IsDead;
        }
    }

    private void Awake()
    {
        // Rigidbody와 NavMeshAgent 초기화
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        rotate = GetComponent<RotateMonster>();

        // NavMeshAgent 초기 설정
        agent.updateRotation = false; // NavMeshAgent가 회전을 자동으로 제어하지 않게 설정
        agent.enabled = true;

        isDeath = false; // 사망 상태 초기화

        // 충돌체 활성화
        var cols = GetComponents<Collider>();
        foreach (var col in cols)
        {
            col.enabled = true;
        }
    }

    private void Update()
    {
        // 몬스터의 현재 각속도를 업데이트
        monsterAP = rb.angularVelocity.y;

        // 에이전트가 경로를 따라가고 있는지 확인
        if (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            return;
        }

        // NavMesh 위에서 랜덤 위치 설정
        Vector3 newDestination = GetRandomPointOnNavMesh();
        if (agent.isOnNavMesh && agent.enabled)
        {
            agent.SetDestination(newDestination);
        }
    }

    private Vector3 GetRandomPointOnNavMesh()
    {
        // NavMesh 위에서 랜덤 위치를 찾는 함수
        float radius = 20f; // 탐색 반경
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position; // 유효한 점 반환
        }

        Debug.LogWarning("NavMesh 위에서 유효한 점을 찾지 못했습니다.");
        return transform.position; // 실패 시 현재 위치 반환
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // NavMeshAgent가 NavMesh 위에 있는지 확인
        if (agent && !agent.isOnNavMesh)
        {
            Debug.LogWarning("NavMesh 위에 에이전트가 없습니다. 배치를 확인하세요.");
            return;
        }

        // 경로 갱신 코루틴 시작
        coUpdatePath = StartCoroutine(CoUpdatePath());

        // UI 활성화 및 초기화
        healthSlider.gameObject.SetActive(true);
        rotaSlider.gameObject.SetActive(true);

        healthSlider.maxValue = maxHp;
        healthSlider.value = hp;

        rotaSlider.maxValue = 99f;
        rotaSlider.value = currentRota;
    }

    protected void OnDisable()
    {
        // 코루틴 중지 및 초기화
        if (coUpdatePath != null)
        {
            StopCoroutine(coUpdatePath);
            coUpdatePath = null;
        }

        // NavMeshAgent 비활성화
        if (agent != null && agent.enabled)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        // 타겟 초기화
        target = null;
    }

    private IEnumerator CoUpdatePath()
    {
        // 일정 간격으로 경로를 갱신하는 코루틴
        while (true)
        {
            if (!HasTarget)
            {
                agent.isStopped = true; // 타겟이 없을 경우 멈춤
                target = FindTarget(); // 새 타겟 탐색
            }

            if (HasTarget && agent.enabled && agent.isOnNavMesh)
            {
                agent.isStopped = false; // 타겟이 있을 경우 이동 시작
                agent.SetDestination(target.transform.position);
            }

            yield return new WaitForSeconds(0.25f); // 0.25초마다 갱신
        }
    }

    public LivingEntity FindTarget()
    {
        // 주변에서 타겟을 찾는 함수
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
        return null; // 타겟이 없을 경우 null 반환
    }

    private void FixedUpdate()
    {
        // 현재 회전 속도와 체력을 슬라이더에 반영
        currentRota = monsterAP;
        rotaSlider.value = currentRota;

        healthSlider.value = hp;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        // 데미지를 받을 때 체력 슬라이더를 업데이트
        base.OnDamage(damage, hitPoint, hitNormal);
        healthSlider.value = hp;
    }

    public override void Die()
    {
        // 몬스터가 죽을 때 처리
        base.Die();

        isDeath = true;
        // 경로 갱신 코루틴 중지
        if (coUpdatePath != null)
        {
            StopCoroutine(coUpdatePath);
            coUpdatePath = null;
        }

        // NavMeshAgent 비활성화 및 이동 멈춤
        if (agent.enabled)
        {
            agent.isStopped = true; // 이동 멈춤
            agent.enabled = false;  // NavMeshAgent 비활성화
        }

        // Rigidbody를 Kinematic으로 설정하여 중력 및 물리 효과 차단
        if (rb != null)
        {
            rb.isKinematic = true;  // Kinematic으로 설정하여 물리 영향을 받지 않게 함
        }

        // 충돌체 비활성화
        var cols = GetComponents<Collider>();
        foreach (var col in cols)
        {
            col.enabled = false; // 충돌체 비활성화
        }

        // UI 비활성화
        healthSlider.gameObject.SetActive(false);  // 체력 슬라이더 비활성화
        rotaSlider.gameObject.SetActive(false);    // 회전 슬라이더 비활성화
    }

    public override void AddHp(float add)
    {
        // 체력을 회복할 때 체력 슬라이더 업데이트
        base.AddHp(add);
        healthSlider.value = hp;
    }
}

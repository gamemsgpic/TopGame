using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Rotate rotate;
    private PlayerHealth playerHealth;
    private MonsterController monsterControl;
    public GameObject monster;
    public ParticleSystem hitEffect;



    private float attackTime = 0;
    public float attackDeley = 2f;
    public float damege = 20f;
    public float dotDamage = 1f;

    public float playerAttackVelue { get; private set; }
    public float monsterAttackVelue { get; private set; }

    private bool canAttack = false;


    private void Awake()
    {
        rotate = GetComponent<Rotate>();
        monsterControl = monster.GetComponent<MonsterController>();
        playerHealth = GetComponent<PlayerHealth>();

        if (hitEffect != null)
        {
            hitEffect.Stop();
            SpawnParticleAtContact(gameObject.transform.position);
        }
    }

    private void FixedUpdate()
    {
        playerAttackVelue = rotate.rota;
        monsterAttackVelue = monsterControl.monsterAP;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    canAttack = true;


    //    if (canAttack)
    //    {
    //        if (other.CompareTag("Monster"))
    //        {
    //            var monster = other.GetComponent<LivingEntity>();
    //            if (monster != null && !monster.IsDead)
    //            {
    //                if (hitEffect != null)
    //                {
    //                    hitEffect.transform.position = other.ClosestPoint(transform.position);
    //                    hitEffect.Play();
    //                }
    //                Vector3 enemyPosition = other.transform.position;
    //                Vector3 playerPosition = transform.position;
    //                Vector3 directionToEnemy = (enemyPosition - playerPosition).normalized;
    //                Vector3 enemyUpDirection = other.transform.up;

    //                float yDifference = enemyPosition.y - playerPosition.y;

    //                float angle = Vector3.Angle(enemyUpDirection, directionToEnemy);

    //                if (angle <= 90f && yDifference < 0)
    //                {
    //                    monster.OnDamage((damege * 2), Vector3.zero, Vector3.zero);
    //                    playerHealth.OnDamage((monsterControl.monsterDamage * 0.5f), Vector3.zero, Vector3.zero);
    //                    Debug.Log($"Critical hit! Angle: {angle}, Y Difference: {yDifference}");
    //                }
    //                else
    //                {
    //                    //if (playerAttackVelue > monsterAttackVelue)
    //                    //{
    //                    //    monster.OnDamage(damege, Vector3.zero, Vector3.zero);
    //                    //    playerHealth.OnDamage((monsterControl.monsterDamage * 0.5f), Vector3.zero, Vector3.zero);
    //                    //}
    //                    //else
    //                    //{
    //                    //    monster.OnDamage((damege * 0.5f), Vector3.zero, Vector3.zero);
    //                    //    playerHealth.OnDamage(monsterControl.monsterDamage, Vector3.zero, Vector3.zero);
    //                    //}
    //                }
    //            }
    //        }
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    attackTime += Time.deltaTime;

    //    if (attackTime > attackDeley && canAttack)
    //    {
    //        if (other.CompareTag("Monster"))
    //        {
    //            if (hitEffect != null)
    //            {
    //                hitEffect.transform.position = other.ClosestPoint(transform.position);
    //                if (!hitEffect.isPlaying) // 이미 실행 중인지 확인 // 자꾸 파티클 stop되서 확인
    //                {
    //                    hitEffect.Play();
    //                }
    //            }

    //            var monster = other.GetComponent<LivingEntity>();
    //            if (monster != null && !monster.IsDead)
    //            {
    //                monster.OnDamage(dotDamage, Vector3.zero, Vector3.zero);
    //                playerHealth.OnDamage(dotDamage, Vector3.zero, Vector3.zero);
    //            }
    //        }
    //        attackTime = 0;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            canAttack = true;

            // 이펙트 출력 (첫 충돌)
            if (hitEffect != null)
            {
                hitEffect.transform.position = other.ClosestPoint(transform.position);
                hitEffect.Play();
            }

            // 적에게 큰 데미지 처리
            var monster = other.GetComponent<LivingEntity>();
            if (monster != null && !monster.IsDead)
            {
                Vector3 enemyPosition = other.transform.position;
                Vector3 playerPosition = transform.position;

                // 방향 벡터 계산
                Vector3 directionToEnemy = (enemyPosition - playerPosition).normalized;
                Vector3 enemyUpDirection = other.transform.up;

                // 각도 계산
                float angle = Vector3.Angle(enemyUpDirection, directionToEnemy);
                float yDifference = enemyPosition.y - playerPosition.y; // Y축 비교

                // 디버깅 로그 추가
                //Debug.Log($"Angle: {angle}, Y Difference: {yDifference}");

                // 위에서 내려찍었는지 확인
                if (angle >= 130f && yDifference < 0)
                {
                    // 두배 데미지 처리
                    monster.OnDamage((damege * 2), Vector3.zero, Vector3.zero);
                    //Debug.Log($"Critical hit! Angle: {angle}, Y Difference: {yDifference}");
                }
                else
                {
                    if (playerAttackVelue > monsterAttackVelue)
                    {
                        monster.OnDamage(damege, Vector3.zero, Vector3.zero);
                        playerHealth.OnDamage((monsterControl.monsterDamage * 0.5f), Vector3.zero, Vector3.zero);
                    }
                    else
                    {
                        monster.OnDamage((damege * 0.5f), Vector3.zero, Vector3.zero);
                        playerHealth.OnDamage(monsterControl.monsterDamage, Vector3.zero, Vector3.zero);
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        attackTime += Time.deltaTime;

        if (attackTime > attackDeley && canAttack)
        {
            if (other.CompareTag("Monster"))
            {
                // 지속 데미지 및 이펙트 유지
                if (hitEffect != null)
                {
                    hitEffect.transform.position = other.ClosestPoint(transform.position);
                    if (!hitEffect.isPlaying)
                    {
                        hitEffect.Play();
                    }
                }

                var monster = other.GetComponent<LivingEntity>();
                if (monster != null && !monster.IsDead)
                {
                    monster.OnDamage(dotDamage, Vector3.zero, Vector3.zero);
                    playerHealth.OnDamage(dotDamage, Vector3.zero, Vector3.zero);
                }
                attackTime = 0;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        attackTime = 0f;
        canAttack = false;

        if (hitEffect != null)
        {
            hitEffect.Stop();
        }
    }

    private void SpawnParticleAtContact(Vector3 position)
    {
        hitEffect = Instantiate(hitEffect, position, Quaternion.identity);
    }

    private Vector3 GetContactPoint(Collider other)
    {
        // 충돌 접점 계산
        if (other.TryGetComponent<Collider>(out Collider collider))
        {
            RaycastHit hit;
            Vector3 direction = (other.transform.position - transform.position).normalized;
            if (collider.Raycast(new Ray(transform.position, direction), out hit, Mathf.Infinity))
            {
                return hit.point;
            }
        }
        return other.ClosestPoint(transform.position); // 기본 접점 반환
    }
}

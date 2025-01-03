using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    public Slider healthSlider;
    public Slider rotaSlider;
    private Rotate rotate;
    private PlayerTop pt;
    private TopMove topMove;
    private float currntRota;

    private void Awake()
    {
        rotate = GetComponent<Rotate>();
        pt = GetComponent<PlayerTop>();
        topMove = GetComponent<TopMove>();

        topMove.enabled = true;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);

        healthSlider.value = hp;
    }

    private void FixedUpdate()
    {
        currntRota = rotate.rota;
        rotaSlider.value = currntRota;

    }

    protected override void OnEnable()
    {
        base.OnEnable();

        healthSlider.gameObject.SetActive(true);
        rotaSlider.gameObject.SetActive(true);

        healthSlider.maxValue = maxHp;
        healthSlider.minValue = 0f;
        healthSlider.value = hp;

        rotaSlider.maxValue = 99f;
        rotaSlider.minValue = 0f;
        rotaSlider.value = currntRota;
    }

    public override void Die()
    {
        base.Die();
        healthSlider.gameObject.SetActive(false);
        pt.Die();
        topMove.enabled = false;
    }

    public override void AddHp(float add)
    {
        base.AddHp(add);
        healthSlider.value = hp;
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            OnDamage(20, Vector3.zero, Vector3.zero);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            AddHp(20);
        }
    }

}

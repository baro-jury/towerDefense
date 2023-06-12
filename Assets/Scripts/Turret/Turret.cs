using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float attackRange;

    public float AttackRange { get; set; }
    public Enemy CurrentEnemyTarget { get; set; }

    private CircleCollider2D circleCollider;

    protected virtual void Start()
    {
        AttackRange = attackRange;
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = attackRange;
    }

    protected virtual void Update()
    {
        RotateTowardsTarget();
    }

    private void RotateTowardsTarget()
    {
        if (CurrentEnemyTarget == null)
        {
            return;
        }

        Vector3 targetPos = CurrentEnemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }
}
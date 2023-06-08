using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public static Action<Enemy> OnEndReached;

    [SerializeField] private int health;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int deathCoinReward;

    public int Health { get; set; }
    public float MoveSpeed { get; set; }
    public int DeathCoinReward { get; set; }
    public Waypoint Waypoint { get; set; }
    public Vector3 CurrentPointPosition { get; set; }

    private bool canMove = true;
    private int _currentWaypointIndex = 0;
    private Vector3 _lastPointPosition;
    private SpriteRenderer _spriteRenderer;

    //private EnemyHealth _enemyHealth;

    void Start()
    {
        Health = health;
        MoveSpeed = moveSpeed;
        DeathCoinReward = deathCoinReward;
        Waypoint = GameObject.Find("Waypoint").GetComponent<Waypoint>();
        CurrentPointPosition = Waypoint.Points[_currentWaypointIndex];
        _lastPointPosition = transform.position;
        //_enemyHealth = GetComponent<EnemyHealth>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        Rotate();

        if (CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    private void Move()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, MoveSpeed * Time.deltaTime);
        }
    }

    private void Rotate()
    {
        if (CurrentPointPosition.x > _lastPointPosition.x)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }

    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f)
        {
            _lastPointPosition = transform.position;
            return true;
        }

        return false;
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = Waypoint.Points.Length - 1;
        if (_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++;
            CurrentPointPosition = Waypoint.Points[_currentWaypointIndex];
        }
        else
        {
            EndPointReached();
        }
    }

    private void EndPointReached()
    {
        OnEndReached?.Invoke(this);
        //_enemyHealth.ResetHealth();
        ResetHealth();
        ObjectPooler.ReturnToPool(gameObject);
    }

    public void ResetHealth()
    {
        Health = health;
    }

    public void StopMovement()
    {
        canMove = false;
    }

    public void ResumeMovement()
    {
        canMove = true;
    }
}

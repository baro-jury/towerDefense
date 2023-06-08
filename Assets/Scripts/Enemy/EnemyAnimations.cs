using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    public GameObject deathParticles; 
    private Animator _animator;
    private Enemy _enemy;
    //private EnemyHealth _enemyHealth;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        //_enemyHealth = GetComponent<EnemyHealth>();
    }

    private void PlayHurtAnimation()
    {
        _animator.SetTrigger("hurtTrigger");
    }

    private float GetCurrentAnimationLenght()
    {
        float animationLenght = _animator.GetCurrentAnimatorStateInfo(0).length;
        return animationLenght;
    }

    private IEnumerator PlayHurt()
    {
        _enemy.StopMovement();
        PlayHurtAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLenght() + 0.3f);
        _enemy.ResumeMovement();
    }
    private IEnumerator PlayHurt(int damage)
    {
        _enemy.StopMovement();
        PlayHurtAnimation();
        _enemy.Health -= damage;
        yield return new WaitForSeconds(GetCurrentAnimationLenght() + 0.3f);
        _enemy.ResumeMovement();
    }

    private IEnumerator PlayDead()
    {
        _enemy.StopMovement();
        //Instantiate(deathParticles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        _enemy.ResumeMovement();
        //_enemyHealth.ResetHealth();
        _enemy.ResetHealth();
        ObjectPooler.ReturnToPool(_enemy.gameObject);
    }

    private void EnemyHit(Enemy enemy)
    {
        if (_enemy == enemy)
        {
            StartCoroutine(PlayHurt());
        }
    }
    private void EnemyHit(Enemy enemy, int damage)
    {
        if (_enemy == enemy)
        {
            StartCoroutine(PlayHurt(damage));
        }
    }

    private void EnemyDead(Enemy enemy)
    {
        if (_enemy == enemy)
        {
            StartCoroutine(PlayDead());
        }
    }

    private void OnEnable()
    {
        //EnemyHealth.OnEnemyHit += EnemyHit;
        //EnemyHealth.OnEnemyKilled += EnemyDead;
        Projectile.OnEnemyHit += EnemyHit;
        Projectile.OnEnemyDead += EnemyDead;
    }

    private void OnDisable()
    {
        //EnemyHealth.OnEnemyHit -= EnemyHit;
        //EnemyHealth.OnEnemyKilled -= EnemyDead;
        Projectile.OnEnemyHit -= EnemyHit;
        Projectile.OnEnemyDead -= EnemyDead;
    }
}
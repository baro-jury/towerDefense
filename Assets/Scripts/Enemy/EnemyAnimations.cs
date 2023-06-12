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

    private void PlayDeadAnimation()
    {
        _animator.SetTrigger("deadTrigger");
    }

    private float GetCurrentAnimationLenght()
    {
        float animationLenght = _animator.GetCurrentAnimatorStateInfo(0).length;
        return animationLenght;
    }

    private IEnumerator PlayDamaged(int damage)
    {
        _enemy.StopMovement();
        PlayHurtAnimation();
        _enemy.Health -= damage;
        yield return new WaitForSeconds(GetCurrentAnimationLenght() + 0.3f);
        _enemy.ResumeMovement();
    }

    private IEnumerator PlayHurt()
    {
        _enemy.StopMovement();
        PlayHurtAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLenght() + 0.2f);
        _enemy.ResumeMovement();
    }

    private IEnumerator PlayDead()
    {
        _enemy.StopMovement();
        //Instantiate(deathParticles, transform.position, Quaternion.identity);
        //---- tao 1 prefabs deahParticle co anim, ko lien quan gi den enemy ----
        PlayDeadAnimation();
        //yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(GetCurrentAnimationLenght());
        _enemy.ResumeMovement();
        //_enemyHealth.ResetHealth();
        _enemy.ResetHealth();
        ObjectPooler.ReturnToPool(_enemy.gameObject);
    }

    private void EnemyDamaged(Enemy enemy, int damage)
    {
        if (_enemy == enemy)
        {
            StopAllCoroutines();
            StartCoroutine(PlayDamaged(damage));
        }
    }

    private void EnemyHit(Enemy enemy)
    {
        if (_enemy == enemy)
        {
            StopAllCoroutines();
            StartCoroutine(PlayHurt());
        }
    }


    private void EnemyDead(Enemy enemy)
    {
        if (_enemy == enemy)
        {
            StopAllCoroutines();
            StartCoroutine(PlayDead());
        }
    }

    private void OnEnable()
    {
        //EnemyHealth.OnEnemyHit += EnemyHit;
        //EnemyHealth.OnEnemyKilled += EnemyDead;
        Projectile.OnEnemyDamaged += EnemyDamaged;
        Projectile.OnEnemyHit += EnemyHit;
        Projectile.OnEnemyDead += EnemyDead;
    }

    private void OnDisable()
    {
        //EnemyHealth.OnEnemyHit -= EnemyHit;
        //EnemyHealth.OnEnemyKilled -= EnemyDead;
        Projectile.OnEnemyDamaged -= EnemyDamaged;
        Projectile.OnEnemyHit -= EnemyHit;
        Projectile.OnEnemyDead -= EnemyDead;
    }
}
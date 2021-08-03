using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Raptor : MonoBehaviour
{
    AudioSource _audioSource;
    Animator _animator;
    NavMeshAgent _navMeshAgent;
    private GameObject _player;
    [SerializeField] float raptorHealth = 40;
    [SerializeField] float damageTakenAmount = 10;
    [SerializeField] AudioClip raptorRoarSound;

    private bool _isDead = false;
    private bool _isDealingDamage = true;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _isDead = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < 100f)
        {
            _animator.SetBool("Run", true);

            _navMeshAgent.SetDestination(_player.transform.position);
        }

        if (Vector3.Distance(transform.position, _player.transform.position) < 8f)
        {
            if (_isDealingDamage)
            {
                _isDealingDamage = false;
                StartCoroutine(DealDamage());
            }
            
        }
        else
        {
            _animator.SetBool("attack", false);
        }

        if (!_isDead)
        {
            if (raptorHealth <= 0)
            {
                _isDead = true;
                GameManager.Instance.killedEnemyNumbers++;
            
                Destroy(this.gameObject);
                //particül effect
            }
        }
     
    }

    IEnumerator DealDamage()
    {
        _player.GetComponent<PlayerHealth>().TakeDamage();
        _animator.SetBool("attack", true);
        _audioSource.PlayOneShot(raptorRoarSound);
        
        yield return new WaitForSeconds(1);
        
        _isDealingDamage = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            raptorHealth -= damageTakenAmount;
            // particül effect
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            raptorHealth -= damageTakenAmount;
            // particül effect
        }
    }
}
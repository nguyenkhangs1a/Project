using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MoveZombie : MonoBehaviour {
    private NavMeshAgent _nav;
    private Transform _Player;
    private Animator _ani;
    private Rigidbody _rig;
    private int boold = 3;
    bool isAttack;
    private Animation _idle;
	// Use this for initialization
	void Start () {
        _nav = GetComponent<NavMeshAgent>();
        if(_nav==null)
        {
            return;
        }
        _ani = GetComponent<Animator>();
       _Player = GameObject.FindGameObjectWithTag("Player").transform;
       isAttack = true;
       _idle = GetComponent<Animation>();
       //_idle["walk"].speed = 3f;
	}
   void Attack()
   {
       float disf = Vector3.Distance(transform.position, _Player.position);
       if(disf <= 30f && disf > 2f)
       {
           move();
           
       }
       else if(disf > 30f)
       {
           _ani.SetBool("Walk", false); 
       }
       if (disf <= 2f)
       {
           _nav.velocity = Vector3.zero;
           if(isAttack)
           {
               StartCoroutine(SetAttack());
           }
           
           _ani.SetBool("Walk", false);
       }
       else
       {
           _ani.SetBool("Attack", false);
       }
   }
	
	// Update is called once per frame


	void Update () {
        Attack();
	}


    public void Gethit(int dame)
    {
        boold -= dame;
        Debug.Log("HP=" + boold);
        if(boold<=0)
        {
            Debug.Log("Zombie dead");
            Destroy(gameObject);
            CreatZB.Count--;
        }
    }
    void move()
    {
        if (_Player == null || gameObject==null) return;
        if(gameObject!=null )
        {
            transform.LookAt(_Player.position);
            _nav.SetDestination(_Player.position);
            _ani.SetBool("Walk", true);
        }
        
    }

    IEnumerator SetAttack()
    {
        isAttack = false;
        _ani.SetBool("Attack", true);
        yield return new WaitForSeconds(2f);
        isAttack = true;
    }
}

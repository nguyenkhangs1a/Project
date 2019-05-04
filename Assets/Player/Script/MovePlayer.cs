using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {
    private Rigidbody _rig;
    private Animator _ani;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float Rotaty;
    [SerializeField]
    private float _jump;
    private Transform _transf;
    bool isjump=true;


    [SerializeField]
    private GameObject obj;
    [SerializeField]
    private Transform _Ponittransf;
    bool isint = true;

    bool isground = false;
    [SerializeField]
    private int CountBullet ;
    [SerializeField]
    private int FullBullet=30;
	// Use this for initialization
	void Start () {
        _rig = GetComponent<Rigidbody>();
        _ani = GetComponent<Animator>();
        _transf = GetComponent<Transform>();
        CountBullet = FullBullet;
	}
	
	// Update is called once per frame
	void Update () {
        inputKey();
	}
    void inputKey()
    {
        if(Input.GetKey(KeyCode.W))
        {
            Vector3 vel = _rig.velocity;
            Vector3 v = _transf.forward * speed * Time.deltaTime;
            v.y = vel.y;
            _rig.velocity=v;
            _ani.SetBool("Walk",true);
            //Up
        }
        else if(Input.GetKey(KeyCode.S))
        {
            //Down
            Vector3 vel = _rig.velocity;
            Vector3 v = -_transf.forward * speed * Time.deltaTime;
            v.y = vel.y;
            _rig.velocity = v;
            _ani.SetBool("Walk", true);
        }
        else { _ani.SetBool("Walk", false); }
        if(Input.GetKey(KeyCode.A))
        {
            Vector3 r = new Vector3(0, -(Rotaty * Time.deltaTime), 0);
            _transf.Rotate(r, Space.World);
            //rotaty left
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Vector3 r = new Vector3(0, Rotaty * Time.deltaTime, 0);
            _transf.Rotate(r, Space.World);
            //rotaty right
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            //hit
            if(isint)
            {
                StartCoroutine(DelayInit());
                _ani.SetTrigger("shoot");
            } 
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isjump && !isground)
            {
                StartCoroutine(DelayJump());
                isground = true;//jump
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CountBullet = FullBullet;
        }
    }
    IEnumerator DelayJump()
    {
        isjump = false;
        _rig.AddForce(Vector3.up * _jump);
        yield return new WaitForSeconds(0.5f);
        isjump = true;      
    }

    IEnumerator DelayInit()
    {
        isint = false;
        if(CountBullet>0)
        InitBullet1();
        else { }
        yield return new WaitForSeconds(1f);
        isint = true;
    }
    void InitBullet1()
    {
       Destroy( Instantiate(obj, _Ponittransf.position, _transf.rotation),5f);
       CountBullet--;
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Ground"))
        {
           
            isground=false;
        }
    }
}

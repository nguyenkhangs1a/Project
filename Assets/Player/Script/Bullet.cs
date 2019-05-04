using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private Rigidbody _rig;
    private Transform _transf;
    [SerializeField]
    private float speed;
    bool canUpdate = true;
    int dame = 1;
	// Use this for initialization
	void Start () {
        _rig = GetComponent<Rigidbody>();
        _transf = GetComponent<Transform>();      
	}
	
	// Update is called once per frame
	void Update () {
        if(canUpdate)
        {
            StartCoroutine(DelayUpdate());
        }
	}
    IEnumerator DelayUpdate()
    {
        Vector3 v = _transf.forward * speed * Time.deltaTime;
        _rig.velocity = v;
        canUpdate = false;
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(DelayUpdate());
        canUpdate = true;
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Zombie"))
        {
            col.transform.gameObject.GetComponent<MoveZombie>().Gethit(dame);
            Destroy(gameObject);
        }
    }
}

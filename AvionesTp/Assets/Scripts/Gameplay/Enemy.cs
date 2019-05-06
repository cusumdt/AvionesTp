using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ShipBase
{

    [SerializeField]
    private States EnemyState;
    [SerializeField]
    private float Speed;
    [SerializeField]
    private float RotationRate;
    [SerializeField]
    private float AttackDistance;
    [SerializeField]
    private float EscapeDistance;

    private Rigidbody rigi;
    private MachineGun Gun;

    public enum States
    {
        Idle, Attack, Escape, Last,
    }

    public Transform Player;

 

    private void Awake()
    {
        rigi = GetComponent<Rigidbody>();
        Speed = 60000f;
        RotationRate = 700f;
        AttackDistance = 400f;
        EscapeDistance = 100f;
        Gun = GetComponentInChildren<MachineGun>();
        Health = 100;
        if(!Player)
        {
            GameObject go = GameObject.Find("Player");
            Player = go.transform;
        }
    }

    void Update()
    {
        if(Health>0)
        {
            CheckState();
            switch (EnemyState)
            {
                case States.Idle:

                    break;
                case States.Attack:
                    SetRotation();
                    rigi.AddRelativeForce(0, 0, Speed * Time.deltaTime, ForceMode.Force);
                    Gun.Attack();
                    break;
                case States.Escape:
                    SetRotation(Player.rotation);
                    rigi.AddRelativeForce(0, 0, Speed * Time.deltaTime, ForceMode.Force);
                    break;
            }
        }
        else
        {
            GameManager.Instance.AddEnemyDead();
            Explode();
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Floor" || LayerMask.LayerToName(other.gameObject.layer) == "Water" || other.tag == "Player")
        {
            Health = 0;
        }

        if (other.tag == "PlayerBullet")
        {
            Health -= 50;
        }

        Debug.Log("Health: " + Health);
    }

    void CheckState()
    {
        if(Vector3.Distance(transform.position, Player.position) < EscapeDistance)
        {
            EnemyState = States.Escape;
        }
        else if(Vector3.Distance(transform.position, Player.position) < AttackDistance)
        {
            if (GetTargetAhead())
            {
                EnemyState = States.Attack;
            }
            else
            {
                EnemyState = States.Escape;
            }
        }
        else
        {
            EnemyState = States.Idle;
        }
    }

    bool GetTargetAhead()
    {
        Vector3 heading = Player.position - transform.position;
        float dot = Vector3.Dot(heading, transform.forward);
        return dot > 0;
    }

    void SetRotation()
    {
        Quaternion q01 = Quaternion.identity;
        q01.SetLookRotation(Player.position - transform.position, Vector3.up);
        transform.rotation = q01;
    }

    void SetRotation(Quaternion rot)
    {
        transform.rotation = rot;
    }

    public States GetState()
    {
        return EnemyState;
    }

}

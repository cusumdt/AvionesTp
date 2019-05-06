using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : ShipBase
{
    private Rigidbody rigi;
    [SerializeField]
    private float SpeedZ;
    [SerializeField]
    private float PitchRate;
    [SerializeField]
    private float RollRate;
    [SerializeField]
    private float YawRate;
    private float InputPitch;
    private float InputRoll;
    private float InputYaw;
    private float Throttle;
    private MachineGun machineGun;

    void Awake()
    {
        rigi = GetComponentInParent<Rigidbody>();
        SpeedZ = 8000f;
        PitchRate = 70;
        RollRate = -70;
        YawRate = 70;
        Physics.gravity = new Vector3(0f,-91.8f, 0f);
        machineGun = GetComponentInChildren<MachineGun>();
        Health = 200;
 
        
    }

    private void FixedUpdate()
    {
        if (Health > 0)
        {
            GetInput(ref InputPitch, ref InputRoll, ref InputYaw, ref Throttle);

            rigi.AddRelativeForce(new Vector3(0, 0, SpeedZ * Throttle), ForceMode.Force);
        }
    }

    private void Update()
    {
        if(Health<=0)
        {
            CameraManager.Instance.SwitchToExplosionCamera(transform.position);
            Explode();
            GameManager.Instance.GameOver();
        }
        else
        {
            GetInput(ref InputPitch, ref InputRoll, ref InputYaw, ref Throttle);
            transform.Rotate(InputPitch * PitchRate * Time.deltaTime, InputYaw * YawRate * Time.deltaTime, InputRoll * RollRate * Time.deltaTime, Space.Self);
            machineGun.Attack();
     
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Floor" || LayerMask.LayerToName(other.gameObject.layer) == "Water" || other.tag == "Enemy")
        {
            Health = 0;
        }

        if (other.tag == "EnemyBullet")
        {
            Health -= 50;
        }

        Debug.Log("Health: " + Health);
    }


    void GetInput(ref float InputPitch, ref float InputRoll, ref float InputYaw, ref float Throttle)
    {
        bool AccInput;
        float Target = Throttle;

        Vector3 mousePosition = Input.mousePosition;

        InputRoll = Input.GetAxis("Horizontal");

        InputPitch = (mousePosition.y - (Screen.height * 0.5f)) / (Screen.height * 0.5f);
        InputYaw = (mousePosition.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

        InputPitch = -Mathf.Clamp(InputPitch, -1.0f, 1.0f);
        InputYaw = Mathf.Clamp(InputYaw, -1.0f, 1.0f);


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Target = 1.0f;
            AccInput = true;
        }
        else
        { 
            Target = 0f;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                AccInput = true;
            }
            else
            { 
                AccInput = false;
            }
        }
        
        if (AccInput)
        {
            Throttle = Mathf.MoveTowards(Throttle, Target, Time.deltaTime * 0.5f);
        }
        else
        {
            Throttle = Mathf.MoveTowards(Throttle, Target, Time.deltaTime * 0.25f);
        }
        
    }    

    public int GetHealth()
    {
        return Health;
    }
}

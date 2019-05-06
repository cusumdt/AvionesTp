using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    [SerializeField]
    private GameObject BulletPrefab;
    [SerializeField]
    private float BulletSpeed = 10f;
    [SerializeField]

    private List<GameObject> Bullet;
    private List<float> BulletLifeTimer;
    private float Timer = 0f;
    private float BulletLifeTime = 3f;

    private const int TotalBullets = 20;

    public float BulletCooldown = 0.1f;



    void Start()
    {
        Bullet = new List<GameObject>();
        BulletLifeTimer = new List<float>();

        for(int i=0;i<TotalBullets;i++)
        {
            if (BulletPrefab)
            {
                GameObject go = Instantiate(BulletPrefab);
                go.SetActive(false);
                Bullet.Add(go);
                BulletLifeTimer.Add(0);
                if(transform.parent.tag=="Player")
                {
                    Bullet[i].tag = "PlayerBullet";
                }
                else
                {
                    Bullet[i].name = "EnemyBullet";
                    Bullet[i].tag = "EnemyBullet";
                }
            }
        }
    }

    public void Attack()
    {
        if(this.transform.parent.tag=="Player")
        {
            for(int i=0;i<TotalBullets;i++)
            {
                if(!Bullet[i].activeSelf)
                {
                    if (Timer >= BulletCooldown)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            Bullet[i].SetActive(true);
                            Bullet[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                            Bullet[i].transform.position = transform.position;
                            Bullet[i].transform.rotation = Quaternion.identity;
                            Bullet[i].GetComponent<Rigidbody>().AddForce(transform.parent.forward*BulletSpeed, ForceMode.VelocityChange);
                            Timer = 0;
                            BulletLifeTimer[i] = 0;
                        }
                    }
                }
                else
                {
                    BulletLifeTimer[i] +=Time.deltaTime;
                    if (BulletLifeTimer[i] >= BulletLifeTime)
                    {
                        Bullet[i].SetActive(false);
                        BulletLifeTimer[i] = 0;
                    }
                }
            }  
        }
        else
        { 
            for (int i = 0; i < TotalBullets; i++)
            {
                if (!Bullet[i].activeSelf)
                {
                    if (Timer >= BulletCooldown)
                    {
                        Bullet[i].SetActive(true);
                        Bullet[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                        Bullet[i].transform.position = transform.position;
                        Bullet[i].transform.rotation = Quaternion.identity;
                        Bullet[i].GetComponent<Rigidbody>().AddForce(transform.parent.forward * BulletSpeed, ForceMode.VelocityChange);
                        Timer = 0;
                        BulletLifeTimer[i] = 0;
                    }
                }
                else
                {
                    BulletLifeTimer[i] += Time.deltaTime;
                    if(BulletLifeTimer[i]>=BulletLifeTime)
                    {
                        Bullet[i].SetActive(false);
                    }
                }
            }  
        }
        Timer += Time.deltaTime;
    }
}

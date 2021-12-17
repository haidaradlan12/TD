using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    Transform turretTransform;

    float range = 10f;
    public GameObject bulletPrefab;

    public int cost = 20;

    float fireCooldown = 0.5f;
    float fireCooldownLeft = 0;

    public float damage = 1f;
    public float radius = 0;


    //tambahan
    public Transform target;
    public float healthTower = 10;
    // Start is called before the first frame update
    void Start()
    {
        turretTransform = transform.Find("Defender");
    }

    // Update is called once per frame
    void Update()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

        Enemy nearestEnemy = null;

        float dist = Mathf.Infinity;

        foreach(Enemy e in enemies)
        {
            float d = Vector3.Distance(this.transform.position, e.transform.position);
            if (nearestEnemy == null || d<dist)
            {
                nearestEnemy = e;
                dist = d;
            }
        }

        if(nearestEnemy == null)
        {
            Debug.Log("Raono musuh cok");
            return;
        }
        
        Vector3 dir = nearestEnemy.transform.position - turretTransform.transform.position;

        Quaternion lookRot = Quaternion.LookRotation(dir);
        turretTransform.transform.rotation = Quaternion.Lerp(turretTransform.transform.rotation, lookRot, 4 * Time.deltaTime);
        float xais = turretTransform.transform.rotation.eulerAngles.x;
        float yais = turretTransform.transform.rotation.eulerAngles.y;
        float zais = turretTransform.transform.rotation.eulerAngles.z;
        turretTransform.transform.rotation = Quaternion.Euler(0.0f, 0.0f, zais);

        fireCooldownLeft -= Time.deltaTime;
        if(fireCooldownLeft <= 0 && dir.magnitude <= range)
        {
            fireCooldownLeft = fireCooldown;
            ShootAt(nearestEnemy);
        }
    }

    void ShootAt(Enemy e)
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, turretTransform.transform.position, turretTransform.transform.rotation);

        Bullet b = bulletGO.GetComponent<Bullet>();
        b.target = e.transform;
        b.damage = damage;
        b.radius = radius;
    }

    public void takeDamageTower(float damageenemy)
    {
        healthTower -= damageenemy;
        if (healthTower <= 0)
        {
            MatiTower();
        }
    }

    public void MatiTower()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    GameObject pathGO;
    //
    public Transform enemyelittransform;

    public GameObject BulletEnemyPrefabs;

    float damageenemy = 0.5f;

    public Transform targettower;

    float fireCooldown = 0.5f;
    float fireCooldownLeft = 0;

    //
    Transform targetPath;
    int pathPointIndex = 0;

    public float health = 4f;

    public int coinValue = 2;

    public float speed;
    public int Heading;
    // Start is called before the first frame update
    void Start()
    {
        pathGO = GameObject.Find("Path"); 
        
        //
        enemyelittransform = transform.Find("Enemy Elite");
    }

    void GetNextPathPoint()
    {
        if(pathPointIndex < pathGO.transform.childCount)
        {
            targetPath = pathGO.transform.GetChild(pathPointIndex);
            pathPointIndex++;
        }
        else
        {
            targetPath = null;
            ReachedGoal();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPath == null)
        {
            GetNextPathPoint();
            if (targetPath == null)
            {
                ReachedGoal();
            }
        }

        Vector3 dir = targetPath.position - this.transform.localPosition;

        float distThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distThisFrame)
        {
            targetPath = null;
        }
        else
        {
            transform.Translate(dir.normalized * distThisFrame , Space.World);
            Quaternion targerRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targerRotation, Heading*Time.deltaTime);
            float xais = this.transform.rotation.eulerAngles.x;
            float yais = this.transform.rotation.eulerAngles.y;
            float zais = this.transform.rotation.eulerAngles.z;
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, zais);

        }

        //
        Tower[] towers = GameObject.FindObjectsOfType<Tower>();

        Tower nearestTower = null;

        float dist = Mathf.Infinity;

        foreach (Tower e in towers)
        {
            float d = Vector3.Distance(this.transform.position, e.transform.position);
            if (nearestTower == null || d < dist)
            {
                nearestTower = e;
                dist = d;
            }
        }

        if (nearestTower == null)
        {
            Debug.Log("Towere kelewat cok");
            return;
        }
        
        Vector3 dir2 = nearestTower.transform.position - enemyelittransform.transform.position;

        Quaternion lookRot = Quaternion.LookRotation(dir2);
        enemyelittransform.transform.rotation = Quaternion.Lerp(enemyelittransform.transform.rotation, lookRot, 4 * Time.deltaTime);
        float xais2 = enemyelittransform.transform.rotation.eulerAngles.x;
        float yais2 = enemyelittransform.transform.rotation.eulerAngles.y;
        float zais2 = enemyelittransform.transform.rotation.eulerAngles.z;
        enemyelittransform.transform.rotation = Quaternion.Euler(0.0f, 0.0f, zais2);
        
        fireCooldownLeft -= Time.deltaTime;
        if (fireCooldownLeft <= 0  && dir2.magnitude <= 10)
        {
            fireCooldownLeft = fireCooldown;
            Shoot(nearestTower);
        }
    }

    void ReachedGoal()
    {
        GameObject.FindObjectOfType<ScoreManager>().LoseLife();
        Destroy(gameObject);
    }

     public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Mati();
        }
    }

    public void Mati()
    {
        GameObject.FindObjectOfType<ScoreManager>().coin += coinValue;
        Destroy(gameObject);
    }
    
    //
    void Shoot(Tower e)
    {
        GameObject bulletGO = (GameObject)Instantiate(BulletEnemyPrefabs, enemyelittransform.transform.position, enemyelittransform.transform.rotation);

        BulletEnemy b = bulletGO.GetComponent<BulletEnemy>();
        b.targetTurret = e.transform;
    }
}

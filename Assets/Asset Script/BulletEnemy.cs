using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float speed = 8f;
    public Transform targetTurret;
    public float damageenemy = 0.5f;
    public float radius = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (targetTurret == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir2 = targetTurret.position - this.transform.localPosition;

        float distThisFrame = speed * Time.deltaTime;
        if (dir2.magnitude <= distThisFrame)
        {
            DoBulletHitEnemy();
        }
        else
        {
            transform.Translate(dir2.normalized * distThisFrame, Space.World);
            Quaternion targerRotation = Quaternion.LookRotation(dir2);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targerRotation, 4 * Time.deltaTime);
            float xais2 = this.transform.rotation.eulerAngles.x;
            float yais2 = this.transform.rotation.eulerAngles.y;
            float zais2 = this.transform.rotation.eulerAngles.z;
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, zais2);

        }
    }

    void DoBulletHitEnemy()
    {
        if (radius == 0)
        {
            targetTurret.GetComponent<Tower>().takeDamageTower(damageenemy);
        }
        else
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider c in cols)
            {
                Tower e = c.GetComponent<Tower>();
                if (e != null)
                {
                    e.GetComponent<Tower>().takeDamageTower(damageenemy);
                }
            }
        }

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public Transform target;
    public float damage = 1f;
    public float radius = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - this.transform.localPosition;

        float distThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distThisFrame)
        {
            DoBulletHit();
        }
        else
        {
            transform.Translate(dir.normalized * distThisFrame, Space.World);
            Quaternion targerRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targerRotation, 4 * Time.deltaTime);
            float xais = this.transform.rotation.eulerAngles.x;
            float yais = this.transform.rotation.eulerAngles.y;
            float zais = this.transform.rotation.eulerAngles.z;
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, zais);

        }
    }

    void DoBulletHit()
    {
        if (radius == 0)
        {
            target.GetComponent<Enemy>().takeDamage(damage);
        }
        else
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, radius);

            foreach(Collider c in cols)
            {
                Enemy e = c.GetComponent<Enemy>();
                if(e != null)
                {
                    e.GetComponent<Enemy>().takeDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }
}

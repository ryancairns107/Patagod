using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class jeroenAI : MonoBehaviour
{
    public GameObject thisShip;
    public List<GameObject> Targets;
    public GameObject TargetShip;
    public float sphereSize;
    Quaternion desiredRotation;
    public float angle;
    public float movementSpeed = 10;
    public int currentHP;
    public int maxHP = 100;
    public shiphealth healthBar;
    public int ballDamage;
    public int cannonAmmo;
    public ParticleSystem particleSys;
    public NavMeshAgent navAgent;
    public GameObject CannonBall = null;
    public Transform CannonBackSpawnPoint = null;
    public Transform CannonLeftSpawnPoint = null;
    public Transform CannonRightSpawnPoint = null;

    private void Start()
    {
        currentHP = maxHP;
        particleSys = GetComponentInChildren<ParticleSystem>();
        particleSys.Stop();
    }
    //
    void Update()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
        RaycastHit hit;
        if (Physics.SphereCast(thisShip.transform.position, 500, transform.forward, out hit, 1))
        {
            if (hit.transform.gameObject.tag == "Boatbody" && hit.transform.gameObject != this.gameObject)
            {
                if (Targets.Contains(hit.transform.gameObject) == false)
                {
                    Targets.Add(hit.transform.gameObject);
                }
                for(int i = 0; i < Targets.Count; i++)
                {
                    if (Targets[i] != hit.transform.gameObject)
                    {
                        Targets.Clear();
                    }
                }
            }
            else
            {
                Targets.Remove(hit.transform.gameObject);
            }
        }
        if (Targets != null)
        {
            TargetShip = Targets[0];
        } else
        {
            TargetShip = null;
        }
        if (TargetShip != null)
        {
            Vector3 posRelative = TargetShip.transform.position - transform.position;
            desiredRotation = Quaternion.LookRotation(posRelative);
           // desiredRotation *= Quaternion.Euler(0, -90, 0);
          //  transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 0.5f * Time.deltaTime);
            Vector3 heading = TargetShip.transform.position - transform.position;
            angle = AngleTowards(transform.forward, heading, transform.up);
            Debug.Log(angle);
            if (angle == 1f)
            {
                desiredRotation *= Quaternion.Euler(0, -90, 0);
            }
            else if (angle == -1f)
            {
                desiredRotation *= Quaternion.Euler(0, 90, 0);
            }
            else
            {
                desiredRotation *= Quaternion.Euler(0, -90, 0);
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 0.5f * Time.deltaTime);

            if (cannonAmmo > 0)
            {
                switch (angle)
                {
                    case 1:
                        StartCoroutine(shootRight());
                        break;
                    case 0:
                        StartCoroutine(shootBack());
                        break;
                    case -1:
                        StartCoroutine(shootLeft());
                        break;
                }
                    
            }
        }
    }
    private void Damaged(int damage)
    {
        currentHP += damage;
        healthBar.SetHealth(currentHP);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wall") 
        {
            desiredRotation = transform.rotation *= Quaternion.Euler(0, -180, 0);
            transform.rotation = desiredRotation;
        }
        if (other.gameObject.tag == "canon" || other.gameObject.tag == "Boatbody")
        {
            Damaged(ballDamage);
            Debug.Log("Ship Damaged!!!");
        }
        if (other.gameObject.tag == "Ammo")
        {
            cannonAmmo += 20;
        }
        if (other.gameObject.tag == "Health")
        {
            currentHP += 50;
        }
    }
    float AngleTowards(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }
    public IEnumerator shootRight()
    {
        GameObject newCannonBall = Instantiate(CannonBall, CannonRightSpawnPoint.position, CannonRightSpawnPoint.rotation);
        cannonAmmo -= 1;
        yield return new WaitForFixedUpdate();
    }
    public IEnumerator shootBack()
    {
        GameObject newCannonBall = Instantiate(CannonBall, CannonBackSpawnPoint.position, CannonBackSpawnPoint.rotation);
        cannonAmmo -= 1;
        yield return new WaitForFixedUpdate();
    }
    public IEnumerator shootLeft()
    {
        GameObject newCannonBall = Instantiate(CannonBall, CannonRightSpawnPoint.position, CannonRightSpawnPoint.rotation);
        cannonAmmo -= 1;
        yield return new WaitForFixedUpdate();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 500);
    }
}

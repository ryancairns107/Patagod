using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JeroenAI : MonoBehaviour
{
    public GameObject thisShip;
    public List<GameObject> Targets;
    public GameObject TargetShip;
    Quaternion desiredRotation;
    public float angle;
    public float movementSpeed = 10;
    public int currentHP;
    public int maxHP = 100;
    public shiphealth healthBar;
    public int ballDamage;
    public int cannonAmmo;
    public bool canShoot;
    public ParticleSystem particleSys;
    public NavMeshAgent navAgent;
    public GameObject CannonBall = null;
    public Transform CannonBackSpawnPoint = null;
    public Transform CannonLeftSpawnPoint = null;
    public Transform CannonRightSpawnPoint = null;
    public GameObject healthPickup = null;
    public GameObject ammoPickup = null;
    public bool hasDestination = false;

    private void Start()
    {
        currentHP = maxHP;
        navAgent = GetComponent<NavMeshAgent>();
        particleSys = GetComponent<ParticleSystem>();
        particleSys.Stop();
        canShoot = true;
        navAgent.isStopped = true;
    }
    void Update()
    {
        healthBar.health = currentHP;
        healthPickup = GameObject.FindWithTag("Health");
        ammoPickup = GameObject.FindWithTag("Ammo");
        if (Targets != null && Targets.Count > 0)
        {
            TargetShip = Targets[0];
        }
        else
        {
            TargetShip = null;
        }
        if (TargetShip == null)
        {
            if (ammoPickup != null)
            {
                navAgent.isStopped = false;
                navAgent.destination = ammoPickup.transform.position;
            } else if (healthPickup != null)
            {
                navAgent.isStopped = false;
                navAgent.destination = healthPickup.transform.position;
            }
            else
            {
                navAgent.isStopped = true;
            }
        }
        if (TargetShip != null)
        {
            float distanceToTarget = Vector3.Distance(TargetShip.transform.position, transform.position);
            Debug.Log(distanceToTarget);
            Vector3 posRelative = TargetShip.transform.position - transform.position;
            desiredRotation = Quaternion.LookRotation(posRelative);
            Vector3 heading = TargetShip.transform.position - transform.position;
            angle = AngleTowards(transform.forward, heading, transform.up);
            Debug.Log(angle);
            if (distanceToTarget <= 250)
            {
                navAgent.isStopped = true;
                navAgent.destination = gameObject.transform.position;
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
                transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 2f * Time.deltaTime);
            } else if (distanceToTarget > 250 && TargetShip != null)
            {
                navAgent.destination = TargetShip.transform.position;
                navAgent.isStopped = false;
            }

            if (cannonAmmo > 0 && canShoot == true && distanceToTarget <= 250)
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
        if (cannonAmmo <= 5 || currentHP <= 40)
        {
            if (cannonAmmo <= 5 && ammoPickup != null)
            {
                navAgent.isStopped = false;
                navAgent.destination = ammoPickup.transform.position;
                Debug.Log("GoForAmmo");
            }
            else if (currentHP <= 40 && healthPickup != null)
            {
                navAgent.isStopped = false;
                navAgent.destination = healthPickup.transform.position;
                Debug.Log("GoForHP");
            }
            else
            {
                navAgent.isStopped = true;
            }
        }
        if (currentHP > 100)
        {
            currentHP = 100;
        } else if (currentHP <= 0)
        {
            currentHP = 100;
        }
        if (navAgent.isStopped == true)
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
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
        if (other.gameObject.tag == "canon" || other.gameObject.tag == "Boatbody" && other.gameObject != this)
        {
            Damaged(ballDamage);
            Debug.Log("Ship Damaged!!!");
        }
        if (other.gameObject.tag == "Ammo")
        {
            cannonAmmo += 10;
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
            return 1;
        }
        else if (dir < 0f)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
    public IEnumerator shootRight()
    {
        GameObject newCannonBall = Instantiate(CannonBall, CannonRightSpawnPoint.position, CannonRightSpawnPoint.rotation);
        cannonAmmo -= 1;
        StartCoroutine(shootCooldown());
        yield return null;
    }
    public IEnumerator shootBack()
    {
        GameObject newCannonBall = Instantiate(CannonBall, CannonBackSpawnPoint.position, CannonBackSpawnPoint.rotation);
        cannonAmmo -= 1;
        StartCoroutine(shootCooldown());
        yield return null;
    }
    public IEnumerator shootLeft()
    {
        GameObject newCannonBall = Instantiate(CannonBall, CannonLeftSpawnPoint.position, CannonLeftSpawnPoint.rotation);
        cannonAmmo -= 1;
        StartCoroutine(shootCooldown());
        yield return null;
    }
    public IEnumerator shootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(3);
        canShoot = true;
    }
    public IEnumerator rotateLeft()
    {
        
        yield return new WaitForSeconds(3);
        canShoot = true;
    }
}

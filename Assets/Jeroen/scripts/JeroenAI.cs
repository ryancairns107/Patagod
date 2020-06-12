using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public int deaths = 0;
    private bool isRotating = false;
    private bool terrainAvoidance = false;
    public LayerMask Rocks;
    public int OsledKills;
    public int PataKills;
    public Text deathText;
    private bool gettingStuff = false;
    

    private void Start()
    {
        /*
        * Sets current HP to the max allowed HP, gets the navagent and particlesystem components.
        * Also makes sure the ship can shoot when it spawns
        * Disables the navagent as well unless it spots an item or another ship.
        */

        currentHP = maxHP;
        navAgent = GetComponent<NavMeshAgent>();
        particleSys = GetComponent<ParticleSystem>();
        particleSys.Stop();
        canShoot = true;
        navAgent.isStopped = true;
    }
    void Update()
    {
        /*
         * Finds a health and ammo pickup gameobject on the map and updates the deathtext to the amount of deaths the ship has.
         */
        healthPickup = GameObject.FindWithTag("Health");
        ammoPickup = GameObject.FindWithTag("Ammo");
        deathText.text = "" + deaths;
        /*
         * This script checks if the big spherecollider/lookout has any ships inside of it and then proceeds to set it's current target to the first ship in the list.
        */
        if (Targets != null && Targets.Count > 0)
        {
            TargetShip = Targets[0];
        }
        else
        {
            TargetShip = null;
        }
        /*
         *If there is no target spotted it will instead try to pick up the ammo.
        */
        if (TargetShip == null)
        {
            if (ammoPickup != null && ammoPickup.activeSelf)
            {
                navAgent.isStopped = false;
                navAgent.destination = ammoPickup.transform.position;
            } 
            else
            {
                navAgent.isStopped = true;
                navAgent.destination = gameObject.transform.position;
            }
        }
        /*
         * Here the ship checks if it has enough ammo and HP to engage, it also checks if the ship is not near any terrain.
         * It will then proceed to rotate itself with it's side towards the target and proceed with firing it's cannons.
         * The AI calculates whether it's target is to the left or to the right of itself and based on this it will fire the right or left cannons.
        */
        if (TargetShip != null && cannonAmmo > 5 && currentHP > 20 && terrainAvoidance == false)
        {
            float distanceToTarget = Vector3.Distance(TargetShip.transform.position, transform.position);
            Vector3 posRelative = TargetShip.transform.position - transform.position;
            desiredRotation = Quaternion.LookRotation(posRelative);
            Vector3 heading = TargetShip.transform.position - transform.position;
            angle = AngleTowards(transform.forward, heading, transform.up);
            if (distanceToTarget <= 250 && terrainAvoidance == false)
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
                // rotation
                transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 2f * Time.deltaTime);

                /*
                 * AI calculates if it's close enough (If the target ship is too far away the shots have a higher chance of missing)
                */
            } else if (distanceToTarget > 250 && TargetShip != null)
            {
                navAgent.destination = TargetShip.transform.position;
                navAgent.isStopped = false;
            }

            // Shooting function
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
        /*
         * This part checks if either the ammo or the HP is nearing 0 
         * It will then proceed to turn the navAgent back on and set it's destination to the ammo or health pickup respectively.
         * 
         */
        if (cannonAmmo <= 5 || currentHP <= 20)
        {
            if (cannonAmmo <= 5 && ammoPickup != null && TargetShip != null)
            {
                TargetShip = null;
                desiredRotation = transform.rotation;
                navAgent.isStopped = false;
                navAgent.destination = ammoPickup.transform.position;
            }
            else if (currentHP <= 20 && healthPickup != null)
            {
                TargetShip = null;
                desiredRotation = transform.rotation;
                navAgent.isStopped = false;
                navAgent.destination = healthPickup.transform.position;
            }
            else
            {
                navAgent.isStopped = true;
                navAgent.destination = gameObject.transform.position;
            }
        }
        // Checks if the HP has gone over 100 and will reset it to 100 if it has.
        if (currentHP > 100)
        {
            currentHP = 100;
        }
        // adds in a death if HP reaches 0 and will reset it's own health to 100
        else if (currentHP <= 0)
        {
            currentHP = 100;
            deaths += 1;
        }
        // Checks if the navagent is stopped and will move the ship forward if it is.
        if (navAgent.isStopped == true)
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
            /*
             * If the navagent is stopped (So there is no ammopickup available) and there is no target it will proceed to perform Idle rotations
             * as set in the rotateIdle IEnumerator
            */
            if (navAgent.isStopped == true && TargetShip == null)
            {  
                StartCoroutine(rotateIdle());
                transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 2f * Time.deltaTime);
            }
        } else
        {
            StopCoroutine(rotateIdle());
        }
        /*
         * Checks if there is any terrain in front of the ship using a raycast, if there is terrain
         * it will stop rotateIdle and it will proceed to set a new desiredRotation to rotate to.
         */
        if (Physics.Raycast(transform.position, transform.forward, 70, Rocks) && navAgent.isStopped == true)
        {
            StopCoroutine(rotateIdle());
            StartCoroutine(terrainEncounter());
        }
    }
    /*
     * Checks if the ship was damaged.
     */
    private void Damaged(int damage)
    {
        currentHP += damage;
        healthBar.SetHealth(currentHP);

    }
    private void OnTriggerEnter(Collider other)
    {
        /*
         * Checks if the player is colliding with a map boundary and will flip the ship if returns true
         */
        if (other.gameObject.tag == "wall") 
        {
            transform.rotation *= Quaternion.Euler(0, -180, 0);
        }
        /*
         * Damage check, if the ship is colliding with a cannonball or another ship it receive 20 damage.
         * It will also check the name of the gameobject it's colliding with and give a kill if the ship's health reaches 0
         * when it collides.
         */
        if (other.gameObject.tag == "canon" || other.gameObject.tag == "Boatbody" && other.gameObject != this)
        {
            Damaged(ballDamage);
            healthBar.SetHealth(currentHP);
            if (currentHP < 20 && (other.gameObject.name == "PataCannonBall(Clone)" || other.gameObject.name == "PataShip(Clone)"))
            {
                PataKills += 1;
            }
            if (currentHP < 20 && (other.gameObject.name == "OsledCannonBall(Clone)" || other.gameObject.name == "OsledShip(Clone)"))
            {
                OsledKills += 1;
            }
        }
        // Ammo & Health pickups
        if (other.gameObject.tag == "Ammo")
        {
            cannonAmmo += 10;
        }
        if (other.gameObject.tag == "Health")
        {
            currentHP += 20;
        }
    }
    /*
     * This is the script that checks whether the target is to the left or to the right of itself.
     */
    float AngleTowards(Vector3 direction, Vector3 target, Vector3 up)
    {
        Vector3 crossP = Vector3.Cross(direction, target);
        float leftright = Vector3.Dot(crossP, up);

        if (leftright > 0f)
        {
            return 1;
        }
        else if (leftright < 0f)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
    /*
     * Shooting functions, the chance shootBack is used is near-0 since no ship will be perfectly behind the JeroenShip gameobject.
     */
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
    // basic cooldown function
    public IEnumerator shootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(3);
        canShoot = true;
    }
    /*
     * If the ship has not detected any enemies or ammo objects, it will set a random desiredRotation every 5 seconds
     * and proceed to turn towards it.
     */
    public IEnumerator rotateIdle()
    {
        if(isRotating == false && navAgent.isStopped == true)
        {
            desiredRotation = transform.rotation;
            isRotating = true;
            int rotNum = Random.Range(0, 2);
            if (rotNum == 0 && TargetShip == null)
            {
                desiredRotation *= Quaternion.Euler(0, -90, 0);
                Debug.Log("rotating");
            }
            else if (rotNum == 1 && TargetShip == null)
            {
                desiredRotation *= Quaternion.Euler(0, 90, 0);
                Debug.Log("rotating");
            }
            yield return new WaitForSeconds(5f);
            isRotating = false;
        }
    }
    /*
     * Terrain encountering, if the ship's raycast detects terrain in front of it, it will set a different rotation
     */
     public IEnumerator terrainEncounter()
    {
        if (terrainAvoidance == false)
        {
            desiredRotation = transform.rotation;
            Debug.Log("terrain avoiding");
            terrainAvoidance = true;
            int rotNum = Random.Range(0, 2);
            if (rotNum == 0 && TargetShip == null)
            {
                desiredRotation *= Quaternion.Euler(0, -90, 0);
            }
            else if (rotNum == 1 && TargetShip == null)
            {
                desiredRotation *= Quaternion.Euler(0, -90, 0);
            }
            yield return new WaitForSeconds(1f);
            terrainAvoidance = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeroenAI : MonoBehaviour
{
    public GameObject thisShip;
    public GameObject TargetShip;
    public float sphereSize;
    Quaternion desiredRotation;
    public float angle;
    public float movementSpeed = 10;
    public int currentHP;
    public int maxHP = 100;
    public shiphealth healthBar;
    public int ballDamage;
    public int deaths;

    private void Start()
    {
        currentHP = maxHP;
    }
    void Update()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
        Debug.Log(desiredRotation);
        RaycastHit hit;
        if (Physics.SphereCast(thisShip.transform.position, 500, transform.forward, out hit, 1))
        {
            if (hit.transform.gameObject.tag == "Boatbody" && hit.transform.gameObject != this.gameObject)
            {
                TargetShip = hit.transform.gameObject;
            }
            else
            {
                TargetShip = null;
            }
        }
        if (TargetShip != null)
        {
            Vector3 posRelative = TargetShip.transform.position - transform.position;
            desiredRotation = Quaternion.LookRotation(posRelative);
            desiredRotation *= Quaternion.Euler(0, -90, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 0.5f * Time.deltaTime);
        }
    }
    private void Damaged(int damage)
    {
        currentHP += damage;
        healthBar.SetHealth(currentHP);
        if (currentHP == 0)
        {
            deaths += 1;
            currentHP = 100;
            healthBar.SetHealth(currentHP);
        }

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
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 500);
    }
}

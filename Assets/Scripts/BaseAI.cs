using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannedRobotEvent {
    public string Name;
    public float Distance; 
}

public class BaseAI
{
    public PirateShipController Ship = null;

    // Events
    public virtual void OnScannedRobot(ScannedRobotEvent e)
    {
        // 
    }

    public IEnumerator Ahead(float distance) {
        yield return Ship.__Ahead(distance);
    }

    public IEnumerator Back(float distance) {
        yield return Ship.__Back(distance);
    }

    public IEnumerator TurnLookoutLeft(float angle) {
        yield return Ship.__TurnLookoutLeft(angle);
    }

    public IEnumerator TurnLookoutRight(float angle) {
        yield return Ship.__TurnLookoutRight(angle);
    }

    public IEnumerator TurnLeft(float angle) {
        yield return Ship.__TurnLeft(angle);
    }

    public IEnumerator TurnRight(float angle) {
        yield return Ship.__TurnRight(angle);
    }

    public IEnumerator FireFront(float power) {
        yield return Ship.__FireFront(power);
    }

    public IEnumerator FireLeft(float power) {
        yield return Ship.__FireLeft(power);
    }

    public IEnumerator FireRight(float power) {
        yield return Ship.__FireRight(power);
    }

    public virtual IEnumerator RunAI() {
        yield return null;
    }
}

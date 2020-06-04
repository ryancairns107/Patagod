using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PataAI : BaseAI
{
    public override IEnumerator RunAI()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return Ahead(100);
            yield return FireFront(3);
            yield return TurnLookoutLeft(50);
            yield return TurnLeft(360);
            yield return FireLeft(2);
            yield return TurnLookoutRight(75);
            yield return Back(100);
            yield return FireRight(1);
            yield return TurnLookoutLeft(80);
            yield return TurnRight(90);
        }
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
    }
}

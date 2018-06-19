using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PeopleOnBoat {FAILED, P, D, PP, DD, PD }
public enum BoatLocation { Left, Right }

public class status
{
    public int[] currentStatus;
    public PeopleOnBoat peopleOnBoat;
    public BoatLocation boatLocation;
    public status prev;

    public status(int startPriestCount,int startDevilCount,int endPriestCount,int endDevilCount, BoatLocation boatLocation = BoatLocation.Left, PeopleOnBoat peopleOnBoat = PeopleOnBoat.FAILED, status prevStatus = null)
    {
        currentStatus = new int[4];
        currentStatus[0] = startPriestCount;
        currentStatus[1] = startDevilCount;
        currentStatus[2] = endPriestCount;
        currentStatus[3] = endDevilCount;
        this.peopleOnBoat = peopleOnBoat;
        this.boatLocation = boatLocation;
        prev = prevStatus;
    }
    public bool Equals(status cStatus)
    {
        return currentStatus.Equals(cStatus.currentStatus);
    }
}

public class AI
{
    private bool Check(status cStatus)
    {
        return (cStatus.currentStatus[0] >= cStatus.currentStatus[1] || cStatus.currentStatus[0] == 0) &&
            (cStatus.currentStatus[2] >= cStatus.currentStatus[3] || cStatus.currentStatus[2] == 0);
    }

    private PeopleOnBoat BFSeach(status sStatus)
    {
        Queue<status> queue = new Queue<status>();
        queue.Enqueue(sStatus);

        status cStatus;

        while (queue.Count != 0)
        {
            cStatus = queue.Dequeue();
            status nextStatus;
            if (cStatus.currentStatus[2] + cStatus.currentStatus[3] == 6)
            {
                while(cStatus.prev != null && cStatus.prev.prev != null)
                {
                    cStatus = cStatus.prev;
                }
                Debug.Log(cStatus.peopleOnBoat);
                return cStatus.peopleOnBoat;
            }
            
            if(cStatus.boatLocation == BoatLocation.Left)
            {
                if(cStatus.currentStatus[0] > 0)
                {
                    nextStatus = new status(cStatus.currentStatus[0] - 1, cStatus.currentStatus[1],
                         cStatus.currentStatus[2] + 1, cStatus.currentStatus[3], BoatLocation.Right, PeopleOnBoat.P, cStatus);
                    if (Check(nextStatus)) queue.Enqueue(nextStatus);
                }
                if(cStatus.currentStatus[0] > 1)
                {
                    nextStatus = new status(cStatus.currentStatus[0] - 2, cStatus.currentStatus[1],
                        cStatus.currentStatus[2] + 2, cStatus.currentStatus[3], BoatLocation.Right, PeopleOnBoat.PP, cStatus);
                    if (Check(nextStatus)) queue.Enqueue(nextStatus);
                }
                if (cStatus.currentStatus[1] > 0)
                {
                    nextStatus = new status(cStatus.currentStatus[0], cStatus.currentStatus[1] - 1,
                        cStatus.currentStatus[2], cStatus.currentStatus[3] + 1, BoatLocation.Right, PeopleOnBoat.D, cStatus);
                    if (Check(nextStatus)) queue.Enqueue(nextStatus);
                }
                if (cStatus.currentStatus[1] > 1)
                {
                    nextStatus = new status(cStatus.currentStatus[0], cStatus.currentStatus[1] - 2,
                        cStatus.currentStatus[2], cStatus.currentStatus[3] + 2, BoatLocation.Right, PeopleOnBoat.DD, cStatus);
                    if (Check(nextStatus)) queue.Enqueue(nextStatus);
                }
                if (cStatus.currentStatus[0] > 0 && cStatus.currentStatus[1] > 0)
                {
                    nextStatus = new status(cStatus.currentStatus[0] - 1, cStatus.currentStatus[1] - 1,
                        cStatus.currentStatus[2] + 1, cStatus.currentStatus[3] + 1, BoatLocation.Right, PeopleOnBoat.PD, cStatus);
                    if (Check(nextStatus)) queue.Enqueue(nextStatus);
                }
            }
            else
            {
                if (cStatus.currentStatus[2] > 0)
                {
                    nextStatus = new status(cStatus.currentStatus[0] + 1, cStatus.currentStatus[1],
                        cStatus.currentStatus[2] - 1, cStatus.currentStatus[3], BoatLocation.Left, PeopleOnBoat.P, cStatus);
                    if (Check(nextStatus)) queue.Enqueue(nextStatus);
                }
                if (cStatus.currentStatus[2] > 1)
                {
                    nextStatus = new status(cStatus.currentStatus[0] + 2, cStatus.currentStatus[1],
                        cStatus.currentStatus[2] - 2, cStatus.currentStatus[3], BoatLocation.Left, PeopleOnBoat.PP, cStatus);
                    if (Check(nextStatus)) queue.Enqueue(nextStatus);
                }
                if (cStatus.currentStatus[3] > 0)
                {
                    nextStatus = new status(cStatus.currentStatus[0], cStatus.currentStatus[1] + 1,
                        cStatus.currentStatus[2], cStatus.currentStatus[3] - 1, BoatLocation.Left, PeopleOnBoat.D, cStatus);
                    if (Check(nextStatus)) queue.Enqueue(nextStatus);
                }
                if (cStatus.currentStatus[3] > 1)
                {
                    nextStatus = new status(cStatus.currentStatus[0] - 1, cStatus.currentStatus[1] + 2,
                        cStatus.currentStatus[2], cStatus.currentStatus[3] - 2, BoatLocation.Left, PeopleOnBoat.DD, cStatus);
                    if (Check(nextStatus)) queue.Enqueue(nextStatus);
                }
                if (cStatus.currentStatus[2] > 0 && cStatus.currentStatus[3] > 0)
                {
                    nextStatus = new status(cStatus.currentStatus[0] + 1, cStatus.currentStatus[1] + 1,
                        cStatus.currentStatus[2] - 1, cStatus.currentStatus[3] - 1, BoatLocation.Left, PeopleOnBoat.PD, cStatus);
                    if (Check(nextStatus)) queue.Enqueue(nextStatus);
                }
            }
        }

        return PeopleOnBoat.FAILED;
    }

    public bool Run()
    {
        FirstSceneController action = SSDirector.getInstance().currentSceneController as FirstSceneController;
        if (action.onBoatDevil.Count + action.onBoatPriest.Count == 0)
        {
            BoatLocation location = action.boat.transform.position.x < 0 ? BoatLocation.Left : BoatLocation.Right;
            status cStatus = new status(action.startPriest.Count, action.startDevil.Count, action.endPriest.Count, action.endDevil.Count, location);
            PeopleOnBoat peopleOnBoat = BFSeach(cStatus);
            if (location == BoatLocation.Left)
            {
                if (peopleOnBoat == PeopleOnBoat.P)
                {
                    action.actionManager.OnBoat(action.startPriest[0],action.actionManager);
                }
                else if (peopleOnBoat == PeopleOnBoat.PP)
                {
                    action.actionManager.OnBoat(action.startPriest[0], action.actionManager);
                    action.actionManager.OnBoat(action.startPriest[action.startPriest.Count - 1], action.actionManager);
                }
                else if (peopleOnBoat == PeopleOnBoat.D)
                {
                    action.actionManager.OnBoat(action.startDevil[0], action.actionManager);
                }
                else if (peopleOnBoat == PeopleOnBoat.DD)
                {
                    action.actionManager.OnBoat(action.startDevil[0], action.actionManager);
                    action.actionManager.OnBoat(action.startDevil[action.startDevil.Count - 1], action.actionManager);
                }
                else if (peopleOnBoat == PeopleOnBoat.PD)
                {
                    action.actionManager.OnBoat(action.startDevil[0], action.actionManager);
                    action.actionManager.OnBoat(action.startPriest[0], action.actionManager);
                }
                else return false;
            }
            else
            {
                if (peopleOnBoat == PeopleOnBoat.P)
                {
                    action.actionManager.OnBoat(action.endPriest[0],action.actionManager);
                }
                else if (peopleOnBoat == PeopleOnBoat.PP)
                {
                    action.actionManager.OnBoat(action.endPriest[0],action.actionManager);
                    action.actionManager.OnBoat(action.endPriest[action.endPriest.Count - 1],action.actionManager);
                }
                else if (peopleOnBoat == PeopleOnBoat.D)
                {
                    action.actionManager.OnBoat(action.endDevil[0],action.actionManager);
                }
                else if (peopleOnBoat == PeopleOnBoat.DD)
                {
                    action.actionManager.OnBoat(action.endDevil[0],action.actionManager);
                    action.actionManager.OnBoat(action.endDevil[action.endDevil.Count - 1],action.actionManager);
                }
                else if (peopleOnBoat == PeopleOnBoat.PD)
                {
                    action.actionManager.OnBoat(action.endDevil[0],action.actionManager);
                    action.actionManager.OnBoat(action.endPriest[0],action.actionManager);
                }
                else return false;
            }
        }
        else if (action.actionManager.moveToAction == null || !action.actionManager.moveToAction.enable)
        {
            action.actionManager.MoveBoat(action.boat,action.actionManager);
        }
        
        return true;
    }
}

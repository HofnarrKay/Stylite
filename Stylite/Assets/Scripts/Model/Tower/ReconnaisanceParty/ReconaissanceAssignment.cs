using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReconaissanceAssignment : GameComponent
{
    private List<SoldierJob> availableSoldiers;
    private List<SoldierJob> unusedSoldiers;

    private ReconnaissanceParty reconnaissanceParty;
    private UnitTemplate[] templates;

    public override void Setup()
    {
        availableSoldiers = new List<SoldierJob>();
        unusedSoldiers = new List<SoldierJob>();
        reconnaissanceParty = new ReconnaissanceParty(5);
        templates = new UnitTemplate[5];

        reconnaissanceParty.Setup();
    }

    public void AddUnitTemplate(UnitTemplate unit, int index)
    {
        templates[index] = unit;
    }

    //public void AddSoldier(SoldierJob soldier)
    //{
    //    availableSoldiers.Add(soldier);
    //    soldier.addedWorker += OnWorkerAddedToSoldierJob;
    //    soldier.removedWorker += OnWorkerRemovedFromSoldierJob;
    //}

    //public void OnWorkerAddedToSoldierJob(Job job)
    //{
    //    if (unusedSoldiers.Contains((SoldierJob)job))
    //    {
    //        unusedSoldiers.Remove((SoldierJob)job);
    //    }

    //    if(!availableSoldiers.Contains((SoldierJob)job))
    //    {
    //        availableSoldiers.Add((SoldierJob)job);
    //    }
    //    CheckReconaissancePartyAvailability();
    //}

    //public void OnWorkerRemovedFromSoldierJob(Job job, Worker worker)
    //{
    //    if(availableSoldiers.Contains((SoldierJob)job))
    //    {
    //        availableSoldiers.Remove((SoldierJob)job);
    //    }

    //    if(!unusedSoldiers.Contains((SoldierJob)job))
    //    {
    //        unusedSoldiers.Add((SoldierJob)job);
    //    }
    //    CheckReconaissancePartyAvailability();
    //}

    //public void CheckReconaissancePartyAvailability()
    //{
    //    foreach(Worker worker in reconnaissanceParty.Soldiers.ToArray())
    //    {
    //        if(!worker.HasJob())
    //        {
    //            reconnaissanceParty.RemoveWorker(worker);
    //        }
    //    }

    //    if (reconnaissanceParty.maxSize > reconnaissanceParty.Soldiers.Count)
    //    {
    //        foreach (SoldierJob soldier in availableSoldiers)
    //        {
    //            bool soldierInReconaissance = false;
    //            foreach (var soldierInReconnaisance in reconnaissanceParty.Soldiers.ToArray())
    //            {
    //                if (soldierInReconnaisance.Job == soldier) soldierInReconaissance = true;
    //            }

    //            if(!soldierInReconaissance)
    //            {
    //                reconnaissanceParty.AddWorker(soldier.Worker);
    //            }

    //            if (reconnaissanceParty.Soldiers.Count == reconnaissanceParty.maxSize) return;
    //        }
    //    }
    //}

    public ReconnaissanceParty GetReconaissanceParty() => reconnaissanceParty;
}

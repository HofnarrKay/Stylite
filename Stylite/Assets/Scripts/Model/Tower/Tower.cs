using System;
using System.Collections.Generic;
using UnityEngine;

public struct TowerData
{
    public Dictionary<Position, TowerFloorData> Floors;
    public List<Position> AvailableBuildingSpots;
    public ResourcesData Resources;
    public UnitLoadoutData unitLoadoutData;
    public SpellContainerData SpellContainer;

    public TowerData(Dictionary<Position, TowerFloor> floors, List<Position> availableBuildingSpots, Resources resources, UnitLoadout unitLoadout, SpellContainer spellContainer)
    {
        Floors = new Dictionary<Position, TowerFloorData>();

        foreach (var floor in floors)
        {
            Floors.Add(floor.Key, floor.Value.GetData());
        }

        AvailableBuildingSpots = availableBuildingSpots;
        Resources = resources.GetData();

        unitLoadoutData = unitLoadout.GetData();

        SpellContainer = spellContainer.GetData();
    }
}

public class Tower : GameComponent
{
    private Resources resources;
    private Dictionary<Position, TowerFloor> floors;
    private List<Position> availableBuildingSpots;
    private Population population;
    private JobAssignment jobAssignment;
    private ReconaissanceAssignment reconaissanceAssignment;
    private SpellContainer spellContainer;
    private UnitLoadout unitLoadout;

    public int floorCount => floors.Count;



    public override void Setup()
    {
        resources = new Resources();
        floors = new Dictionary<Position, TowerFloor>();
        availableBuildingSpots = new List<Position>();

        population = new Population();
        population.Setup();
        population.InhabitantBorn += OnPopulationGrowth;

        jobAssignment = new JobAssignment();
        jobAssignment.Setup();

        reconaissanceAssignment = new ReconaissanceAssignment();
        reconaissanceAssignment.Setup();

        unitLoadout = new UnitLoadout();
        Bind(unitLoadout);
        unitLoadout.Setup();

        spellContainer = new SpellContainer();
        Bind(spellContainer);
        spellContainer.Setup();
        spellContainer.AddSockets(9);
        spellContainer.AddSpell(new SpellComponent(), 0);

        CalculateBuildingSpots();
    }

    public override void Process(float deltaTime)
    {
        population.Process(deltaTime);
        foreach (var floor in floors)
        {
            floor.Value.Process(deltaTime);
        }
    }

    public override void Shutdown()
    {
        jobAssignment.Shutdown();
        population.Shutdown();
    }

    public void AddFloor(Position position, TowerFloor floor)
    {
        //Building position check
        if (position.Height < floors.Count)
        {
            floors[position].Shutdown();
            floors[position] = floor;
        }
        else
        {
            floors.Add(position, floor);
        }

        //Floor connection
        floor.ConnectToTower(this);
        CalculateBuildingSpots();

        foreach (var job in floor.GetJobs())
        {
            job.AddedJobToTower(this);
            jobAssignment.AddJob(job); 
        }
    }

    public bool ContainsResources(Resources resources)
    {
        return this.resources.Contains(resources);
    }

    public void ReduceResources(Resources resources)
    {
        this.resources.Subtract(resources);
    }

    public void OnPopulationGrowth(Inhabitant inhabitant)
    {
        AssignWorker(new Worker(inhabitant));
    }

    public Inhabitant CreateInhabitant(float age, float educationLevel, string name) => population.CreateInhabitant(age, educationLevel, name);

    public bool AssignWorker(Worker worker) => jobAssignment.AssignWorker(worker);

    private void CalculateBuildingSpots()
    {
        availableBuildingSpots.Clear();

        availableBuildingSpots.Add(new Position(floorCount));
    }

    public void ConnectResourceProductionFloorToTower(ResourceProductionFloor towerFloor)
    {
        towerFloor.Production += OnResourceProduction;
    }

    public bool Contains(Resources resources)
    {
        return resources.Contains(resources);
    }

    public void Subtract(Resources resources)
    {
        resources.Subtract(resources);
    }

    public bool CheckForBuildingSpots(Position position)
    {
        foreach (Position buildingPosition in availableBuildingSpots)
        {
            if(buildingPosition.Equals(position))
            {
                return true;
            }
        }
        return false;
    }

    public void OnResourceProduction(Resource resource, ProductionEnvironment productionEnvironment)
    {
        resources.Add(resource);
    }

    public void AddedSoldierJob(SoldierJob soldier)
    {
    }

    public void ChangeJobImportance(Position position, int jobIndex, float newValue)
    {
        floors[position].GetJobs()[jobIndex].ChangeImportance(newValue);  
    }

    public void ChangeJobState(Position position, int jobIndex, bool isOpen)
    {
        jobAssignment.ChangeJobState(floors[position].GetJobs()[jobIndex], isOpen);
    }

    public void AddUnitTemplate(UnitTemplateData unitTemplate, int index)
    {
    }

    public void AddNewUnitTemplate(UnitTemplate unit)
    {
        unitLoadout.AddUnit(unit);
    }

    public ReconnaissanceParty GetReconaissanceParty() => reconaissanceAssignment.GetReconaissanceParty();

    public TowerData getData() => new TowerData(floors, availableBuildingSpots, resources, unitLoadout, spellContainer);
}

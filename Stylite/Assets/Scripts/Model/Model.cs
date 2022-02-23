using System.Collections.Generic;
using UnityEngine;

public struct ModelData
{
    public ActionsData Actions;
    public TowerData Tower;
    public EnvironmentData Environment;

    public ModelData(Tower tower,  Actions actions, Environment environment)
    {
        Actions = actions.getData();
        Tower = tower.getData();
        Environment = environment.getData();
    }
}

public class Model : GameComponent
{
    static Model instance;

    private Actions actions;
    private Tower tower;
    private Environment environment;

    public static Model Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Model();
                instance.Setup();
            }
            return instance;
        }
    }

    private List<GameComponent> gameComponents;

    public override void Setup()
    {
        actions = new Actions();

        gameComponents = new List<GameComponent>();

        environment = new Environment();
        Bind(environment);
        environment.Setup();
        gameComponents.Add(environment);

        tower = new Tower();
        Bind(tower);
        tower.Setup();
        gameComponents.Add(tower);



        Resources actionCost = new Resources();
        actionCost.Add(new Resource("Refined Marble", 100));
        actions.AddAction(new UnitTemplateAction("UnitTemplateAction", actionCost)); 


        //Building Action Mining District
        Resources costsMineral = new Resources();
        costsMineral.Add(new Resource("Marble", 100));

        //create Jobs
        Job[] jobsMineralProduction = new Job[3];

        Resources mineralProduction = new Resources();
        mineralProduction.Add(new Resource("Refined Marble", 200));
        mineralProduction.Add(new Resource("Marble", -200));

        jobsMineralProduction[0] = new Job(mineralProduction, "Converter", 2.0f, 0);
        jobsMineralProduction[1] = new Job(mineralProduction.Duplicate(), "Converter", 1.0f, 1);
        jobsMineralProduction[2] = new Job(mineralProduction.Duplicate(), "Converter", 1.0f, 2);

        ResourceProductionFloor workshop = new ResourceProductionFloor("Workshop", jobsMineralProduction);
        actions.AddAction(new BuildingAction("BuildingAction", costsMineral, workshop, tower));

        //Building Action Farming District
        Resources costsFood = new Resources();
        costsFood.Add(new Resource("Marble", 100));

        //create Jobs
        Job[] jobsFoodProduction = new Job[1];

        Resources production = new Resources();
        production.Add(new Resource("Food", 6));

        jobsFoodProduction[0] = new Job(production, "Farmer", 3.0f, 0);


        ResourceProductionFloor farm = new ResourceProductionFloor("Farm", jobsFoodProduction);
        actions.AddAction(new BuildingAction("BuildingAction", costsFood, farm, tower));


        //Building Action Farming District
        Resources costsResearch = new Resources();
        costsResearch.Add(new Resource("Marble", 500));

        //create Jobs
        Job[] jobsResearchProduction = new Job[2];

        Resources productionResearch = new Resources();
        productionResearch.Add(new Resource("Research", 6));
        productionResearch.Add(new Resource("Marble", -10));

        jobsResearchProduction[0] = new Job(productionResearch, "Researcher", 3.0f, 0);
        jobsResearchProduction[1] = new Job(productionResearch.Duplicate(), "Researcher", 3.0f, 1);

        ResourceProductionFloor laboratory = new ResourceProductionFloor("Laboratory", jobsResearchProduction);
        actions.AddAction(new BuildingAction("BuildingAction", costsFood, laboratory, tower));


        //Add Missions
        environment.AddMission(new MissionOption("Scavenge ruins"));
        environment.AddMission(new MissionOption("Pillage the industry"));
        environment.AddMission(new MissionOption("Capture the travelling magistocrat"));


        Job[] jobs = new Job[0];
        //jobs[0] = new Job(resourceProduction, "Elder", 0.0f, 0);

        tower.AddFloor(new Position(0), new ResourceProductionFloor("Abandoned", jobs));
        tower.AddFloor(new Position(1), farm.Duplicate());
        tower.AddFloor(new Position(2), workshop.Duplicate());
        tower.AddFloor(new Position(3), laboratory.Duplicate());
        tower.AddFloor(new Position(4), workshop.Duplicate());

        Worker worker = new Worker(tower.CreateInhabitant(20, 80, "Benedikt"));
        tower.AssignWorker(worker);

        Worker workerGoon = new Worker(tower.CreateInhabitant(26, 50, "Karthus"));
        tower.AssignWorker(workerGoon);

        for (int i = 0; i < 5; i++)
        {
            Worker workerX = new Worker(tower.CreateInhabitant(50, 30, "Goon" + i.ToString()));
            tower.AssignWorker(workerX);
        }
    }

    public override void Process(float deltaTime)
    {
        foreach (var component in gameComponents)
        {
            component.Process(deltaTime);
        }
    }

    public override void OnCreatedAction(ModelAction action) => actions.AddAction(action);

    public override void OnExpandActionRequest(ExpandActionArguments expandActionArguments) => actions.ExpandAction(expandActionArguments);

    public void CallAction(ActionArguments arguments) => actions.CallAction(arguments, this);

    public bool ContainsResources(Resources resources) => tower.ContainsResources(resources);

    public void ReduceResources(Resources resources) => tower.ReduceResources(resources);

    public void EmbarkOnMission(string key) => environment.EmbarkOnMission(key, tower.GetReconaissanceParty());

    public void ChangeJobImportance(Position position, int jobIndex, float newValue) => tower.ChangeJobImportance(position, jobIndex, newValue);

    public void ChangeJobState(Position position, int jobIndex, bool isOpen) => tower.ChangeJobState(position, jobIndex, isOpen);

    public void AddUnitTemplate(UnitTemplateData unit, int index) => tower.AddUnitTemplate(unit, index);
    
    public void AddNewUnitTemplate(UnitTemplate unit) => tower.AddNewUnitTemplate(unit);

    public ModelData GetData() => new ModelData(tower, actions, environment);
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : GameComponent
{
    public Action<Inhabitant> InhabitantBorn;
    public Action<Inhabitant> InhabitantDied;

    private List<Inhabitant> inhabitants;

    private float currentGrowth = 0;
    private float neededGrowthForBirth = 5;

    public override void Setup()
    {
        inhabitants = new List<Inhabitant>();
    }

    public override void Shutdown()
    {
    }

    public override void Process(float deltaTime)
    {
        CalculatePopulationGrowth(deltaTime);
    }

    private void CalculatePopulationGrowth(float deltaTime)
    {
        currentGrowth += deltaTime;

        if(currentGrowth > neededGrowthForBirth)
        {
            currentGrowth %= neededGrowthForBirth;
            InhabitantBorn?.Invoke(CreateInhabitant(0, 0, "Rando"));
        }
    }

    public Inhabitant CreateInhabitant(float age, float educationLevel, string name)
    {
        Inhabitant inhabitant = new Inhabitant(age, educationLevel, name);
        inhabitants.Add(inhabitant);
        return inhabitant;
    }


}

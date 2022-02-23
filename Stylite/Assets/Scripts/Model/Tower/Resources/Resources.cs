using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct ResourcesData
{
    public Dictionary<string, ResourceData> resources;
    public List<ResourceData> ResourceList => resources.Values.ToList();

    public ResourcesData(Dictionary<string, Resource> resources)
    {
        this.resources = new Dictionary<string, ResourceData>();

        foreach (var resource in resources)
        {
            this.resources.Add(resource.Key, resource.Value.GetData()); 
        }
    }
}


public class Resources : GameComponent
{
    Dictionary<string, Resource> resources;

    public List<Resource> ResourceList => resources.Values.ToList();

    public Resources() => resources = new Dictionary<string, Resource>();

    public bool EqualsData(ResourcesData costs)
    {
        foreach (var resourcesPair in resources.ToArray())
        {
            if(costs.resources.ContainsKey(resourcesPair.Key))
            {
                if (costs.resources[resourcesPair.Key].amount != resourcesPair.Value.Amount) return false;
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public void Add(Resource gainedResource)
    {
        if(resources.ContainsKey(gainedResource.Key))
        {
            resources[gainedResource.Key].Add(gainedResource.Amount);
            return;
        }
        resources.Add(gainedResource.Key ,gainedResource);
    }


    public void Subtract(Resources substractedResources)
    {
        foreach (Resource resource in substractedResources.resources.Values.ToList())
        {
            Subtract(resource);
        }
    }

    public void Subtract(Resource substractedResource)
    {
        if (resources.ContainsKey(substractedResource.Key))
        {
            resources[substractedResource.Key].Add(-substractedResource.Amount);
            return;
        }
        resources.Add(substractedResource.Key, substractedResource);
    }

    public bool Contains(Resources resources)
    {
        foreach (var resource in resources.resources)
        {
            if (!this.resources.ContainsKey(resource.Key)) return false;
            if (resource.Value.Amount > this.resources[resource.Key].Amount) return false;
        }

        return true;
    }

    public Resources Duplicate()
    {
        Resources resources = new Resources();
        foreach (var valuePair in this.resources)
        {
            resources.Add(valuePair.Value.Duplicate());
        }
        return resources;
    }

    public ResourcesData GetData() => new ResourcesData(resources);
}

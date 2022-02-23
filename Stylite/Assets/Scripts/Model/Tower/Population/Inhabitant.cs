using System.Collections.Generic;

public class InhabitantData
{
    public string Name;
    public float Age = 0;
    public float EducationLevel = 0;

    public InhabitantData(string name, float age, float educationLevel)
    {
        Name = name;
        Age = age;
        EducationLevel = educationLevel;
    }
}

public class Inhabitant
{
    private string name;
    private float age = 0;
    private float educationLevel = 0;
    private Dictionary<string, Experience> experiences;

    public string Name
    {
        get => name;
    }
    public float Age
    {
        get => age;
    }
    public float EducationLevel
    {
        get => educationLevel;
    }


    public Inhabitant(float age, float educationLevel, string name = "Flavius")
    {
        this.age = age;
        this.educationLevel = educationLevel;
        this.name = name;
        experiences = new Dictionary<string, Experience>();
    }

    public void AddExperience(Experience experience)
    {
        if (experiences == null) experiences = new Dictionary<string, Experience>();
        experiences.Add(experience.Key, experience);
    }

    public Experience GetExperience(string key)
    {
        if (experiences.ContainsKey(key)) return experiences[key];
        return null;
    }

    public InhabitantData GetData() => new InhabitantData(name, age, educationLevel);
}

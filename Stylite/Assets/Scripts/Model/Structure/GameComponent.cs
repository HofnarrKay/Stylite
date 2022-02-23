using System;

public class GameComponent
{
    public Action<ModelAction> CreatedAction;
    public Action<ExpandActionArguments> ExpandActionRequest;

    public virtual void Setup()
    {

    }

    public virtual void Process(float deltaTime)
    {

    }

    public virtual void Shutdown()
    {

    }

    public void Bind(GameComponent gameComponent)
    {
        gameComponent.CreatedAction += OnCreatedAction;
        gameComponent.ExpandActionRequest += OnExpandActionRequest;
    }

    public void Unbind(GameComponent gameComponent)
    {
        gameComponent.CreatedAction -= OnCreatedAction;
        gameComponent.ExpandActionRequest -= OnExpandActionRequest;
    }

    public virtual void OnExpandActionRequest(ExpandActionArguments expandActionArguments) => ExpandActionRequest?.Invoke(expandActionArguments);

    public virtual void OnCreatedAction(ModelAction modelAction) => CreatedAction?.Invoke(modelAction);
}

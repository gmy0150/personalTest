using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyRunner 
{
    private Strategy runnableStrategy = null;
    public void Run()
    {
        if (runnableStrategy == null) return;
        runnableStrategy.Run();
    }
    public void SetStrategy(Strategy strategy)
    {
        this.runnableStrategy = strategy;
    }
    public Strategy GetStrategy()
    {
        return this.runnableStrategy;
    }
    public void Update()
    {
        if (runnableStrategy != null)
        {
            runnableStrategy.Update();
        }
    }
}

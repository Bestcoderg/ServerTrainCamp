using System;
using System.Collections.Generic;

public class PlayerStateManager
{
    public PlayerStateManager()
    {
        psm = new Dictionary<int, PlayerState>();
    }

    private Dictionary<int, PlayerState> psm;


    public PlayerState GetPlayerState(int id)
    {
        return psm[id];
    }

    public void AddPlayerState(int id, PlayerState ps)
    {
        psm.Add(id, ps);
    }

    public bool RemovePlayerState(int id)
    {
        return psm.Remove(id);
    }

    public bool Contains(int id)
    {
        return psm.ContainsKey(id);
    }
}


public class PlayerState
{
    private double upDown;
    private double leftRight;
    private double beta;
    private int nth;

    public PlayerState(double beta, double upDown = 0, double leftRight=0)
    {
        this.upDown = upDown;
        this.leftRight = leftRight;
        this.beta = beta;
        nth = 1;
    }
    public double GetUpDown()
    {
        return upDown;
    }

    public double GetLeftRight()
    {
        return leftRight;
    }

    public void ExpAvgState(double upDown, double leftRight)
    {
        this.upDown = (this.upDown * beta + (1 - beta) * upDown);
        this.upDown /= (1 - Math.Pow(beta, nth));
        this.leftRight = (this.upDown * beta + (1 - beta) * upDown);
        this.leftRight /= (1 - Math.Pow(beta, nth));
        nth += 1;
    }
}

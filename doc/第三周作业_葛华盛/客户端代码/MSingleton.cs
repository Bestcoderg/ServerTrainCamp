using System;

public abstract class MBaseSingleton
{
    public abstract bool Init();
    public abstract void Uninit();
    public abstract bool IsInited { get; }
    public abstract void OnLogout(bool clearAccountData = true);
}

public abstract class MSingleton<T> : MBaseSingleton where T : class, new()
{
    private bool _isInited = false;

    protected MSingleton()
    {
        if (null != _instance)
        {
            throw new Exception(_instance.ToString() + @" can not be created again.");
        }
        _isInited = false;
    }

    private static readonly T _instance = new T();

    public static T singleton
    {
        get
        {
            return _instance;
        }
    }

    public override bool Init()
    {
        _isInited = true;
        return _isInited;
    }
    public override void Uninit()
    {
        _isInited = false;
    }

    public override bool IsInited
    {
        get { return _isInited; }
    }

    public override void OnLogout(bool clearAccountData = true) { }

}

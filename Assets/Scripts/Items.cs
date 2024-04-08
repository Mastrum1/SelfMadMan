using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items
{
    public Player _mPlayer;
    private Sprite _sprite;
    public Sprite Sprite { get => _sprite; set => _sprite = value; }

    public virtual void Obtain()
    {

    }
}

public class Coins : Items
{
    private int _amount;
    public int Amount { get => _amount; set => _amount = value; }

    public override void Obtain()
    {
        _mPlayer = GameManager.instance.GetComponent<Player>();
        _mPlayer.NewCurrency(_mPlayer.Money + Amount);
        //put amount in money
    }
}


public class MinigameItem : Items
{
    private string _sceneName;
    public string SceneName { get => _sceneName; set => _sceneName = value; }

    public override void Obtain()
    {
        Debug.Log("ta race" + SceneName);
        MiniGameSelector.instance.UnlockMinigame(SceneName);
    }
}

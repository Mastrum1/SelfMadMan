using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

[System.Serializable]
public class Items
{
    protected Player _mPlayer;
    private Sprite _sprite;
    public Sprite Sprite { get => _sprite; set => _sprite = value; }

    public virtual void Obtain()
    {

    }

    public virtual void Use()
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
        MiniGameSelector.instance.UnlockMinigame(SceneName);
    }
}

[System.Serializable]
public class UsableItem : Items
{
    int ID;
    public int _ID { get => ID; set => ID = value; }

    int _Quantity;
    public int Quantity { get => _Quantity; set => _Quantity = value; }

    public override void Obtain()
    {
        //use item
        _mPlayer = GameManager.instance.GetComponent<Player>();
        _mPlayer.AddUsableItemInInventory(this);

    }

    public override void Use()
    {
        _mPlayer = GameManager.instance.GetComponent<Player>();
        _mPlayer.UseUsableItem(this);
    }
}

[System.Serializable]
public class FournituresClass : Items
{
    int ID;
    public int _ID { get => ID; set => ID = value; }

    public FournituresClass(int iD)
    {
        _ID = iD;
    }
}

public class FournituresClassSO : Items
{
    ItemsSO _ItemsSO;

    public int GetItemSOID()
    {
        return _ItemsSO.ID;
    }
    public override void Obtain()
    {
        _mPlayer = GameManager.instance.GetComponent<Player>();
        _mPlayer.AddFournitureInInventory(this);

    }
}

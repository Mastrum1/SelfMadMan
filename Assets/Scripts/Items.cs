using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Items
{
    protected Player _mPlayer; //TO DELETE
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
        MoneyManager.Instance.AddMoney(Amount);
        MoneyManager.Instance.UpdateMoney();
    }
}


public class MinigameItem : Items
{
    private string _sceneName;
    public string SceneName { get => _sceneName; set => _sceneName = value; }

    public override void Obtain()
    {
        MiniGameSelector.instance.UnlockMinigame(SceneName);
        QuestManager.Instance.UnlockQuest(SceneName);
    }
}

public class FurnitureItem : Items
{

    private string _prefabName;
    public string PrefabName { get => _prefabName; set => _prefabName = value; }

    public override void Obtain()
    {
        //FurnitureManager.instance.UnlockFurniture(PrefabName);
    }
}

[System.Serializable]
public class UsableItem : Items //TO DELETE
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
public class FournituresClass : Items // TO DELETE
{
    int ID;
    public int _ID { get => ID; set => ID = value; }

    public FournituresClass(int iD)
    {
        _ID = iD;
    }
}

public class FournituresClassSO : Items //TO DELETE
{
    ItemsSO _ItemsSO;

    public int GetItemSOID()
    {
        return _ItemsSO.ID;
    }
    public override void Obtain()
    {
       /* _mPlayer = GameManager.instance.GetComponent<Player>();
        _mPlayer.AddFournitureInInventory(this);*/

    }
}

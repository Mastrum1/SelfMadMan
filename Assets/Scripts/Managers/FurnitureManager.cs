using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class FurnitureManager : MonoBehaviour
{
    public enum FurnitureType { FRAME, STATUE, OBJECT }
    private static FurnitureManager _instance;
    public static FurnitureManager instance => _instance;
    // Start is called before the first frame update
    [System.Serializable]
    public class Furniture
    {
        private GameObject _activeGO = null;
        public GameObject ActiveGO { get => _activeGO; set => _activeGO = value; }

        [SerializeField] private GameObject _prefabParent;
        public GameObject PrefabParent { get => _prefabParent; set => _prefabParent = value; }

        [SerializeField] private List<GameObject> _GOPerEra = new List<GameObject>();
        public List<GameObject> GOPerEra { get => _GOPerEra; set => _GOPerEra = value; }

        public bool Picked;
        public bool Locked;
        [SerializeField] public FurnitureType Type;

        public void UnlockFunriture()
        {
            Locked = true;
        }

        public void PickFurniture(int era)
        {
            if (_activeGO != null)
                _activeGO.SetActive(false);

            _activeGO = _GOPerEra[era];
            _activeGO.SetActive(true);

            if (!Picked)
                Picked = true;
        }

        public void UnPick()
        {
            _activeGO.SetActive(false);
            Picked = false;
        }

        public int GetIdInList(List<Furniture> list)
        {
            return list.IndexOf(this);
        }
    }

    [SerializeField] public List<Furniture> FurnitureList;
    public List<int> ActiveFurnitures;

    void Start()
    {
        if (instance == null)
            _instance = this;

        foreach (var furniture in GameManager.instance.Player.AllFurnituresSave)
        {
            
        }
        ActiveFurnitures = GameManager.instance.Player.ActivesFurnitures;
        //Player -> load data into FurnitureList and ActiveFurnitures
        
    }

    public void UnlockFurniture(string name)
    {
        foreach (var furniture in FurnitureList)
        {
            if (furniture.PrefabParent.name == name)
            {
                furniture.UnlockFunriture();
                GameManager.instance.Player.UnlockFurniture(furniture.GetIdInList(FurnitureList))
            }
        }
    }

    public void PickFurniture(string name)
    {
        foreach (var furniture in FurnitureList)
        {
            if (furniture.PrefabParent.name == name)
            {
                int tempIndex = 0;
                for(int i = 0; i < ActiveFurnitures.Count; i++)
                {
                    int index = ActiveFurnitures[i];
                    if (FurnitureList[index].Type == furniture.Type)
                    {
                        FurnitureList[index].UnPick();
                        tempIndex = index;
                        break;
                    }
                }
                ActiveFurnitures.Remove(tempIndex);
                GameManager.instance.Player.UnPickFurniture(ActiveFurnitures.IndexOf(tempIndex));
                furniture.PickFurniture(GameManager.instance.Era);
                ActiveFurnitures.Add(FurnitureList.IndexOf(furniture));
                GameManager.instance.Player.PickFurniture(FurnitureList.IndexOf(furniture));
                return;
            }
        }
    }

    public void SetEra(int era)
    {
        foreach (int index in ActiveFurnitures)
            FurnitureList[index].PickFurniture(era);
    }

    private void OnDestroy()
    {
        //Player -> SaveData
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public bool Unlock;
        public FurnitureType Type;

        public void UnlockFunriture()
        {
            Unlock = true;
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

        //Player
        
    }

    public void UnlockFurniture(string name)
    {
        foreach (var furniture in FurnitureList)
        {
            if (furniture.PrefabParent.name == name)
            {
                furniture.UnlockFunriture();
            }
        }
    }

    public void PickFurniture(string name)
    {
        foreach (var furniture in FurnitureList)
        {
            if (furniture.PrefabParent.name == name)
            {
                foreach (int index in ActiveFurnitures)
                {
                    if (FurnitureList[index].Type == furniture.Type)
                        FurnitureList[index].UnPick();
                }
                furniture.PickFurniture(GameManager.instance.Era);
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

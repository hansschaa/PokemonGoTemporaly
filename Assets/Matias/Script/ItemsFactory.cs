using System.Collections.Generic;
using UnityEngine;


public class ItemsFactory : MonoBehaviour
{
    private static ItemsFactory instance;

    public static ItemsFactory Instance
    {
        get { return instance; }
    }

    
    [SerializeField] private GameObject[] availableItems;
    [SerializeField] private Player player;
    [SerializeField] private float waitTime = 180.0f;
    [SerializeField] private int startingItems = 5;
    [SerializeField] private float minRange = 5.0f;
    [SerializeField] private float maxRange = 50.0f;
    public GameObject circlePotionFX;
    
    public List<GameObject> generatedItemList;
    private void Awake() {

        if (instance == null)
        {

            generatedItemList = new List<GameObject>(); 
            instance = this;
            if (!GameManager.instance.isPokemonsLoaded)
            {
                for (int i = 0; i < startingItems; i++)
                {
                    generatedItemList.Add(InstantiateItem());
                }
            }
        }

        
        
        else if (instance != this)
        {
            Destroy(gameObject);
            //LoadItems();
        }

        
        
        DontDestroyOnLoad(gameObject);
    }


    public void LoadItems()
    {
        for (int i = 0; i < generatedItemList.Count; i++)
        {
            print	("Crgando a :" + generatedItemList[i].name);
            Instantiate(generatedItemList[i], generatedItemList[i].transform.position, Quaternion.identity);
        }
    }
    
    private GameObject InstantiateItem() {
        int index = Random.Range(0, availableItems.Length);
        float x = player.transform.position.x + GenerateRange();
        float y = player.transform.position.y + 1;
        float z = player.transform.position.z + GenerateRange();
        var a = Instantiate(availableItems[index], new Vector3(x, y, z), Quaternion.identity);
        a.transform.localScale = Vector3.one * 3;
        a.transform.SetParent(transform);

        InstantiateGroundFX(a);

        return a;
    }

    private float GenerateRange() {
        float randomNum = Random.Range(minRange, maxRange);
        bool isPositive = Random.Range(0, 10) < 5;
        return randomNum * (isPositive ? 1 : -1);
    }

    private void InstantiateGroundFX(GameObject a)
    {
        Instantiate(circlePotionFX, a.transform.position + Vector3.up * 2, Quaternion.identity).transform.SetParent(a.transform);
    }
}

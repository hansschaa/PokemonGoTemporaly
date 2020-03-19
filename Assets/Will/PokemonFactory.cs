using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public enum PokemonType
{
    ABRA, PIKACHU, SNORLAX, SQUIRTLE, BUTTERFLY
}

public class PokemonFactory : MonoBehaviour
{
    private static PokemonFactory instance;

    public static PokemonFactory Instance 
    { 
        get { return instance; } 
    } 
    
    // 0 : abra, 1: pikachu , 2: snorlax, 3: squirtle , 4 Butterfly
    [SerializeField] private GameObject[] availablePokemon;
    [SerializeField] private Player player;
    [SerializeField] private float waitTime = 180.0f;
    [SerializeField] private int startingPokemon = 1;
    [SerializeField] private float minRange = 5.0f;
    [SerializeField] private float maxRange = 50.0f;
    public List<GameObject> generatedPokemonList;
    public GameObject circleFX;
   



    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            if (!GameManager.instance.isPokemonsLoaded)
            {
                GameManager.instance.isPokemonsLoaded = true;
                for (int i = 0; i < startingPokemon; i++)
                {
                    generatedPokemonList.Add(InstantiatePokemon());
                }
            }
        }

        
        
        else if (instance != this)
        {
            Destroy(gameObject);
            //LoadPokemons();
        }

        
        
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        //PrintDistanceToPokemons();
    }

    private void PrintDistanceToPokemons()
    {
        var player = GameManager.instance.playerTransform;

        for(int i = 0 ; i < generatedPokemonList.Count; i++)
        {
            var pokemon = generatedPokemonList[i];
            print("Distancia a:" + pokemon.name + ", es:" + Vector2.Distance(new Vector2(pokemon.transform.position.x, pokemon.transform.position.z), new Vector2(player.position.x,player.position.z)));
        }
    }




    #region Pokemon Generator

    private GameObject InstantiatePokemon() 
    {
        
        int index = Random.Range(0, availablePokemon.Length);
        float x = player.transform.position.x + GenerateRange();
        float y = player.transform.position.y;
        float z = player.transform.position.z + GenerateRange();
        var a = Instantiate(availablePokemon[index], new Vector3(x, y, z), Quaternion.identity);
        
        a.transform.localScale = Vector3.one * 3;
        a.transform.SetParent	(this.gameObject.transform);
        InstantiateGroundFX(a);
        return a;

    }

    private float GenerateRange() {
        float randomNum = Random.Range(minRange, maxRange);
        bool isPositive = Random.Range(0, 10) < 5;
        return randomNum * (isPositive ? 1 : -1);
    }

    private void LoadPokemons()
    {
        print	("Cargar Pokemoins");
        for (int i = 0; i < generatedPokemonList.Count; i++)
        {
            print	("Crgando a :" + generatedPokemonList[i].name);
            var a = Instantiate(generatedPokemonList[i], generatedPokemonList[i].transform.position, Quaternion.identity);
            InstantiateGroundFX(a);
        }
    }

    private void InstantiateGroundFX(GameObject a)
    {
        Instantiate(circleFX, a.transform.position + Vector3.up * 2, Quaternion.identity).transform.SetParent(gameObject.transform);
    }

    #endregion
}

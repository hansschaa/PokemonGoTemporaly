using System.Collections;
using System.Collections.Generic;
using Prime31.TransitionKit;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    MAP, BATTLE, OPTIONS
}

public class GameManager : ExtendedBehaviour
{
    public static GameManager
        instance = null; //Static instance of GameManager which allows it to be accessed by any other script.

    //private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
    private int level = 3; //Current level number, expressed in game as "Day 1".
    [HideInInspector] public BackPack backPack;
    public Transform playerTransform;
    public GameObject enemy;
    public GameObject pokeBall;
    public int groundId;
    public SceneType sceneType;
    public int genreSelected;
    [Header("States")] 
    public bool isPokemonsLoaded;

    [Header("Music")] 
    public AudioClip battleMusic;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }

        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }



        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Get a component reference to the attached BoardManager script
        //boardScript = GetComponent<BoardManager>();

        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    //Initializes the game for each level.
    void InitGame()
    {
        backPack = GetComponent<BackPack>();
        sceneType = SceneType.MAP;
    }

    public bool HaveAvalaiblePokemon()
    {
        var currentPokemonList = backPack.pokemons;

        for (int i = 0; i < currentPokemonList.Count; i++)
        {
            if (currentPokemonList[i].GetComponent<Pokemon>().currentHp > 0)
            {
                var aux = currentPokemonList[0];
                var pokeToCharge = currentPokemonList[i];

                currentPokemonList[0] = pokeToCharge;
                currentPokemonList[i] = aux;

                return true;
            }

        }

        return false;
    }

    public void ShowHelpText()
    {

    }

    public void ShowBattleTransition()
    {
        AudioManager.instance.PlayMusic(battleMusic);
        var enumValues = System.Enum.GetValues( typeof( PixelateTransition.PixelateFinalScaleEffect ) );
        var randomScaleEffect = (PixelateTransition.PixelateFinalScaleEffect)enumValues.GetValue( Random.Range( 0, enumValues.Length ) );

        var pixelater = new PixelateTransition()
        {
            //nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1,
            finalScaleEffect = randomScaleEffect,
            duration = 2.0f
        };
        TransitionKit.instance.transitionWithDelegate( pixelater );
         
        Wait(2f, () =>
        {
            enemy	.transform.SetParent	(null);
            PokemonFactory.Instance.gameObject.SetActive	(false);
            SceneManager.LoadScene(3);
        });
    }

}

using UnityEngine;
using UnityEngine.UI;
public enum MyPokemonType
{
    Normal, Fighting,Flying,Poison,Ground,Rock,Bug,Ghost,Steel,Fire,Water,Grass,Electric,Psychic,Ice,Dragon,Dark,Fairy
}

public class Pokemon : MonoBehaviour
{
    
    public string pokemonName;
    public int maxHp;
    public int currentHp;
    public Sprite pokemonIcon;
    [HideInInspector] public int myExperience;
    public int experienceToGive;
    public int lv;
    public MyPokemonType pokemonType;
    [Header("Pool Attack")] public Attack[] attacks;

    public void OnMouseDown()
    {
        if (GameManager.instance.sceneType == SceneType.MAP)
        {
            var player = GameManager.instance.playerTransform;
            //Ver si estoy cerca del pokemon
            if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(player.position.x,player.position.z)) <= 10)
            {
                if (GameManager.instance.HaveAvalaiblePokemon())
                {
                    print("Has encontrado a" + gameObject.name);
                    GameManager.instance.sceneType = SceneType.BATTLE;

                    var newEnemy = gameObject;
                    GameManager.instance.enemy = newEnemy;
                    DontDestroyOnLoad(newEnemy);
                    GameManager.instance.groundId =transform.GetSiblingIndex();
                    //Destroy the spriteFX
                    //Destroy(transform.parent.GetChild(transform.GetSiblingIndex()+1).gameObject);

                    //SceneManager.LoadScene("BattleScene");
                    
                    GameManager.instance.ShowBattleTransition();
                }

                else
                {
                    HUDManager.instance.ShowFeedBackPane("Don't have live Pokemons");
                }

            }
            
        }
    } 
}

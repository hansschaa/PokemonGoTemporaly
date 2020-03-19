using UnityEngine;


public class BattleController : MonoBehaviour
{
    public Transform enemyPos;
    public Transform playerPos;
    
    public enum BattleStates{
        OPTIONSSTATE, BAGSTATE, SWITCHPKMSTATE, ENEMYATTACKSTATE , PLAYERATTACKSTATE, WINSTATE, GAMEOVERSTATE
    }

    public BattleStates battleState;

    public void SetupBattle(UIManager uiManager)
    {
        //Setup Pokemon Position and scale/////////////////////////////////
        
        //Enemy
        var enemy = GameManager.instance.enemy;
        
        enemy.transform.position = Vector3.zero;
        enemy.transform.localScale = Vector3.one;
        enemy.transform.rotation = new Quaternion(0,80,0, 1);
        
        enemy.transform.SetParent(enemyPos);

        //Player
        Instantiate(GameManager.instance.backPack.pokemons[0], playerPos.position, Quaternion.identity).transform.SetParent(playerPos.transform);
        
        //////////////////////////////////////////////////////////////////
        battleState = BattleController.BattleStates.OPTIONSSTATE;
        
        uiManager.currentPokemon = GameManager.instance.backPack.pokemons[0].GetComponent<Pokemon>();
        uiManager.enemyPokemon = GameManager.instance.enemy.GetComponent<Pokemon>();
        
        
        uiManager.playerNamePokemonText.text = GameManager.instance.backPack.pokemons[0].GetComponent<Pokemon>().pokemonName;
        uiManager.playerLvPokemonText.text = "Lv " +GameManager.instance.backPack.pokemons[0].GetComponent<Pokemon>().lv;
        
        uiManager.enemyNamePokemonText.text = GameManager.instance.enemy.GetComponent<Pokemon>().pokemonName;
        uiManager.enemyLvPokemonText.text ="Lv " + GameManager.instance.enemy.GetComponent<Pokemon>().lv;

        //Correct Lifes
        uiManager.foregroundPlayerLifeImage.fillAmount = uiManager.currentPokemon.currentHp;
        uiManager.foregroundEnemyLifeImage.fillAmount = uiManager.enemyPokemon.currentHp;

        uiManager.playerLifeText.text = uiManager.currentPokemon.currentHp + " / " + uiManager.currentPokemon.maxHp;
        uiManager.enemyLifeText.text =  uiManager.enemyPokemon.currentHp + " / " + uiManager.enemyPokemon.maxHp;
        
        

        //
        //GameObject pkmn = GameObject.Find("Arcanine").GetComponent<GameObject>();
       // Vector3 pos = GameObject.Find("debgPlanePrefab").transform.position;
       // pkmn.transform.position = pos;
    }
}

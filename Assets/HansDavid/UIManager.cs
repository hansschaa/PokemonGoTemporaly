using System.Linq;
using DG.Tweening;
using Prime31.TransitionKit;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : ExtendedBehaviour
{
    [Header("Important Variables!!!")] 
    public BattleController battleController;

    public Transform pokeballSpawn;
    
    [Header("Button Panel")] 
    public GameObject actionButtonsPanel;
    public GameObject attackButtonsPanel;
    public GameObject objectsButtonsPanel;
    public GameObject attackButton;

    [Header("Catch Pokemon View")] 
    public GameObject catchPanel;
    public TextMeshProUGUI catchPokemonText;
    public GameObject catchBackButton;
    
    [Header("Win View")] 
    public GameObject winView;
    public TextMeshProUGUI pokemonNameGainExp;
    public TextMeshProUGUI expValueText;
    public TextMeshProUGUI levelUpText;
    public GameObject backButton;
    private Sequence _winSequence;
    private Sequence _catchPokemonSequence;
    
    [Header("Current Pokemon Data")] 
    public TextMeshProUGUI playerNamePokemonText;
    public TextMeshProUGUI playerLvPokemonText;
    public Image foregroundPlayerLifeImage;
    public TextMeshProUGUI playerLifeText;
    
    [Header("Enemy Pokemon Data")] 
    public TextMeshProUGUI enemyNamePokemonText;
    public TextMeshProUGUI enemyLvPokemonText;
    public Image foregroundEnemyLifeImage;
    public TextMeshProUGUI enemyLifeText;
    
    [HideInInspector] public Pokemon currentPokemon;
    [HideInInspector] public Pokemon enemyPokemon;
    [HideInInspector] public int enemyCountAttack;
    
    
    [Header("Sounds")] 
    public AudioClip victorySound;
    public AudioClip catchSuccesfullSound;
    public void Start()
    {
        battleController.SetupBattle(this);
        UpdateBarLife(foregroundPlayerLifeImage, playerLifeText,currentPokemon);
       
    }

    public void AttackButton(bool show)
    {
        if (show)
        {
            battleController.battleState = BattleController.BattleStates.PLAYERATTACKSTATE;
            actionButtonsPanel.SetActive(false);

            var attacksPanel = attackButtonsPanel.transform.GetChild(0);

            foreach (Transform buttonAttack in attacksPanel.transform)
                Destroy(buttonAttack.gameObject);

            foreach (var attack in currentPokemon.attacks)
            {
                var a = Instantiate(attackButton, Vector3.zero, Quaternion.identity, attacksPanel.transform);
                a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = attack.attackName;
                a.GetComponent<AttackButtonController>().attack = attack;
                a.GetComponent<AttackButtonController>().uiManager = this;
            }

            attackButtonsPanel.SetActive(true);

        }
        else
        {
            actionButtonsPanel.SetActive(true);
            attackButtonsPanel.SetActive(false);
        }
    }

    public void ShowWinScreen()
    {
        //Setup win screen
        pokemonNameGainExp.text = currentPokemon.name + " got";
        expValueText.text = enemyPokemon.experienceToGive.ToString() + " exp.";

        var haveNextLevel = false;
        if (currentPokemon.myExperience >= 2)
        {
            haveNextLevel = true;
            currentPokemon.lv++;
            currentPokemon.myExperience = 0;
            
        }

        _winSequence = DOTween.Sequence();

        if (haveNextLevel)
        {
            _winSequence.Append(winView.transform.DOLocalMoveX(0, .5f))
                .Append(pokemonNameGainExp.transform.DOScale(Vector2.one,.5f).SetEase(Ease.Flash))
                .Append(expValueText.transform.DOScale(Vector2.one,.5f).SetEase(Ease.Flash))
                .Append(levelUpText.transform.DOScale(Vector2.one,.5f).SetEase(Ease.Flash));
        }

        else
        {
            _winSequence.Append(winView.transform.DOLocalMoveX(0, 1f))
                .Append(pokemonNameGainExp.transform.DOScale(Vector2.one,1).SetEase(Ease.Flash))
                .Append(expValueText.transform.DOScale(Vector2.one,1).SetEase(Ease.Flash));
        }



        _winSequence.Play().OnComplete(()=> backButton.SetActive(true));

    }

    public void BackpackButton()
    {
        battleController.battleState = BattleController.BattleStates.BAGSTATE;
    }

    public void SwitchPokemon()
    {
        battleController.battleState = BattleController.BattleStates.SWITCHPKMSTATE;
    }
    
    public void ShowPlayerOptions()
    {
        attackButtonsPanel.SetActive(false);
        objectsButtonsPanel.SetActive(false);
        actionButtonsPanel.SetActive(true);
    }

    public void DoDamage(Attack attack, Entitie target)
    {
        if (target == Entitie.ENEMY)
        {
            HideShowAllPanelOptions(false);
            //GameManager.instance.backPack.pokemons[0].transform.DOShakePosition(1);
            //currentPokemon.gameObject.transform.DOShakePosition(1);
            Wait(1f, () =>
            {
                //Restar vida al enemigo
                if (enemyPokemon.currentHp - attack.value <= 0)
                {
                    AudioManager.instance.PlayMusic(victorySound);
                    enemyPokemon.transform.DOShakeRotation(1);
                    enemyPokemon.transform.DOShakeScale(1);
                    enemyPokemon.currentHp = 0;
                    UpdateBarLife(foregroundEnemyLifeImage, enemyLifeText,enemyPokemon);
            
                    //Lo mataste
                    battleController.battleState = BattleController.BattleStates.WINSTATE;
            
                    currentPokemon.myExperience += enemyPokemon.experienceToGive;

                    var pokemonFactory = PokemonFactory.Instance;
                    Destroy(PokemonFactory.Instance.transform.GetChild(GameManager.instance.groundId).gameObject);


                    Wait(1f, ShowWinScreen);
                    return;
                }

            
                
                enemyPokemon.currentHp -= attack.value;
            
                UpdateBarLife(foregroundEnemyLifeImage, enemyLifeText,enemyPokemon);
                battleController.battleState = BattleController.BattleStates.ENEMYATTACKSTATE;
                Wait(1F, EnemyAction);
            });
        }

        else
        {
            //Restar vida al player
            enemyPokemon.transform.DOShakePosition(1);
            Wait(1f, () =>
            {
                
                if (currentPokemon.currentHp - attack.value < 0)
                {
                    currentPokemon.currentHp = 0;
                
                    //SwitchPokemon si es que le quedan pokemons
                    battleController.battleState = BattleController.BattleStates.SWITCHPKMSTATE;
                    SceneManager.LoadScene(2);
                }
            
                else
                    currentPokemon.currentHp -= attack.value;
            
                UpdateBarLife(foregroundPlayerLifeImage, playerLifeText,currentPokemon);
                
            });
        }
    }

    private void EnemyAction()
    {

        Attack attack= enemyCountAttack != 3 ?	enemyPokemon.attacks[0] : enemyPokemon.attacks[1];

        if (enemyCountAttack == 3)
            enemyCountAttack = 0;

        switch (attack.typeAttack)
        {
            case TypeAttack.DAMAGE:
                DoDamage(attack, Entitie.PLAYER);
                break;
            case TypeAttack.HEAL:
                break;
            case TypeAttack.ATTACKUP:
                break;
            case TypeAttack.DEFENSEUP:
                break;
            case TypeAttack.VELOCITYUP:
                break;
        }
        
        battleController.battleState = BattleController.BattleStates.OPTIONSSTATE;
        ShowPlayerOptions();
    }


    public void UpdateBarLife(Image lifeBarImage, TextMeshProUGUI lifeText, Pokemon pokemon)
    {
        float life =  (float)pokemon.currentHp / pokemon.maxHp ;
        lifeBarImage.fillAmount = life;
        
        UpdateTextLife(pokemon, lifeText);
    }

    public void UpdateTextLife(Pokemon pokemon, TextMeshProUGUI text)
    {
        text.text = pokemon.currentHp + " / " + pokemon.maxHp;
    }

    public void UsePotion(int potionHealthValue)
    {
        //Value of pokemon life percent
        int newValue = 0;
        
        switch (potionHealthValue)
        {
            case 25:
                GameManager.instance.backPack.potion25--;
                newValue = (int) (currentPokemon.maxHp * 0.25f);
                break;
            
            case 50: 
                GameManager.instance.backPack.potion50--;
                newValue = (int) (currentPokemon.maxHp * 0.5f);
                break;
            
            case 100:  
                GameManager.instance.backPack.potion100--;
                newValue = currentPokemon.maxHp;
                break;
        }

        if (currentPokemon.currentHp + newValue >= currentPokemon.maxHp)
            currentPokemon.currentHp = currentPokemon.maxHp;
        

        else
            currentPokemon.currentHp += newValue;

        UpdateBarLife(foregroundPlayerLifeImage, playerLifeText,currentPokemon);
        battleController.battleState = BattleController.BattleStates.ENEMYATTACKSTATE;
        
        HideShowAllPanelOptions(false);
        Wait(1F, EnemyAction);
    }

    public void UsePokeball()
    {
        GameManager.instance.backPack.pokeballs--;
        
        HideShowAllPanelOptions(false);
        
        var a = Instantiate(GameManager.instance.pokeBall, pokeballSpawn.position, Quaternion.identity);
        a.transform.DOLocalMove(enemyPokemon.transform.position, .7f).SetEase(Ease.Flash).OnComplete(()=>SavePokemon(a));
    }

    private void SavePokemon(GameObject pokeBall)
    {
        
        enemyPokemon.transform.GetChild(0).gameObject.SetActive(false);

        pokeBall.transform.DOJump(Vector3.back* 0.001f, .5f, 5, 2);
        
        Wait(2f, () =>
        {
            //Lo atrapas
            if (enemyPokemon.currentHp <= enemyPokemon.maxHp/2)
            {
                //Setup new pokemon
                var a = Instantiate(Resources.Load<GameObject>(enemyPokemon.pokemonName));
                a.SetActive(false);
                a.GetComponent<Pokemon>().currentHp = enemyPokemon.currentHp;
                DontDestroyOnLoad(a);
                GameManager.instance.backPack.pokemons.Add(a);
                
                catchPokemonText.text = enemyPokemon.pokemonName + " Catch!!!";
                var pokemonFactory = PokemonFactory.Instance;
                Destroy(PokemonFactory.Instance.transform.GetChild(GameManager.instance.groundId).gameObject);
                
                _catchPokemonSequence = DOTween.Sequence();
                Wait(2f, () =>
                {
                    _catchPokemonSequence.Append(catchPanel.transform.DOLocalMoveX(0, .5f)).SetEase(Ease.InOutSine)
                        .Append(catchPokemonText.transform.DOScale(Vector2.one,.5f).SetEase(Ease.Flash))
                        .Append(catchBackButton.transform.DOScale(Vector2.one,.5f).SetEase(Ease.Flash)).OnComplete(() =>
                        {
                            a.SetActive(true);
                        });

                    _catchPokemonSequence.Play();
                });
                
                AudioManager.instance.PlayMusic(catchSuccesfullSound);
            }
            
            //No lo atrapas
            else
            {
                enemyPokemon.transform.GetChild(0).gameObject.SetActive(true);
                Destroy(pokeBall);
                HideShowAllPanelOptions(true);
                EnemyAction	();
            }
        });
        
    }

    public void ShowObjectsPanel(bool show)
    {
        //Disable Buttons that not have item
        if (show)
        {
            Transform buttonsTransform = objectsButtonsPanel.transform.GetChild(0).transform;
            if (GameManager.instance.backPack.potion25 == 0)
                buttonsTransform.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = false;

            if (GameManager.instance.backPack.potion50 == 0)
                buttonsTransform.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = false;

            if (GameManager.instance.backPack.potion100 == 0)
                buttonsTransform.transform.GetChild(2).gameObject.GetComponent<Button>().interactable = false;

            if (GameManager.instance.backPack.pokeballs == 0 || GameManager.instance.backPack.pokemons.Count == 10)
                buttonsTransform.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = false;
        }

        actionButtonsPanel.SetActive(!show);
        objectsButtonsPanel.SetActive(show);
    }

    public void ToScene(string sceneUrl)
    {
        PokemonFactory.Instance.generatedPokemonList.Remove(GameManager.instance.enemy);
        GameManager.instance.sceneType = SceneType.MAP;
        
        
        var wind = new WindTransition()
        {
            duration = 1.0f,
            size = 0.3f
        };
        TransitionKit.instance.transitionWithDelegate( wind );
        
        Wait	(1f, () =>
        {
            SceneManager.LoadScene("PokeTalcaGoScene");
        });
    }

    


    public void HideShowAllPanelOptions(bool active)
    {
        actionButtonsPanel.SetActive(active);
        attackButtonsPanel.SetActive(active);
        objectsButtonsPanel.SetActive(active);
    }

    public void OnExitButtonDown()
    {
        GameManager.instance.sceneType = SceneType.MAP;
        var wind = new WindTransition()
        {
            duration = 1.0f,
            size = 0.3f
        };
        TransitionKit.instance.transitionWithDelegate( wind );
        
        Wait(1f, () =>
        {
            GameManager.instance.enemy.transform.localScale = Vector3.one * 3;
            GameManager.instance.enemy.transform.SetParent(PokemonFactory.Instance.transform);
            
            Destroy( GameManager.instance.enemy);
            GameManager.instance.enemy = null;

             
            SceneManager.LoadScene("PokeTalcaGoScene");
        });
    }


}

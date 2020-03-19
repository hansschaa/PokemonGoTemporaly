using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    public Camera uiCamera;

    [Header("Panels")]
    public GameObject itemList;
    public GameObject pokemonList;
    public Transform pokemonMenuContent;
    public Transform feedbackPane;
    public Vector3 leftScreen { get; private set; }
    public Vector3 rigthScreen { get; private set; }
    public Vector3 bottomScreen { get; private set; }
    public AudioClip tapSoundFX ;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        print("Screen Height: " + Screen.height);
        leftScreen = uiCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 10));
        rigthScreen = uiCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
        bottomScreen = uiCamera.ScreenToWorldPoint(new Vector3(0, 0, 10));
        SetupPanelPositions();
    }

    public void SetupPanelPositions()
    {
        itemList.transform.position = leftScreen;
        itemList.SetActive(true);

        pokemonList.transform.position = rigthScreen;
        pokemonList.SetActive(true);

        feedbackPane.position = bottomScreen;
        feedbackPane.gameObject.SetActive(true);


    }

    #region Display
    public void ShowItemPanel()
    {
        AudioManager.instance.PlaySingle(tapSoundFX);
        SetupItemPanel();
        itemList.transform.DOMoveX(rigthScreen.x, 1);
    }

    public void HideItemPanel()
    {
        GameManager.instance.sceneType = SceneType.MAP;
        itemList.transform.DOMoveX(leftScreen.x, 1);
    }

    public void SetupItemPanel()
    {
        GameManager.instance.sceneType = SceneType.OPTIONS;

        var itemListGameObject = itemList.transform.GetChild(0);
        var potion25CountText = itemListGameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        var potion50CountText = itemListGameObject.transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        var potion100CountText = itemListGameObject.transform.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        var pokeballCountText = itemListGameObject.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        potion25CountText.text = "x" + GameManager.instance.backPack.potion25.ToString();
        potion50CountText.text = "x" +GameManager.instance.backPack.potion50.ToString();
        potion100CountText.text = "x" +GameManager.instance.backPack.potion100.ToString();
        pokeballCountText.text = "x" +GameManager.instance.backPack.pokeballs.ToString();
    }

    public void ShowPokemonPanel()
    {
        AudioManager.instance.PlaySingle(tapSoundFX);
        SetupPokemonPanel();
        pokemonList.transform.DOMoveX(leftScreen.x, 1);
    }

    public void HidePokemonPanel()
    {
        AudioManager.instance.PlaySingle(tapSoundFX);
        GameManager.instance.sceneType = SceneType.MAP;
        pokemonList.transform.DOMoveX(rigthScreen.x, 1);
    }

    public void ShowFeedBackPane(string feedbackTxt)
    {
        CancelInvoke();
        var feedBackTxtRef = feedbackPane.GetChild(0).GetComponent<TextMeshProUGUI>();
        feedBackTxtRef.text = feedbackTxt;
        Invoke("HideFeedBackPane",5f);

        feedbackPane.transform.DOMoveY(bottomScreen.y + 1, .5f).SetEase(Ease.InOutExpo);
    }

    

    public void HideFeedBackPane()
    {
        CancelInvoke();
        feedbackPane.transform.DOMoveY(bottomScreen.y, 1);
    }

    public void SetupPokemonPanel()
    {
        GameManager.instance.sceneType = SceneType.OPTIONS;

        var pokemonListRef = GameManager.instance.backPack.pokemons;

        for(int i = 0 ; i < pokemonListRef.Count ; i++)
        {
            var pokemonData = pokemonListRef[i].GetComponent<Pokemon>();

            var pokemonImage = pokemonMenuContent.GetChild(i);
            var pokemonName = pokemonImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            var pokemonHealth = pokemonImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            var pokemonLevel = pokemonImage.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            var pokemonType = pokemonImage.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            
            //HACK : Cambiar imagen del pokemon
            pokemonImage.GetComponent<Image>().sprite = pokemonData.pokemonIcon;
            pokemonName.text = pokemonData.pokemonName;
            pokemonHealth.text = "HP: " + pokemonData.currentHp + " / " + pokemonData.maxHp;
            pokemonLevel.text = "Lv: " + pokemonData.lv;
            pokemonType.text = pokemonData.pokemonType.ToString();

            print(pokemonListRef[i].name);
        }

    }
    #endregion
}

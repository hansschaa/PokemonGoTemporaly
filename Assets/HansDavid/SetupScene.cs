using System.Collections;
using System.Collections.Generic;
using Prime31.TransitionKit;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetupScene : MonoBehaviour
{
    public AudioClip levelMusic;
    public bool isMapScene;
    public void Awake()
    {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMusic(levelMusic);
        if (isMapScene)
        {
            PokemonFactory.Instance.gameObject.SetActive(true);
            PokemonFactory.Instance.gameObject.GetComponent<Mapbox.Examples.Scripts.Utilities.DragRotate>().playerTransform = GameManager.instance.playerTransform;
        }

    }
}

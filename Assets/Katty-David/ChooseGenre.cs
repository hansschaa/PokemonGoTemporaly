using System.Collections;
using System.Collections.Generic;
using Prime31.TransitionKit;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseGenre : MonoBehaviour
{
    public int Genre ;
    public static int gen;
    public void Start()
    {
        
    }

    public void OnClickMale()
    {
        Genre = 0;
        gen = 0;
        var wind = new WindTransition()
        {
            nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 1 : 1,
            duration = 1.0f,
            size = 0.3f
        };
        TransitionKit.instance.transitionWithDelegate( wind );
        //SceneManager.LoadScene("PokeTalcaGoScene");
    }

    public void OnClickFemale()
    {
        Genre = 1;
        gen = 1;
        var wind = new WindTransition()
        {
            nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 1 : 1,
            duration = 1.0f,
            size = 0.3f
        };
        TransitionKit.instance.transitionWithDelegate( wind );
        
        //SceneManager.LoadScene("PokeTalcaGoScene");
    }

    
}

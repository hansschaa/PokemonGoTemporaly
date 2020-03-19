
using System;
using Prime31.TransitionKit;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCharacterClick : MonoBehaviour
{
    public AudioClip tapSoundFX;
    public void OnMouseDown()
    {
         AudioManager.instance.PlaySingle(tapSoundFX);
        GameManager.instance.genreSelected = Int32.Parse(name);
        var wind = new WindTransition()
        {
            nextScene = SceneManager.GetActiveScene().buildIndex == 2 ? 2 : 2,
            duration = 1.0f,
            size = 0.3f
        };
        TransitionKit.instance.transitionWithDelegate( wind );
        //SceneManager.LoadScene("PokeTalcaGoScene");
    }
}

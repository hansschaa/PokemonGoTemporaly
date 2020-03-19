using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using Prime31.TransitionKit;

public class TouchListener : MonoBehaviour
{
    public AudioClip tapSoundFX;
    public bool isPressed;

    public void Start()
    {
        isPressed = false;
    }
    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && !isPressed)
        {
            isPressed = true;
            AudioManager.instance.PlaySingle(tapSoundFX);

            var wind = new WindTransition()
            {
                nextScene = SceneManager.GetActiveScene().buildIndex == 5 ? 5 : 5,
                duration = 1.0f,
                size = 0.3f
            };
            TransitionKit.instance.transitionWithDelegate( wind );
        }

    }
}

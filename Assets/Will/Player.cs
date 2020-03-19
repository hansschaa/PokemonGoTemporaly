using UnityEngine;


public class Player : MonoBehaviour
{
    public int genreSelected;

    void Awake() {
        genreSelected = GameManager.instance.genreSelected;
        if(genreSelected == 1)
        {
            GameObject maleA = GameObject.Find("LocationBasedGame").transform.GetChild(1).GetChild(0).GetChild(1).gameObject;
            GameManager.instance.playerTransform = GameObject.Find("LocationBasedGame").transform.GetChild(1).GetChild(0).GetChild(2).gameObject.transform;
            Destroy(maleA);
        }
        else if (genreSelected == 0)
        {
            GameObject femaleA = GameObject.Find("LocationBasedGame").transform.GetChild(1).GetChild(0).GetChild(2).gameObject;
            GameManager.instance.playerTransform = GameObject.Find("LocationBasedGame").transform.GetChild(1).GetChild(0).GetChild(1).gameObject.transform;
            Destroy(femaleA);
        }

    }
}

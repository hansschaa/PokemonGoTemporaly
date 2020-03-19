using UnityEngine;

public enum PotionType
{
    SMALL, MEDIUM, HUGE
}

public class PotionHealth : MonoBehaviour
{
    public PotionType potionType;
    
    public void OnMouseDown()
    {
        if (GameManager.instance.sceneType == SceneType.MAP)
        {
            var player = GameManager.instance.playerTransform;
            if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(player.position.x,player.position.z)) <= 10)
            {
                switch (potionType)
                {
                    case PotionType.SMALL:
                        GameManager.instance.backPack.potion25++;
                        HUDManager.instance.ShowFeedBackPane("Found a Small Potion!");
                        break;
                    case PotionType.MEDIUM:
                        GameManager.instance.backPack.potion50++;
                        HUDManager.instance.ShowFeedBackPane("Found a Medium Potion!");
                        break;
                    case PotionType.HUGE:
                        HUDManager.instance.ShowFeedBackPane("Found a Huge Potion!");
                        GameManager.instance.backPack.potion100++;
                        break;
                }

                ItemsFactory.Instance.generatedItemList.Remove(gameObject);
                Destroy	(gameObject);
            }

        }
    } 
}


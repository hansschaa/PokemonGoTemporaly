using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonCenter : MonoBehaviour
{
    public AudioClip pokemonCenterFX;
    public void OnMouseDown()
    {
        if (GameManager.instance.sceneType == SceneType.MAP)
        {
            var player = GameManager.instance.playerTransform;
            //Ver si estoy cerca del pokemon
            if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(player.position.x,player.position.z)) <= 10)
            {
                AudioManager.instance.PlaySingle(pokemonCenterFX);
                HUDManager.instance.ShowFeedBackPane("Pokemones curados");

                var pokemons = GameManager.instance.backPack.pokemons;
                for(int i = 0 ; i < pokemons.Count ; i++)
                {
                    pokemons[i].GetComponent<Pokemon>().currentHp = pokemons[i].GetComponent<Pokemon>().maxHp;
                }
            }
            
        }
    } 
}

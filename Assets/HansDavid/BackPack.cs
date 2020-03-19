using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : MonoBehaviour
{
    public List<GameObject> pokemons;
    public int pokeballs ;
    public int potion25;
    public int potion50;
    public int potion100;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}

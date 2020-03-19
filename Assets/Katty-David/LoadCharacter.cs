using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCharacter : MonoBehaviour
{
    public GameObject button;
    public GameObject Avatar;

    void Start()
    {
        Avatar = GetComponent<GameObject>();
        int genre = button.GetComponent<ChooseGenre>().Genre;
        if(genre == 0)
        {
           //GameObject currentAvatar = Instantiate(Resources.Load("Walking_male")); 
          // Avatar.AddComponent<GameObject>(); 
          // = currentAvatar;
        }else
        {
            
        }

    }

    
    void Update()
    {
        
    }
}

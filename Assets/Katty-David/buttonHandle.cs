using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonHandle : MonoBehaviour
{
   //void OnMouseUp()
   //{
     //SceneManager.LoadScene("ChooseAvatar");
   //}

   public void GotoMainScene()
    {
        SceneManager.LoadScene("MenúScene");
    }

    public void GotoMenuScene()
    {
        SceneManager.LoadScene("ChooseAvatar");
    }
}

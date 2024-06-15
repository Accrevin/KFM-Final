using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    public GameObject menu;
    
    public void OpenUI()
    {
        menu.SetActive(!menu.activeSelf);
    }
}

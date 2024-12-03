using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;    

    public GameObject popUpWindow;

    public TMP_Text objectiveText;

    public TMP_Text nameText;

    public TMP_Text bioText;

    public TMP_Text statText;

    public Image portraitImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePopUpWindow()
    {
        popUpWindow.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructSites : MonoBehaviour
{
    Transform card;
    Camera cam;
    public LayerMask sitesLayer;
    public GameObject gamePanel;

    private void Start() {
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        
# if UNITY_STANDALONE || UNITY_EDITOR

        if(Input.GetMouseButtonDown(0)){
            OpenBuildPanel(Input.mousePosition);
        }

# endif


    }

    void OpenBuildPanel(Vector2 touchPos){
        Vector2 worldPos = cam.ScreenToWorldPoint(touchPos);
        if(Physics2D.OverlapPoint(worldPos,sitesLayer)){
           // instantiate the game panel object
        }

    }
}

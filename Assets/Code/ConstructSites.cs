using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: update gameobject pane
//      being able to retract build icon after use
//      animations transitions



public class ConstructSites : MonoBehaviour
{
    Transform card;
    Camera cam;
    public LayerMask  UILayer;
    public GameObject gamePanel,chickenHouse;

    private void Start() {
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        
# if UNITY_STANDALONE || UNITY_EDITOR

        if(Input.GetMouseButtonDown(0)){
            // GameObject buildPanel = GameObject.FindGameObjectWithTag("build_panel");
            BuildPanel(Input.mousePosition);
        }

# endif


    }

    void BuildPanel(Vector2 touchPos){
        Vector2 worldPos = cam.ScreenToWorldPoint(touchPos);
        if (Physics2D.OverlapPoint(worldPos, UILayer))
        {            
            card = Physics2D.OverlapPoint(worldPos, UILayer).transform;
            if(card.gameObject.CompareTag("construct_sites")){
                if (GameObject.FindWithTag("build_panel") != null){
                    Destroy(GameObject.FindWithTag("build_panel"));
                } else {
                    Instantiate(gamePanel,card.position,Quaternion.identity);
                }
                
            }
            else if(card.gameObject.CompareTag("chickenIcon")){
                Instantiate(chickenHouse,card.position,Quaternion.identity);
                Destroy(card.gameObject);
            }
            
        } 
        
    }
}

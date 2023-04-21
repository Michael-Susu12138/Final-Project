using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructSites : MonoBehaviour
{
    Transform card;
    Camera cam;
    public LayerMask  UILayer;
    public GameObject gamePanel;

    private void Start() {
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        
# if UNITY_STANDALONE || UNITY_EDITOR

        if(Input.GetMouseButtonDown(0)){
            GameObject buildPanel = GameObject.FindGameObjectWithTag("build_panel");
            BuildPanel(Input.mousePosition,buildPanel);
        }

# endif


    }

    void BuildPanel(Vector2 touchPos, GameObject buildPanel){
        Vector2 worldPos = cam.ScreenToWorldPoint(touchPos);
        if (Physics2D.OverlapPoint(worldPos, UILayer))
        {
            print("HIT");
            
            card = Physics2D.OverlapPoint(worldPos, UILayer).transform;
            // if(card.gameObject.CompareTag)
            //  using compare tag to check, put everything onto the UILayer
            Instantiate(gamePanel,card.position,Quaternion.identity);
        } 
        // else if (buildPanel!=null){
        //     print("not null");
        //     print(buildPanelLayer);
        //     print(Physics2D.OverlapPoint(worldPos,buildPanelLayer));
        // } 
        else if (buildPanel!=null && Physics2D.OverlapPoint(worldPos,buildPanelLayer)){
            print("Build a house");
        } 
        else {
            if(buildPanel != null){
                Destroy(buildPanel);
            }
        }
        
         
        

    }
}

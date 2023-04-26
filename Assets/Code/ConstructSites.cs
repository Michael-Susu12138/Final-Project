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
    public GameObject gamePanel,chickenHouselvl1, chickenHouselvl2,done_gamePanel,construct_site_prefab;
    GameObject construct_site,buildPanel,Construction;
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
                
                // create build_panel, if it exists, turn off the build panel
                if (GameObject.FindWithTag("build_panel") != null){
                    Destroy(GameObject.FindWithTag("build_panel"));
                } else { 
                    
                    buildPanel = Instantiate(gamePanel,card.position,Quaternion.identity);
                    Debug.Log(buildPanel.transform.position);

                }
                construct_site = card.gameObject;    
            } else {
                if ( buildPanel != null){
                    Destroy(buildPanel);
                }
            }
            
            // chicken constructions
            if(card.gameObject.CompareTag("chickenIcon")){
                Transform build_trans = GameObject.FindWithTag("build_panel").transform;
                Instantiate(chickenHouselvl1,build_trans.position,Quaternion.identity);
                // after instantiation of the construction, destroy all the game panels and icons
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("chickenIcon")){
                    Destroy(obj);
                }
                if(construct_site != null){
                    Destroy(construct_site);
                }
            }

            // construct_done Panel for each construction
            construct_doneBuildPanel();   

            // chicken house lvl 1
            buildPanel_done_construct("chickHouse_lvl1",card);

            // chick house lvl2
            buildPanel_done_construct("chickHouse_lvl2",card);
            
        } else {
            // clicking somewhere else will close the build panel
            if (buildPanel != null){
                Destroy(buildPanel);
            }
        }
        
        
    }

    private void buildPanel_done_construct(string tagName, Transform card){
        if (card.gameObject.CompareTag(tagName)){
            Construction = card.gameObject;
            if (GameObject.FindWithTag("build_panel") != null){
                Destroy(GameObject.FindWithTag("build_panel"));
            } else { 
                buildPanel = Instantiate(done_gamePanel,card.position,Quaternion.identity);
            }
        } 
        
    }

    private void construct_doneBuildPanel(){
        if(card.gameObject.CompareTag("Destruction")){
            Transform build_trans = GameObject.FindWithTag("build_panel").transform;
            Instantiate(construct_site_prefab,build_trans.position,Quaternion.identity);
            Destroy(Construction);
        } 
        if(card.gameObject.CompareTag("LevelUp")){
            Transform build_trans = GameObject.FindWithTag("build_panel").transform;
            Vector2 newPos = new Vector2(build_trans.position.x,build_trans.position.y);
            Instantiate(chickenHouselvl2,newPos,Quaternion.identity);
            Destroy(Construction);
        }
    }
}

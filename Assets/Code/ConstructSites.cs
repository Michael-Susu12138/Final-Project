using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: when lvl2 constructs, it changes the position of construct_sites



public class ConstructSites : MonoBehaviour
{
    Transform card;
    Camera cam;
    public LayerMask  UILayer;
    public GameObject gamePanel,chickenHouselvl1, chickenHouselvl2,chickenHouselvl3,done_gamePanel,destructionPanel,sheepHouselvl1, sheepHouselvl2,sheepHouselvl3;
    GameObject construct_site,buildPanel,Construction;
    string currentLevel;
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

#if UNITY_IPHONE || UNITY_ANDROID

        //Touch Ver
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                BuildPanel(touch.position);
            }
        }
#endif
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
                // foreach (GameObject obj in GameObject.FindGameObjectsWithTag("chickenIcon")){
                //     Destroy(obj);
                // }
                build_trans.gameObject.SetActive(false);
                if(construct_site.activeSelf){
                    construct_site.SetActive(false);
                }
            }

              

            // chicken house lvl 1
            buildPanel_done_construct("chickHouse_lvl1",card);

            // chick house lvl2
            buildPanel_done_construct("chickHouse_lvl2",card);

            //chick house lvl3
            buildPanel_done_construct("chickHouse_lvl3",card);

            // construct_done Panel for each construction
            construct_doneBuildPanel(currentLevel); 

            // sheep constructions
            if (card.gameObject.CompareTag("sheepIcon")){
                Transform build_trans = GameObject.FindWithTag("build_panel").transform;
                Instantiate(sheepHouselvl1,build_trans.position,Quaternion.identity);
                // after instantiation of the construction, destroy all the game panels and icons
                // foreach (GameObject obj in GameObject.FindGameObjectsWithTag("chickenIcon")){
                //     Destroy(obj);
                // }
                build_trans.gameObject.SetActive(false);
                if(construct_site.activeSelf){
                    construct_site.SetActive(false);
                }
            }
            buildPanel_done_construct("sheepHouse_lvl1",card);
            buildPanel_done_construct("sheepHouse_lvl2",card);
            buildPanel_done_construct("sheepHouse_lvl3",card);
            
        } else {
            // clicking somewhere else will close the build panel
            if (buildPanel != null){
                buildPanel.SetActive(false);
            }
        }
        
        
    }

    private void buildPanel_done_construct(string tagName, Transform card){
        if (card.gameObject.CompareTag(tagName)){
            Debug.Log(tagName);
            Construction = card.gameObject;
            currentLevel = tagName;
            if(GameObject.FindWithTag("build_panel") == null){
                buildPanel = Instantiate(done_gamePanel,card.position,Quaternion.identity);
            }
            else if (GameObject.FindWithTag("build_panel").activeSelf){
                // Destroy(GameObject.FindWithTag("build_panel"));
                GameObject.FindWithTag("build_panel").SetActive(false);
            } else { 
                // buildPanel = Instantiate(done_gamePanel,card.position,Quaternion.identity);
                buildPanel.SetActive(true);
                // Destroy(buildPanel.transform.Find("LevelUp").gameObject);
                // buildPanel.transform.GetChild(0).gameObject.SetActive(false);
            }
        } 
        
    }

    private void construct_doneBuildPanel(string currentLevel){
        GameObject nextLevelPrefab = null;
        if (currentLevel == "chickHouse_lvl1"){
            nextLevelPrefab = chickenHouselvl2;
        } else if (currentLevel == "chickHouse_lvl2"){
            nextLevelPrefab = chickenHouselvl3;
        } else if (currentLevel == "sheepHouse_lvl1"){
            nextLevelPrefab = sheepHouselvl2;
        } else if (currentLevel == "sheepHouse_lvl2"){
            nextLevelPrefab = sheepHouselvl3;
        } else {
            nextLevelPrefab = null;
        }
        if(card.gameObject.CompareTag("Destruction")){
            Transform build_trans = GameObject.FindWithTag("build_panel").transform;
            // Instantiate(construct_site_prefab,build_trans.position,Quaternion.identity);
            construct_site.SetActive(true);
            //destory its parent 
            if (Construction.transform.parent != null){
                Destroy(Construction.transform.parent.gameObject);
            }
            Destroy(Construction);
        } 
        if(card.gameObject.CompareTag("LevelUp")){
            Transform build_trans = GameObject.FindWithTag("build_panel").transform; 
            Instantiate(nextLevelPrefab,build_trans.position,Quaternion.identity);
            // destroy its parent
            if (Construction.transform.parent != null){
                Destroy(Construction.transform.parent.gameObject);
            }
            Destroy(Construction);
        }
    }
}

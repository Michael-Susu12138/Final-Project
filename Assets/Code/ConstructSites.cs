using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: when lvl2 constructs, it changes the position of construct_sites



public class ConstructSites : MonoBehaviour
{
    Transform card;
    Camera cam;
    public LayerMask  UILayer;
    List<GameObject> sites = new List<GameObject>();
    public GameObject gamePanel,chickenHouselvl1, chickenHouselvl2,chickenHouselvl3,done_gamePanel,destructionPanel,sheepHouselvl1, sheepHouselvl2,sheepHouselvl3;
    GameObject construct_site,buildPanel,Construction;
    string currentLevel;
    bool isCoroutineRunning = false;
    GameManager _gameManager;
    private void Start() {
        cam = Camera.main;
        
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        
        int signature = 0;
        foreach(GameObject site in GameObject.FindGameObjectsWithTag("construct_sites")){
            site.transform.name = "site:" + signature.ToString();
            sites.Add(site);
            signature+=1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCoroutineRunning)
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(CheckInput(Input.mousePosition));
            }
#endif

#if UNITY_IPHONE || UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended)
                {
                    StartCoroutine(CheckInput(touch.position));
                }
            }
#endif
        }
    }

    IEnumerator CheckInput(Vector2 inputPosition)
    {
        isCoroutineRunning = true;
        BuildPanel(inputPosition);
        yield return new WaitForSeconds(0.2f);
        isCoroutineRunning = false;
    }

    // private bool input_handled;
    void BuildPanel(Vector2 touchPos){

        // if (input_handled == true){
        //     return;
        // }

        Vector2 worldPos = cam.ScreenToWorldPoint(touchPos);

        if (Physics2D.OverlapPoint(worldPos, UILayer))
        {            
            card = Physics2D.OverlapPoint(worldPos, UILayer).transform;
            if(card.gameObject.CompareTag("construct_sites")){
                // create build_panel, if it exists, turn off the build panel
                Debug.Log("Hit");
                if(buildPanel != null && buildPanel.transform.position == card.position)
                {
                    Destroy(buildPanel);
                    buildPanel = null;
                }
                else
                {
                    if(buildPanel != null)
                    {
                        Destroy(buildPanel);
                    }
                    // buildPanel belongs to this particular construction sites
                    buildPanel = Instantiate(gamePanel, card.position, Quaternion.identity);
                    buildPanel.SetActive(true);
                }
                    

                construct_site = card.gameObject;    
            } else {
                // clicking somewhere else will close the build panel
                if (buildPanel != null){
                    Destroy(buildPanel);
                }
            }
            
            // chicken constructions
            //40 gold

            if(card.gameObject.CompareTag("chickenIcon") && _gameManager.gold >=40){
                _gameManager.useGold(40);
                Transform build_trans = GameObject.FindWithTag("build_panel").transform;
                GameObject c1 = Instantiate(chickenHouselvl1,build_trans.position,Quaternion.identity);
                // after instantiation of the construction, destroy all the game panels and icons
                // foreach (GameObject obj in GameObject.FindGameObjectsWithTag("chickenIcon")){
                //     Destroy(obj);
                // }
                build_trans.gameObject.SetActive(false);
                if(construct_site.activeSelf){
                    construct_site.SetActive(false);
                    c1.transform.name = "c1-"+construct_site.transform.name;
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
            // 50
            if (card.gameObject.CompareTag("sheepIcon") && _gameManager.gold >=50){
                _gameManager.useGold(40);
                Transform build_trans = GameObject.FindWithTag("build_panel").transform;
                GameObject s1 = Instantiate(sheepHouselvl1,build_trans.position,Quaternion.identity);
                // after instantiation of the construction, destroy all the game panels and icons
                // foreach (GameObject obj in GameObject.FindGameObjectsWithTag("chickenIcon")){
                //     Destroy(obj);
                // }
                build_trans.gameObject.SetActive(false);
                if(construct_site.activeSelf){
                    construct_site.SetActive(false);
                    s1.transform.name = "s1-"+construct_site.transform.name;
                }
            }
            buildPanel_done_construct("sheepHouse_lvl1",card);
            buildPanel_done_construct("sheepHouse_lvl2",card);
            buildPanel_done_construct("sheepHouse_lvl3",card);
            
        } else {
            // clicking somewhere else will close the build panel
            if (buildPanel != null){
                Destroy(buildPanel);
            }
        }
        // input_handled = true;
        
        
    }

    private void buildPanel_done_construct(string tagName, Transform card){
        if (card.gameObject.CompareTag(tagName)){
            Debug.Log(tagName);
            Construction = card.gameObject;
            currentLevel = tagName;
            if(GameObject.FindWithTag("build_panel") == null){
                buildPanel = Instantiate(done_gamePanel,card.position,Quaternion.identity);
                buildPanel.transform.name = "bp-"+Construction.transform.name;
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
        bool isMaxLevel = false;

        if (currentLevel == "chickHouse_lvl1")
        {
            nextLevelPrefab = chickenHouselvl2;
        }
        else if (currentLevel == "chickHouse_lvl2")
        {
            nextLevelPrefab = chickenHouselvl3;
        }
        else if (currentLevel == "chickHouse_lvl3")
        {
            isMaxLevel = true;
        }
        else if (currentLevel == "sheepHouse_lvl1")
        {
            nextLevelPrefab = sheepHouselvl2;
        }
        else if (currentLevel == "sheepHouse_lvl2")
        {
            nextLevelPrefab = sheepHouselvl3;
        }
        else if (currentLevel == "sheepHouse_lvl3")
        {
            isMaxLevel = true;
        }
        else
        {
            nextLevelPrefab = null;
        }
        if(card.gameObject.CompareTag("Destruction")){
            Transform build_trans = GameObject.FindWithTag("build_panel").transform;
            // Instantiate(construct_site_prefab,build_trans.position,Quaternion.identity);

            // construct_site stores something else ERROR
            int colonInd = build_trans.name.IndexOf(":");
            int sig = Int32.Parse(build_trans.name.Substring(colonInd+1));
            sites[sig].SetActive(true);
            // construct_site.SetActive(true);
            //destory its parent 
            if (Construction.transform.parent != null){
                Destroy(Construction.transform.parent.gameObject);
            }
            _gameManager.addGold(30);
            Destroy(Construction);
        } 
        if (card.gameObject.CompareTag("LevelUp"))
        {
            // Check if the construction is not at max level
            if (!isMaxLevel && _gameManager.gold >= 50) 
            {
                _gameManager.useGold(50);
                Transform build_trans = GameObject.FindWithTag("build_panel").transform;

                // Only instantiate the next level prefab if it exists
                if (nextLevelPrefab != null)
                {
                    GameObject nl = Instantiate(nextLevelPrefab, build_trans.position, Quaternion.identity);
                    nl.transform.name = "nl-" + build_trans.name;
                    if (nl.transform.childCount > 0)
                    {
                        nl.transform.GetChild(0).transform.name = "nl-" + build_trans.name;
                    }
                }

                // Destroy its parent
                if (Construction.transform.parent != null)
                {
                    Destroy(Construction.transform.parent.gameObject);
                }
                Destroy(Construction);
            }
        }
    }
}
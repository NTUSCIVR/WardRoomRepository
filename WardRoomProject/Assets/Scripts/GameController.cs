using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//manage the events that occurs in the application
public class GameController : MonoBehaviour {
    
    public static GameController Instance;

    [Tooltip("Male Model Prefab(?) for avatarSDK")]
    public GameObject maleModelObject;
    [Tooltip("Female Model Prefab(?) for avatarSDK")]
    public GameObject femaleModelObject;
    [Tooltip("The active model in use now")]
    public GameObject activeModel;
    float userHeight = 1.8f;

    private void Awake()
    {
        Instance = this;
        //if(DataCollector.Instance.imagePath != null)
        //{
        //    if (DataCollector.Instance.gender == GENDER.MALE)
        //        activeModel = Instantiate(maleModelObject);
        //    else
        //        activeModel = Instantiate(femaleModelObject);
        //}
    }

    // Use this for initialization
    void Start () {
        //to do
        //scale and activate current used model
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("StartScene");
            Destroy(DataCollector.Instance.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            CalibrateHeight();
        }
    }

    void CalibrateHeight()
    {
        //calibrate the scale of all objects currently registered using the current camera rig height as a reference
        //all the models need a box collider to represent its height
        //need find the curr camera in use to get height
        foreach(SteamVR_Camera go in FindObjectsOfType<SteamVR_Camera>())
        {
            if(go.gameObject.activeInHierarchy)
            {
                userHeight = go.transform.position.y;
                break;
            }
        }
        ScaleModel(activeModel);
    }

    void ScaleModel(GameObject model)
    {
        float modelHeight = model.GetComponent<BoxCollider>().bounds.extents.y * 2;
        //get the scale needed
        float ratio = userHeight / modelHeight;
        model.transform.localScale = new Vector3(ratio, ratio, ratio);
    }
}
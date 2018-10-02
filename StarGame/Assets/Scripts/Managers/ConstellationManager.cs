using System.Collections.Generic;
using UnityEngine;

public class ConstellationManager : MonoBehaviour
{
    private static ConstellationManager _instance = null;

    public static ConstellationManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ConstellationManager();
            }
            return _instance;
        }
    }

    // Use this for initialization
    public GameObject constellationPrefab;
    public List<Constellation> constellationItemList;
    // Update is called once per frame

    private ConstellationManager()
    {

    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public static float constrainEulerAngle(float x)
    {
        x %= 360;
        if (x > 180)
            x -= 360;
        return x;
    }

    public static bool IsMatchConstellation(Constellation display, GameObject currentFocus)
    {
        //Debug.Log(display.rotation.x - currentFocus.transform.rotation.x);
        //Debug.Log(Mathf.Abs(display.rotation.x - currentFocus.transform.rotation.x));
        if (Mathf.Abs(constrainEulerAngle(display.eulerAngles.x) - constrainEulerAngle(currentFocus.transform.eulerAngles.x)) < 2f &&
            Mathf.Abs(constrainEulerAngle(display.eulerAngles.y) - constrainEulerAngle(currentFocus.transform.eulerAngles.y)) < 2f &&
            Mathf.Abs(constrainEulerAngle(display.eulerAngles.z) - constrainEulerAngle(currentFocus.transform.eulerAngles.z)) < 2f)
            return true;
        return false;
    }

    private void Start()
    {
        for (int i = 0; i < constellationItemList.Count; i++)
        {

            constellationItemList[i].displayInMap =
                Instantiate(
                    constellationPrefab,
                    new Vector3(0, 0, 0),
                    Quaternion.Euler(constellationItemList[i].eulerAngles));
            constellationItemList[i].displayInMap.transform.localScale = constellationItemList[i].scale;
            constellationItemList[i].displayInMap.transform.parent = transform;
            constellationItemList[i].displayInMap.name =
                constellationItemList[i].name;
            // set display material
            constellationItemList[i].displayInMap.transform.
                GetComponentInChildren<Renderer>().material =
                    constellationItemList[i].displayMaterial;
        }

    }

    void Update()
    {


    }
}

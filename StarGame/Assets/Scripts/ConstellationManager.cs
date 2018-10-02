using System.Collections.Generic;
using UnityEngine;

public class ConstellationManager : MonoBehaviour
{
    // Use this for initialization

    public GameObject constellationPrefab;
    public List<ConstellationItem> constellationItemList;
    // Update is called once per frame

    public static float constrainEulerAngle(float x)
    {
        x %= 360;
        if (x > 180)
            x -= 360;
        return x;
    }

    public static bool IsMatchConstellation(ConstellationItem displayItem, GameObject currentFocus)
    {
        //Debug.Log(displayItem.rotation.x - currentFocus.transform.rotation.x);
        //Debug.Log(Mathf.Abs(displayItem.rotation.x - currentFocus.transform.rotation.x));
        if (Mathf.Abs(constrainEulerAngle(displayItem.eulerAngles.x) - constrainEulerAngle(currentFocus.transform.eulerAngles.x)) < 2f &&
            Mathf.Abs(constrainEulerAngle(displayItem.eulerAngles.y) - constrainEulerAngle(currentFocus.transform.eulerAngles.y)) < 2f &&
            Mathf.Abs(constrainEulerAngle(displayItem.eulerAngles.z) - constrainEulerAngle(currentFocus.transform.eulerAngles.z)) < 2f)
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

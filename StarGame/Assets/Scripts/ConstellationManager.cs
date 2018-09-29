using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationManager : MonoBehaviour {
    // Use this for initialization
    
    public GameObject constellationPrefab;
    public List<ConstellationItem> constellationItemList;
    // Update is called once per frame
    private void Start()
    {
        for (int i = 0; i < constellationItemList.Count; i++)
        {
            
            constellationItemList[i].displayInMap =
                Instantiate(
                    constellationPrefab, 
                    new Vector3(0, 0, 0), 
                    Quaternion.Euler(constellationItemList[i].rotation));
            constellationItemList[i].displayInMap.transform.parent = transform;
            constellationItemList[i].displayInMap.name = 
                constellationItemList[i].name;
            // set display material
            constellationItemList[i].displayInMap.transform.
                GetComponentInChildren<Renderer>().material = 
                    constellationItemList[i].displayMaterial;
        }
        
    }

    void Update () {

		
	}
}

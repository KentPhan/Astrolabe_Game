using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ConstellationsMenuManager : MonoBehaviour
{
    public Button CloseButton;
    public CanvasManager canvasManager;
    // Use this for initialization
    public ConstellationManager contellationManager;
    public Transform contentPanel;
    public ObjectPool constellationObjectPool;
    void CloseMenu()
    {
        Debug.Log(canvasManager);
        canvasManager.setMenuStatus(false);
    }

    public void activateConstellation(ConstellationItem currentItem)
    {
        canvasManager.setMenuStatus(false);
        canvasManager.setConstellationItem(currentItem);
    }

    void Start()
    {
        CloseButton.onClick.AddListener(CloseMenu);
        RefreshDisplay();
    }

    void RefreshDisplay()
    {
        RemoveButtons();
        AddButtons();
    }

    private void RemoveButtons()
    {
        Debug.Log("CHild" + contentPanel.childCount);
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = contentPanel.GetChild(0).gameObject;
            constellationObjectPool.ReturnObject(toRemove);
        }
    }

    private void AddButtons()
    {
        for (int i = 0; i < contellationManager.constellationItemList.Count; i++)
        {
            Debug.Log(i);
            ConstellationItem item = contellationManager.constellationItemList[i];
            GameObject newButton = constellationObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel, false);

            ConstellationsScrollViewButton sampleButton = newButton.GetComponent<ConstellationsScrollViewButton>();
            sampleButton.Setup(item, this);
        }
    }

}

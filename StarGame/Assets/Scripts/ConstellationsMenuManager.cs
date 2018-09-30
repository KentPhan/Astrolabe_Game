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

    public void activateConstellationMatch(int idInCostellationItemList)
    {
        Debug.Log("Click Button");
        canvasManager.setMenuStatus(false);
        canvasManager.setConstellationMatch(idInCostellationItemList);
    }

    void Start()
    {
        CloseButton.onClick.AddListener(CloseMenu);
        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        RemoveButtons();
        AddButtons();
    }

    private void RemoveButtons()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = contentPanel.GetChild(0).gameObject;
            constellationObjectPool.ReturnObject(toRemove);
        }
    }

    private GameObject AddButton(int i)
    {
        ConstellationItem item = contellationManager.constellationItemList[i];
        GameObject newButton = constellationObjectPool.GetObject();
        newButton.transform.SetParent(contentPanel, false);

        ConstellationsScrollViewButton sampleButton = newButton.GetComponent<ConstellationsScrollViewButton>();
        sampleButton.Setup(item, this, i);
        return newButton;
    }

    private void AddButtons()
    {
        var notCollectableColor = Color.grey;
        notCollectableColor.a = 0.3f;
        var collectableColor = Color.red;
        collectableColor.a = 0.3f;
        for (int i = 0; i < contellationManager.constellationItemList.Count; i++)
            if (contellationManager.constellationItemList[i].collectable == 1) {
                GameObject newButton = AddButton(i);
                newButton.GetComponent<Image>().color = collectableColor;
            }
        for (int i = 0; i < contellationManager.constellationItemList.Count; i++)
            if (contellationManager.constellationItemList[i].collectable == 0)
            {
                GameObject newButton = AddButton(i);
                newButton.GetComponent<Image>().color = notCollectableColor;
            }

    }

}

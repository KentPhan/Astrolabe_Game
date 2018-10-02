using UnityEngine;
using UnityEngine.UI;


public class ConstellationMenu : MonoBehaviour
{
    // Use this for initialization
    public ConstellationManager contellationManager;
    public Transform contentPanel;

    public void activateConstellationMatch(int idInCostellationItemList)
    {
        GameManager.Instance.GoToMatchStarsMode(idInCostellationItemList);
        CanvasManager.Instance.setConstellationMatch(idInCostellationItemList);
    }

    void Start()
    {
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
            GameManager.Instance.GetPool().ReturnObject(toRemove);
        }
    }

    private GameObject AddButton(int i)
    {
        ConstellationItem item = contellationManager.constellationItemList[i];
        GameObject newButton = GameManager.Instance.GetPool().GetObject();
        newButton.transform.SetParent(contentPanel, false);

        ConstellationsScrollViewButton sampleButton = newButton.GetComponent<ConstellationsScrollViewButton>();
        sampleButton.Setup(item, this, i);
        return newButton;
    }

    private void AddButtons()
    {
        var notCollectableColor = Color.red;
        notCollectableColor.a = 0.3f;
        var collectableColor = Color.black;
        collectableColor.a = 0.3f;
        for (int i = 0; i < contellationManager.constellationItemList.Count; i++)
            if (contellationManager.constellationItemList[i].collectable == 1)
            {
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

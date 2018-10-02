using UnityEngine;
using UnityEngine.UI;


public class ConstellationMenu : MonoBehaviour
{
    // Use this for initialization
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

    private ConstellationsScrollViewButton AddButton(int i)
    {
        Constellation item = ConstellationManager.Instance.constellationItemList[i];
        GameObject newButton = GameManager.Instance.GetPool().GetObject();
        newButton.transform.SetParent(contentPanel, false);

        ConstellationsScrollViewButton sampleButton = newButton.GetComponent<ConstellationsScrollViewButton>();
        sampleButton.Setup(item, this, i);
        return sampleButton;
    }

    private void AddButtons()
    {
        var notCollectableColor = Color.red;
        notCollectableColor.a = 0.3f;
        var collectableColor = Color.black;
        collectableColor.a = 0.3f;
        for (int i = 0; i < ConstellationManager.Instance.constellationItemList.Count; i++)
            if (ConstellationManager.Instance.constellationItemList[i].collectable == 1)
            {
                ConstellationsScrollViewButton newButton = AddButton(i);
                newButton.GetComponent<Image>().color = collectableColor;
            }
        for (int i = 0; i < ConstellationManager.Instance.constellationItemList.Count; i++)
            if (ConstellationManager.Instance.constellationItemList[i].collectable == 0)
            {
                ConstellationsScrollViewButton newButton = AddButton(i);
                newButton.constellationImage.sprite = ConstellationManager.Instance.constellationItemList[i].finishedIcon;
                newButton.GetComponent<Image>().color = notCollectableColor;
            }

    }

}

using UnityEngine;
using UnityEngine.UI;

public class ConstellationsScrollViewButton : MonoBehaviour
{
    ConstellationMenu menuManager;
    public Button constellationButton;
    public Text descriptionLabel;
    public Text collectableLabel;
    [Header("Constellation Border")]
    public Image constellationBorder;
    [Header("Constellation Title Image")]
    public Image constellationTitleImage;
    public float titleWidth;
    public float titleHeight;
    [Header("Scene References")]
    public Image constellationImage;

    int id;
    // Use this for initialization
    Constellation source;
    void activateConstellation()
    {
        if (source.collectable == 1)
        {
            menuManager.activateConstellationMatch(id);
        }
    }


    void Start()
    {
        constellationButton.onClick.AddListener(activateConstellation);
    }


    public void Setup(Constellation current,
        ConstellationMenu _menuManager, int idInContellationList)
    {
        id = idInContellationList;
        source = current;
        menuManager = _menuManager;

        constellationImage.sprite = source.icon;






        // If it is collected or not
        if (source.collectable == 1)
        {
            descriptionLabel.text = "????";
            collectableLabel.text = "Available";
            constellationTitleImage.sprite = CanvasManager.Instance.UIUnknownConstellationTitle;
            constellationBorder.sprite = CanvasManager.Instance.UIConstellationBlueBorder;
            constellationTitleImage.GetComponent<RectTransform>().sizeDelta = new Vector2(CanvasManager.Instance.UIUnknownConstellationTitle.rect.width, CanvasManager.Instance.UIUnknownConstellationTitle.rect.height);
        }

        else
        {
            descriptionLabel.text = source.name;
            collectableLabel.text = "Unavailable";
            constellationTitleImage.sprite = source.UIConstellationTitle;
            constellationBorder.sprite = CanvasManager.Instance.UIConstellationPurpleBorder;
            constellationTitleImage.GetComponent<RectTransform>().sizeDelta = new Vector2(source.UIConstellationTitle.rect.width, source.UIConstellationTitle.rect.height);
        }


        // Size constellation title images properly
        constellationTitleImage.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);


    }
}

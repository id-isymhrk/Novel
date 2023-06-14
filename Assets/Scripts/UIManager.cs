using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject nextPageIcon;

    [SerializeField]
    private GameObject selectButtons;
    public List<Button> _buttonList = new List<Button>();

    [SerializeField]
    private string prefabsDirectory = "Prefabs/";
    private const string BUTTON_PREFAB = "SelectButton";
    private const string OPTION_TEXT = "_text";

    public void SetSelectButton(string cmd, string name, string parameter)
    {
        name = name.Substring(name.IndexOf('"') + 1, name.LastIndexOf('"') - name.IndexOf('"') - 1);
        Button button = _buttonList.Find(n => n.name == name);
        if (button == null)
        {
            button = Instantiate(Resources.Load<Button>(prefabsDirectory + BUTTON_PREFAB), selectButtons.transform);
            button.name = name;
            button.transform.Translate(0, -_buttonList.Count * 100, 0);
            button.onClick.AddListener(() => SelectButtonOnClick(name));
            _buttonList.Add(button);
        }
        SetImage(cmd, parameter, button.image);
    }

    private void SetImage(string cmd, string parameter, Image image)
    {
        cmd = cmd.Replace(" ", "");
        parameter = parameter.Substring(parameter.IndexOf('"') + 1, parameter.LastIndexOf('"') - parameter.IndexOf('"') - 1);
        switch (cmd)
        {
            case OPTION_TEXT:
                image.GetComponentInChildren<TextMeshProUGUI>().SetText(parameter);
                //image.GetComponentInChildren<TextMeshProUGUI>().text = parameter;
                break;
            default:
                break;
        }
    }
    private void SelectButtonOnClick(string label)
    {
        foreach (Button button in _buttonList)
        {
            Destroy(button.gameObject);
        }

        _buttonList.Clear();

        JumpTo('"' + label + '"');
        ShowNextPage();
    }

    public float ButtonNum()
    {
        return _buttonList.Count;
    }
}

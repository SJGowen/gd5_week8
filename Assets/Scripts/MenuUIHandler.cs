using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]

public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker colorPicker;

    public void NewColorSelected(Color color)
    {
        //Debug.Log($"New color selected: {color}");
        MainManager.Instance.unitColour = color;
    }

    private void Start()
    {
        colorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        colorPicker.onColorChanged += NewColorSelected;
        colorPicker.SelectColor(MainManager.Instance.unitColour);
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        MainManager.Instance.SaveColour();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SaveColorClicked()
    {
        MainManager.Instance.SaveColour();
        //Debug.Log($"Saved color: {MainManager.Instance.unitColour}");
    }

    public void LoadColorClicked()
    {
        MainManager.Instance.LoadColour();
        colorPicker.SelectColor(MainManager.Instance.unitColour);
        //Debug.Log($"Loaded color: {MainManager.Instance.unitColour}");
    }
}

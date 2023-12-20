using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public bool shown = false;

    public Fader background;
    public Diluter logo;
    public ButtonGroup buttons;

    private bool loaded = false;

    private void Start()
    {
        Show(0.3f);
        loaded = true;
    }

    private void OnEnable()
    {
        if (loaded) Show(0.3f);
    }

    public void Show(float time)
    {
        background.Hide(time);
        logo.Show(time);
        buttons.Show();
        buttons.SetInteractable(true);

        this.shown = true;
    }

    public void Hide(float time)
    {
        background.Show(time);
        logo.Hide(time);
        buttons.Hide();
        buttons.SetInteractable(false);

        this.shown = false;
    }

    public void SetInteractable(bool interactable) 
    {
        buttons.SetInteractable(interactable);
    }
}

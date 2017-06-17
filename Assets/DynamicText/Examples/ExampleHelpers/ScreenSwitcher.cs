using UnityEngine;
using System.Collections.Generic;

public class ScreenSwitcher : MonoBehaviour
{
    public int activeScreenIndex = 0;
    public DynamicText numberLabel;

    List<Transform> childObjs = new List<Transform>();

    private static int compareTransformByName(Transform a, Transform b)
    {
        return a.name.CompareTo(b.name);
    }

    void Start()
    {
        childObjs.Clear();
        foreach (Transform child in transform)
            childObjs.Add(child);
        childObjs.Sort(compareTransformByName);

        setScreen(activeScreenIndex);

        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    void setScreen(int newIndex)
    {
        for (int a = 0; a < childObjs.Count; ++a)
        {
            Transform t = childObjs[a];
            t.gameObject.SetActive(false);
        }
        activeScreenIndex = newIndex;
        childObjs[activeScreenIndex].gameObject.SetActive(true);

        if (numberLabel != null)
            numberLabel.SetText((activeScreenIndex + 1).ToString());
    }

    void nextScreen()
    {
        int newIdx = Mathf.Clamp(activeScreenIndex + 1, 0, childObjs.Count - 1);
        if (newIdx != activeScreenIndex)
            setScreen(newIdx);
    }

    void previousScreen()
    {
        int newIdx = Mathf.Clamp(activeScreenIndex - 1, 0, childObjs.Count - 1);
        if (newIdx != activeScreenIndex)
            setScreen(newIdx);
    }
}

using UnityEngine;
using System.Collections;

// Starting from version 1.0.3 you can instantiate Dynamic Text objects
// in Start() and the bounds is immediately available right after that,
// assuming that the instantiation is successful (there must be a main
// camera for the Dynamic Text to grab).

public class TestDTAlignedPrefabArray : MonoBehaviour
{
    public DynamicText[] dynamicTextPrefabs; // DT prefabs to align w/given padding
    public Vector3 startPos = Vector3.zero;
    public Vector3 paddingBetweenTexts = Vector3.zero;

    void Start()
    {
        if (dynamicTextPrefabs == null || dynamicTextPrefabs.Length == 0)
        {
            Debug.LogError("Must fill Dynamic Text Prefabs array in editor");
            return;
        }

        // instantiate all in start
        Vector3 pos = startPos;
        for (int a = 0; a < dynamicTextPrefabs.Length; ++a)
        {
            DynamicText prefab = dynamicTextPrefabs[a];
            //Debug.Log(prefab.serializedText);
            Transform trn = Instantiate(prefab.transform) as Transform;
            trn.parent = this.transform; // optional

            DynamicText dt = trn.GetComponent<DynamicText>();
            dt.transform.position = pos;
            pos += new Vector3(dt.bounds.size.x, 0, 0);
            pos += paddingBetweenTexts; // padding may have x,y,z components...
        }
    }
}

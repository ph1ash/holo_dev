using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonPressAction : MonoBehaviour {

    public Material[] materials;
    Text throttleText;
    private Renderer rend;

    private int toggle = 0;

    void Start()
    {
        throttleText = GameObject.Find("ThrottleText").GetComponent<Text>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

	void OnSelect()
    {
        if (materials.Length < 2)
            return;

        rend.sharedMaterial = materials[toggle];
        toggle = ~toggle;
    }
}

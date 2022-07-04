using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;

public class HyperLinker : MonoBehaviour, IPointerClickHandler
{
    TextMeshProUGUI textMeshPro;
    Camera camera;
    Canvas canvas;

    void Start()
    {
        camera = Camera.main;

        canvas = gameObject.GetComponentInParent<Canvas>();
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            camera = null;
        else
            camera = canvas.worldCamera;

        textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
        textMeshPro.ForceMeshUpdate();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(textMeshPro, Input.mousePosition, camera);

        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = textMeshPro.textInfo.linkInfo[linkIndex];
            Application.OpenURL(linkInfo.GetLinkID());
        }
    }
}

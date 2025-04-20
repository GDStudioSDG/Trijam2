using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnergyDiller : MonoBehaviour
{
    public static int energy = 100;
    [SerializeField] TextMeshProUGUI energyText;
    [SerializeField] TextMeshProUGUI TimeTB;

    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;
    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = FindFirstObjectByType<EventSystem>();
        energyText.text = "Left Energy: " + energy.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeTB.text = "Time: " + EnemyShedule.GetTime();
    }

    private void Update()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        //raycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
        {

            //Debug.Log("Hit UI: " + result.gameObject.name);
            print(result.gameObject.transform.parent.tag);
            if (result.gameObject.transform.parent.tag == "HidenUI")
            {
                result.gameObject.transform.parent.GetComponent<ButtonOptions>().IsRaycasted = true;
                results = null;
                break;
            }

        }
    }
}

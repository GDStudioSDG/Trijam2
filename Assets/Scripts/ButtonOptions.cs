using System.Collections.Generic;
using Towers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonOptions : MonoBehaviour
{
    [SerializeField]Camera cam;
    [SerializeField] Transform Tower;
    //[SerializeField]GameObject Place;
    [SerializeField]GameObject TowerW; 
    [SerializeField]GameObject TowerG;
    [SerializeField]GameObject TowerL;
    [SerializeField]GameObject TowerF;


    public bool IsRaycasted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        cam = Camera.main;
    }
    public void Fire()
    {
        
        
        GameObject Temp = Instantiate(TowerF);
        int level = Tower.GetComponentInChildren<Tower>().GetLevel();
        Temp.GetComponent<Tower>().SetLevel(level);
        Temp.transform.position = Tower.transform.position;
        DestroyImmediate(Tower.gameObject);
        Tower = Temp.transform;
        //print(Temp.gameObject.transform.position);
        
        //Temp.transform.localPosition = new Vector3(0, 0, 0);
    }
    public void Water()
    {

        GameObject Temp = Instantiate(TowerW);
        int level = Tower.GetComponentInChildren<Tower>().GetLevel();
        Temp.GetComponent<Tower>().SetLevel(level);
        Temp.transform.position = Tower.transform.position;
        DestroyImmediate(Tower.gameObject);
        Tower = Temp.transform;
    }
    public void Ground()
    {
        GameObject Temp = Instantiate(TowerG);
        int level = Tower.GetComponentInChildren<Tower>().GetLevel();
        Temp.GetComponent<Tower>().SetLevel(level);
        Temp.transform.position = Tower.transform.position;
        DestroyImmediate(Tower.gameObject);
        Tower = Temp.transform;
    }
    public void Lightning()
    {
       
        GameObject Temp = Instantiate(TowerL);
        int level = Tower.GetComponentInChildren<Tower>().GetLevel();
        Temp.GetComponent<Tower>().SetLevel(level);
        Temp.transform.position = Tower.transform.position;
        DestroyImmediate(Tower.gameObject);
        Tower = Temp.transform;
    }
    public void LevelUp()
    {
        Tower temp = Tower.GetComponent<Tower>();
        if (EnergyDiller.energy - 10 >= 0 && temp.GetLevel() <3)
        {
            EnergyDiller.energy -= 10;
            temp.SetLevel(temp.GetLevel()+1);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRaycasted)
        GetComponent<CanvasGroup>().alpha = 1;
        else GetComponent<CanvasGroup>().alpha = 0;
        IsRaycasted = false;


        transform.LookAt(new Vector3(cam.transform.position.x, transform.position.y, cam.transform.position.z));
        //transform.Rotate(0,180,0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class Code2D : MonoBehaviour
{
    public GameObject Cylinder;
    public GameObject LastGO;

    public long StartNumber;
    public long Number;
    private long TempNumber;

    public List<long> existingnumbers;
    public List<long> numberstoadd;

    public Transform Location;
    public Transform NextLocation;

    public TextMeshProUGUI Numbertext;
    public TextMeshProUGUI CalculationProcess;
    public TMP_InputField InputField;

    public Vector3 rotation;

    public float OddRotation = 20f;
    public float EvenRotation = -8f;

    public bool ODD;
    public bool AlreadyCalculated = false;
    public bool FinishedCalculating;
    public bool FinishedCreating;
    public bool FirstNumber = true;

    public float NextChangeTime;
    public float WaitTime;

    void Start()
    {
        Number = StartNumber;
        TempNumber = Number;
    }

    void Update()
    {
        long.TryParse(InputField.text, out StartNumber);
        //Number = InputField.text.ToString()(long);
    }

    public void calacB()
    {
        Number = StartNumber;
        //if (AlreadyCalculated == false)
        //{
            StartCoroutine(calculate(WaitTime));
            //AlreadyCalculated = true;
        //}
    }
    public void CreateTree()
    {
        if (FirstNumber == true)
        {
            LastGO = Instantiate(Cylinder, new Vector2(0, 0), Quaternion.identity);
            Numbertext = LastGO.transform.GetChild(1).transform.Find("Number").GetComponent<TextMeshProUGUI>();
            Numbertext.SetText(Number.ToString());
            //LastGO.transform.Find("Number").GetComponent<TextMeshProUGUI>().SetText(Number.ToString());
            FirstNumber = false;
        }
        if (FirstNumber == false)
        {
            for (int i = 0; i < existingnumbers.Count; i++)
            {
                long number = existingnumbers[i];
                //Even
                if (number % 2 == 0 && FinishedCreating == false)
                {
                    NextLocation = LastGO.transform.Find("CylinderTop").GetComponent<Transform>();
                    LastGO = Instantiate(Cylinder, new Vector2 (NextLocation.transform.position.x + 2, NextLocation.transform.position.y + 0.5f), Quaternion.identity);
                    Numbertext = LastGO.transform.GetChild(1).transform.Find("Number").GetComponent<TextMeshProUGUI>();
                    Numbertext.SetText(number.ToString());
                    Debug.Log(number);
                    //LastGO.transform.Find("Number").GetComponent<TextMeshProUGUI>().SetText(Number.ToString());
                }
                //Odd
                if (number % 2 == 1 && FinishedCreating == false)
                {
                    NextLocation = LastGO.transform.Find("CylinderTop").GetComponent<Transform>();
                    LastGO = Instantiate(Cylinder, new Vector2(NextLocation.transform.position.x - 1, NextLocation.transform.position.y + 0.5f), Quaternion.identity);
                    Numbertext = LastGO.transform.GetChild(1).transform.Find("Number").GetComponent<TextMeshProUGUI>();
                    Numbertext.SetText(number.ToString());
                    Debug.Log(number); 
                    //LastGO.transform.Find("Number").GetComponent<TextMeshProUGUI>().SetText(Number.ToString());
                }
                if (i == existingnumbers.Count)
                {
                    FinishedCreating = true;
                    StopCoroutine(calculate(WaitTime));
                }
            }
        }
    }

    public IEnumerator calculate(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        if (FinishedCalculating == false)
        {
            if (Number != 1)
            {
                CalculationProcess.SetText(Number.ToString());
                if (numberstoadd.Contains(Number))
                {
                    existingnumbers.Add(Number);
                }
                FinishedCalculating = false;

                //Even
                if (Number % 2 == 0)
                {
                    ODD = false;
                    TempNumber = Number;
                    Number = TempNumber / 2;
                    existingnumbers.Add(Number);
                    numberstoadd.Sort();                                                             
                    StartCoroutine(calculate(WaitTime));
                    CalculationProcess.SetText(TempNumber.ToString() + " / 2 = " + Number.ToString());
                }
                //Odd
                else
                {
                    ODD = true;
                    TempNumber = Number;
                    Number = TempNumber * 3 + 1;
                    existingnumbers.Add(Number);
                    numberstoadd.Sort();
                    StartCoroutine(calculate(WaitTime));
                    CalculationProcess.SetText(TempNumber.ToString() + " * 3 + 1 = " + Number.ToString());
                }
            }
            if (Number == 1)
            {
                FinishedCalculating = true;
                if (numberstoadd.Contains(Number))
                {
                    numberstoadd.Add(Number);
                }
            }
            existingnumbers = existingnumbers.OrderBy(existingnumbers => existingnumbers).ToList();
        }
        if (FinishedCalculating == true)
        {
            CreateTree();
        }
    }
}

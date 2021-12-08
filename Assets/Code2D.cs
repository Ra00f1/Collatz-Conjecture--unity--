using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Code2D : MonoBehaviour
{
    public GameObject Cylinder;
    public GameObject LastGO;

    public int StartNumber;
    public int Number;

    public List<int> existingnumbers;
    public List<int> numberstoadd;

    public Transform Location;
    public Transform NextLocation;

    public TextMeshProUGUI Numbertext;

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
    }

    void Update()
    {
    }

    public void calacB()
    {
        if (AlreadyCalculated == false)
        {
            StartCoroutine(calculate(WaitTime));
            AlreadyCalculated = true;
        }
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
                int number = existingnumbers[i];
                //Even
                if (number % 2 == 0 && FinishedCreating == false)
                {
                    NextLocation = LastGO.transform.Find("CylinderTop").GetComponent<Transform>();
                    LastGO = Instantiate(Cylinder, new Vector2 (NextLocation.transform.position.x - 1, NextLocation.transform.position.y + 0.5f), Quaternion.identity);
                    Numbertext = LastGO.transform.GetChild(1).transform.Find("Number").GetComponent<TextMeshProUGUI>();
                    Numbertext.SetText(number.ToString());
                    Debug.Log(number);
                    //LastGO.transform.Find("Number").GetComponent<TextMeshProUGUI>().SetText(Number.ToString());
                }
                //Odd
                if (number % 2 == 1 && FinishedCreating == false)
                {
                    NextLocation = LastGO.transform.Find("CylinderTop").GetComponent<Transform>();
                    LastGO = Instantiate(Cylinder, new Vector2(NextLocation.transform.position.x + 2, NextLocation.transform.position.y + 0.5f), Quaternion.identity);
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
                if (numberstoadd.Contains(Number))
                {
                    existingnumbers.Add(Number);
                }
                FinishedCalculating = false;

                //Even
                if (Number % 2 == 0)
                {
                    ODD = false;
                    Number = Number / 2;
                    existingnumbers.Add(Number);
                    numberstoadd.Sort();``1                                                             
                    StartCoroutine(calculate(WaitTime));
                }
                //Odd
                else
                {
                    ODD = true;
                    Number = (Number * 3) + 1;
                    existingnumbers.Add(Number);
                    numberstoadd.Sort();
                    StartCoroutine(calculate(WaitTime));
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

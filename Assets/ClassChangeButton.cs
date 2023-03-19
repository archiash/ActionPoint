using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClassChangeButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler 
{
    public Character.CharacterClass characterClass;
    [SerializeField] ClassPanel classPanel;
    public Image ButtonPanel;

    [SerializeField] Image holdCounterPrefab;
    Image holdCounter;

    bool onHold = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        onHold = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onHold = false;
        holdTime = 0;
        ClearHoldCounter();
    }

    void ClearHoldCounter()
    {
        if (holdCounter != null)
        {
            holdCounter.transform.parent = new GameObject().transform;
            DestroyImmediate(holdCounter);
        }
    }

    public void OnChangeClassButton()
    {
        classPanel.SelectedChangeClass(characterClass,this);
        ButtonPanel.color = Color.gray;
    }

    public void ResetSelected()
    {
        ButtonPanel.color = Color.white;
    }

    float holdTime = 0;

    void Update()
    {
        
        //print($"onHold: {onHold}");
        //print($"Touch: {Input.touchCount}");
#if UNITY_ANDROID && !UNITY_EDITOR
        if(onHold && Input.touchCount > 0 && Inventory.instance.Money >= 300 && characterClass != Character.instance.Class)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 10;
            if (GameObject.FindGameObjectWithTag("OverCanvas").transform.childCount == 0)
                holdCounter = Instantiate(holdCounterPrefab, pos, Quaternion.identity, GameObject.FindGameObjectWithTag("OverCanvas").transform);
            holdCounter.fillAmount = holdTime / 3;
            holdTime += Time.deltaTime;
            if(holdTime >= 3){
                OnChangeClassButton();
                onHold = false;
                holdTime = 0;
                ClearHoldCounter();
            }
        }
#elif UNITY_EDITOR

        if (onHold && Input.GetMouseButton(0) && Inventory.instance.Money >= 300 && characterClass != Character.instance.Class)
        {
            //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //if (GameObject.FindGameObjectWithTag("OverCanvas").transform.childCount == 0)
                //holdCounter = Instantiate(holdCounterPrefab, pos, Quaternion.identity, GameObject.FindGameObjectWithTag("OverCanvas").transform);
            //holdCounter.transform.parent = GameObject.FindGameObjectWithTag("OverCanvas").transform;
            OnChangeClassButton();
            onHold = false;
        }
#endif
    }
}

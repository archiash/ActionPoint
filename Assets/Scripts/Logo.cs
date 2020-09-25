using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Logo : MonoBehaviour, IPointerClickHandler
{
    public Animator animator;
    public void OnPointerClick(PointerEventData eventData)
    {
            animator.SetTrigger("OnTap");        
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class OnPointEvent : MonoBehaviour, IPointerEnterHandler // required interface when using the OnPointerEnter method.
{
   
    public AudioSource audioSource ;

    //Do this when the cursor enters the rect area of this selectable UI object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.Play();
    }
}

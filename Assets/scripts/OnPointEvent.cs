using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

/*!
	\brief Класс отвечающий за воспроизведеие звука при наведении на кнопку
*/

public class OnPointEvent : MonoBehaviour, IPointerEnterHandler // required interface when using the OnPointerEnter method.
{
   
   ///Звук
    public AudioSource audioSource; 

    //Do this when the cursor enters the rect area of this selectable UI object.
    public void OnPointerEnter(PointerEventData eventData) ///Метод, отвечающий за воспроизведеие звука при наведении на кнопку
    {
        audioSource.Play();
    }
}

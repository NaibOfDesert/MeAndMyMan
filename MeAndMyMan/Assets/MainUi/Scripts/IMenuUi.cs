using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

public interface IMenuUi
{
    public void OnPointerClick(PointerEventData eventData);
    public void OnPointerEnter(PointerEventData eventData);
    public void OnPointerExit(PointerEventData eventData);


}

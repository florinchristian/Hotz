using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SafeDoors : MonoBehaviour
{
   private void OnMouseDown()
   {
      SafeType safe = GetComponentInParent<SafeType>();
      safe.OnMouseDown();
   }
}

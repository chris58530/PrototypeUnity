using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThrowable
{
   void Throw(Vector3 direction);
   void PickUp(Transform parentTransform);
   void Pickoff();
   GameObject GetObject();
}

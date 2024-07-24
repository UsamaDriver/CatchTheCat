using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMove : MonoBehaviour
{
    private void OnMouseUp()
    {
      
            HexGridGenerator.instance.MyMove(this.gameObject);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}

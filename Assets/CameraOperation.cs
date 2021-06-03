using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class CameraOperation : MonoBehaviour
{
    private void Start()
    {

    }
    // do dostosowania!
    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(Vector3.forward *200* Time.deltaTime);
            this.transform.Translate(Vector3.down *200* Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(Vector3.back *200* Time.deltaTime);
            this.transform.Translate(Vector3.up *200* Time.deltaTime);   
        }
       
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Rotate(Vector3.up, -1);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Rotate(Vector3.up, 1);
     
        }
    }
}

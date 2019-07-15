using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStable : MonoBehaviour {

    [SerializeField]
    GameObject TheCar;
    [SerializeField]
    float CarX;
    [SerializeField]
    float CarY;
    [SerializeField]
    float CarZ;

    // Update is called once per frame
    void Update ()
    {
        CarX = TheCar.transform.eulerAngles.x;
        CarY = TheCar.transform.eulerAngles.y;
        CarZ = TheCar.transform.eulerAngles.z;

        transform.eulerAngles = new Vector3(CarX -CarX, CarY, CarZ - CarZ);
    }
}

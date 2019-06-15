using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollide : MonoBehaviour
{
    public GameObject startAnchor;
    public GameObject endAnchor;
    public float holdTime = 0.5f;
    public float rampUpTime = 0.5f;


    void Start()
    {
        LineRenderer line = gameObject.AddComponent<LineRenderer>();
        line.SetVertexCount(2);
        line.SetWidth(0.015F, 0.015F);
        // we want the lines to use local space and not world space
        line.useWorldSpace = false;
        line.useLightProbes = false;
        line.receiveShadows = false;
        line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        line.material.color = Color.red;
    }
    void FixedUpdate()
    {

        Vector3 Start = new Vector3(startAnchor.transform.position.x, startAnchor.transform.position.y, startAnchor.transform.position.z);
        Vector3 End = new Vector3(endAnchor.transform.position.x, endAnchor.transform.position.y, endAnchor.transform.position.z);
        GetComponent<LineRenderer>().SetPosition(0, Start);
        GetComponent<LineRenderer>().SetPosition(1, End);


    }

    void Update()
    {
        if (Physics.Linecast(startAnchor.transform.position, endAnchor.transform.position))
        {
            GetComponent<DamagedFlash>().TookDamage();
            //Debug.Log("blocked!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRIG : MonoBehaviour
{
    public VRMap leftHand;
    public VRMap rightHand;
    
    
    // Update is called once per frame
    void LateUpdate()
    {
        leftHand.Map();
        rightHand.Map();
    }
}

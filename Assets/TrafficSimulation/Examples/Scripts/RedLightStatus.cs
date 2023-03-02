// Traffic Simulation
// https://github.com/mchrbn/unity-traffic-simulation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrafficSimulation;

public class RedLightStatus : MonoBehaviour
{

    public int lightGroupId;  // Belong to traffic light 1 or 2?
    public Intersection intersection;
    
    private Light pointLight;
    private Light pointLightGreen;

    void Start(){
        pointLight = this.transform.GetChild(0).GetComponent<Light>();
        pointLightGreen = this.transform.GetChild(1).GetComponent<Light>();
    }

    // Update is called once per frame
    void Update(){
        SetTrafficLightColor();
    }

    void SetTrafficLightColor(){
        if (lightGroupId == intersection.currentRedLightsGroup)
        {
            pointLight.enabled = true;
            pointLightGreen.enabled = false;
        }

        else
        {
            pointLightGreen.enabled = true;
            pointLight.enabled = false;
        }
            
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [Range (0, 1) ]
    public float TimeofDay;
    public float DayDuration = 30;

    public AnimationCurve SunCurve;
    public AnimationCurve MoonCurve;
    public AnimationCurve SkyboxCurve;

    public Material DaySkybox;
    public Material NightSkybox;

    public Light Sun;
    public Light Moon;

    private float SunIntensity; 
    private float MoonIntensity;
    // Start is called before the first frame update
    void Start()
    {
        SunIntensity = Sun.intensity;
        MoonIntensity = Moon.intensity;    
    }

    // Update is called once per frame
    void Update()
    {
        TimeofDay += Time.deltaTime / DayDuration;
        if(TimeofDay >= 1) TimeofDay -= 1;

        RenderSettings.skybox.Lerp(NightSkybox, DaySkybox, SkyboxCurve.Evaluate(TimeofDay));
        RenderSettings.sun = SkyboxCurve.Evaluate(TimeofDay) > 0.1f ? Sun : Moon; 
        DynamicGI.UpdateEnvironment(); 

        Sun.transform.localRotation = Quaternion.Euler(TimeofDay * 360f, 180, 0);
        Moon.transform.localRotation = Quaternion.Euler(TimeofDay * 360f + 170, 180, 0);
        
        Sun.intensity = SunIntensity * SunCurve.Evaluate(TimeofDay);
        Moon.intensity = MoonIntensity * MoonCurve.Evaluate(TimeofDay);
            
        

    }
}

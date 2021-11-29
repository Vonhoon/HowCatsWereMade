using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStuff : MonoBehaviour
{

    public TempMenu temp;

    public void IntroFinished()
    {
        temp.introFin = true;
        temp.particle.Stop();                
    }
}

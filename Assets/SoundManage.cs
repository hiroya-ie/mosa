using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManage : MonoBehaviour
{
	public AudioSource NoiseAudioSource,SEAccelerateAudioSource,SECrashAudioSource,BGMAudioSoucrce;
    // Start is called before the first frame update


    public void AccelerateSound(){SEAccelerateAudioSource.Play();}
    
    public void  CrashSound(){SECrashAudioSource.Play();}
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManage : MonoBehaviour
{
	public AudioSource NoiseAudioSource,SEAudioSource,BGMAudioSoucrce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AccelerateSound()
    {
        SEAudioSource.Play();
    }
}

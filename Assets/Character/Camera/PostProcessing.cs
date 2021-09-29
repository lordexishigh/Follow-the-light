using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    private PostProcessVolume vol;

    private ChromaticAberration chrom;
    private Movement mov;
    private float drinkPower;

    // Start is called before the first frame update
    void Start()
    {
        vol = GetComponent<PostProcessVolume>();
        vol.profile.TryGetSettings(out chrom);

        drinkPower = 0.1f;

        GameObject player = GameObject.FindWithTag("Player");
        mov = player.GetComponent<Movement>();
    }

    public void Drink()
    {
        if (chrom.intensity.value < 1f)
        {
            chrom.intensity.value += drinkPower;
            drinkPower = drinkPower * 1.1f;
            mov.maxVelocity = mov.maxVelocity + 0.025f;
            print(chrom.intensity.value);
        }
    }
}

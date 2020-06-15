using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VisualFX", menuName = "VisualFX", order = 1)]
public class VisualFX : ScriptableObject {

    public GameObject prefab;
    public Color tint = Color.white;

    [HideInInspector]
    public float duration;
    float timer = 0;
    GameObject inst;
    AudioSource soundInst;
    Renderer renderer;
    ParticleSystem ps;

    public void StartFX(Transform par)
    {
        inst = Instantiate(prefab);
        inst.transform.parent = par;
        inst.transform.localPosition = Vector3.zero;
        renderer = inst.GetComponent<Renderer>();
        soundInst = inst.GetComponent<AudioSource>();

        ps = inst.GetComponent<ParticleSystem>();

        if (renderer)
            renderer.material.SetColor("_TintColor", tint);

        if (ps)
        {
            ParticleSystem.MainModule main = ps.main;
            main.startColor = tint;
        }

        timer = 0;
    }

    public bool UpdateFX(float dt)
    {
        if (duration > 0)
        {
            timer += dt;
            if (timer > duration)
            {
                Destroy(inst);
                return true;
            }
            else
            {
                // in the last second, we want to fade out gracefully.
                
                float timeRemaining = duration - timer;
                if (timeRemaining < 1)
                {
                    // stop new particles being emmitted. 
                    // TODO - ideally we want this to happen based on the particles lifetime, not on the last second
                    if (ps)
                        ps.Stop();
                    
                    // fade out renderers like the sphere forcefield
                    if (renderer)
                    {
                        Color col = renderer.material.GetColor("_TintColor");
                        col.a = timeRemaining;
                        renderer.material.SetColor("_TintColor", col);
                    }

                    // fade out the sound
                    if (soundInst)
                        soundInst.volume = timeRemaining;
                        
                }
                return false;
            }
        }
        return false;
    }

    // this will cause it to stop in the next second and fade out nicely
    public void StopFX()
    {
        duration = timer+1.0f;
    }
}

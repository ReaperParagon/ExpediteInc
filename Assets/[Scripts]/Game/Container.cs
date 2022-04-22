using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ObjectType))]
public class Container : MonoBehaviour
{
    public delegate void OnObjectEnterEvent();
    public static event OnObjectEnterEvent OnObjectEnterContainer;

    public UnityEvent OnCorrect;
    public UnityEvent OnIncorrect;
    private ObjectType objectType;
    private AudioSource AS;

    public List<AudioClip> audioClips = new List<AudioClip>();

    public ParticleSystem correctPS;
    public ParticleSystem incorrectPS;

    private void Start()
    {
        objectType = GetComponent<ObjectType>();
        objectType.SetObjectColor(objectType.colorType);

        AS = GetComponent<AudioSource>();
    }

    /// Functions ///

    public void Correct()
    {
        AS.Stop();
        AS.clip = audioClips[0];
        AS.Play();

        correctPS.Play();
    }

    public void Incorrect()
    {
        AS.Stop();
        AS.clip = audioClips[1];
        AS.Play();

        incorrectPS.Play();
    }

    /// Collisions ///

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            // Check if the correct object was dropped
            if (BlockHelperFunctions.GetBlockParent(other.transform).TryGetComponent(out ObjectType ot) && ot.colorType == objectType.colorType)
                OnCorrect?.Invoke();
            else
                OnIncorrect?.Invoke();

            OnObjectEnterContainer?.Invoke();
        }

        // Destroy the other
        Destroy(BlockHelperFunctions.GetBlockParent(other.transform).gameObject);

    }
}

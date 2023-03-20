using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDisplay : MonoBehaviour
{
    [SerializeField] Transform _rightHand;
    [SerializeField] Transform _leftHand;
    [SerializeField] Transform _helmet;


    private void OnEnable()
    {
        EmptyHand(); // KK: isnt needed if scene is proper
        PlayerStorage.OnEquipLeftHand += SetLeftHand;
        PlayerStorage.OnEquipRightHand += SetRightHand;
        PlayerStorage.OnHelmetChange += HelmetChange;
    }
    private void OnDisable()
    {
        PlayerStorage.OnEquipLeftHand -= SetLeftHand;
        PlayerStorage.OnEquipRightHand -= SetRightHand;
        PlayerStorage.OnHelmetChange -= HelmetChange;
    }
    // KK: Would be better to cache the objects and no looping on child
    public void SetLeftHand(GameObject? prefab)
    {
        if (prefab == null)
        {
            EmptyHand(true);
            return;
        }
        Instantiate(prefab, _leftHand);
    }

    public void SetRightHand(GameObject? prefab)
    {
        if (prefab == null)
        {
            EmptyHand(false);
            return;
        }
        Instantiate(prefab, _rightHand);
    }
    public void HelmetChange(GameObject? prefab)
    {
        if (prefab == null)
        {
            foreach (Transform childTransform in _helmet.transform)
            {
                Destroy(childTransform.gameObject);
            }
            return;
        }
        Instantiate(prefab, _helmet);
    }

    public void EmptyHand(bool? isLeftHand = null)
    {
        if (isLeftHand == null)
        {
            foreach (Transform childTransform in _leftHand.transform)
            {
                Destroy(childTransform.gameObject);
            }
            foreach (Transform childTransform in _rightHand.transform)
            {
                Destroy(childTransform.gameObject);
            }
            return;
        }

        if ((bool)isLeftHand)
        {
            foreach (Transform childTransform in _leftHand.transform)
            {
                Destroy(childTransform.gameObject);
            }
            return;
        }

        foreach (Transform childTransform in _rightHand.transform)
        {
            Destroy(childTransform.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YieldContainer : MonoBehaviour
{
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    private static readonly Dictionary<float,WaitForSeconds> _waitForSecondsDictionary = new Dictionary<float,WaitForSeconds>();
    
    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        if (!_waitForSecondsDictionary.ContainsKey(seconds))
        {
            _waitForSecondsDictionary.Add(seconds, new WaitForSeconds(seconds));
        }
        
        return _waitForSecondsDictionary[seconds];
    }
}

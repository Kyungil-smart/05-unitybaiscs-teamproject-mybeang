using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YieldContainer : MonoBehaviour
{
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    private static readonly Dictionary<float,WaitForSeconds> _waitForSecondsDictionary = 
        new Dictionary<float,WaitForSeconds>();
    private static readonly Dictionary<float,WaitForSecondsRealtime> _waitForRealSecondsDictionary = 
        new Dictionary<float,WaitForSecondsRealtime>();
    
    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        if (!_waitForSecondsDictionary.ContainsKey(seconds))
        {
            _waitForSecondsDictionary.Add(seconds, new WaitForSeconds(seconds));
        }
        
        return _waitForSecondsDictionary[seconds];
    }
    
    public static WaitForSecondsRealtime WaitForRealSeconds(float seconds)
    {
        if (!_waitForRealSecondsDictionary.ContainsKey(seconds))
        {
            _waitForRealSecondsDictionary.Add(seconds, new WaitForSecondsRealtime(seconds));
        }
        
        return _waitForRealSecondsDictionary[seconds];
    }
}

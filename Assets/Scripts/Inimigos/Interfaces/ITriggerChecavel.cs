using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerChecavel
{
    bool IsAggroed { get; set; }
    bool IsWithingStrikingDistance { get; set; }
    
    void SetAggroStatus(bool isAggroed);
    void SetStrikingDistanceBool(bool isWithingStrikingDistance);
}

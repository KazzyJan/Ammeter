using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multimeter_model: MonoBehaviour
{
    public static int switch_positions;
    [HideInInspector] public float voltageU;
    [HideInInspector] public float current_strengthI;
    [HideInInspector] public float alternating_current;
    public float powerP;
    public float resistanceR;
}

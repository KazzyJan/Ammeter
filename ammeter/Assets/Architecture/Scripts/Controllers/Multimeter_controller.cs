using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multimeter_controller : MonoBehaviour
{
    Multimeter_model model;
    // Start is called before the first frame update
    void Start()
    {
        model = GetComponent<Multimeter_model>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (Multimeter_model.switch_positions)
        {
            case 0:
                Multimeter_wiev.answer = "0";
                break;
            case 1:
                Multimeter_wiev.answer = ResistanceCalculation(model).ToString();
                break;
            case 2:
                Multimeter_wiev.answer = CurrentCalculation(model).ToString();
                break;
            case 3:
                Multimeter_wiev.answer = AlternatingCurrentCalculation(model).ToString();
                break;
            case 4:
                Multimeter_wiev.answer = VoltageCalculation(model).ToString();
                break;
        }
    }

    float ResistanceCalculation(Multimeter_model model)
    {
        return (float)System.Math.Round(model.resistanceR, 2);
    }

    float CurrentCalculation(Multimeter_model model)
    {
        model.current_strengthI = Mathf.Sqrt(model.powerP / model.resistanceR);
        return (float)System.Math.Round(model.current_strengthI, 2);
    }

    float AlternatingCurrentCalculation(Multimeter_model model)
    {
        model.alternating_current = 0.01f;
        return (float)System.Math.Round(model.alternating_current, 2);
    }

    float VoltageCalculation(Multimeter_model model)
    {
        model.voltageU = Mathf.Sqrt(model.powerP * model.resistanceR);
        return (float)System.Math.Round(model.voltageU, 2);
    }
}

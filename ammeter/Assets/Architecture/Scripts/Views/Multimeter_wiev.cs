using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Multimeter_wiev : MonoBehaviour
{
    [SerializeField] private GameObject main_camera_obj;
    public static string answer;
    private TextMeshPro screen_text;
    private TextMeshProUGUI tmp_r;
    private TextMeshProUGUI tmp_v_alt;
    private TextMeshProUGUI tmp_a;
    private TextMeshProUGUI tmp_v;
    private Camera main_camera;
    private Outline outline;
    private Animator anim;
    private bool mouse_on_switch = false;
    private int switch_positions_wiev;
    [HideInInspector] public bool animation_ended;
    // Start is called before the first frame update
    void Start()
    {
        switch_positions_wiev = 0;

        Transform screen = transform.Find("Multimeter_low").Find("Screen_text");
        screen_text = screen.GetComponent<TextMeshPro>();

        tmp_r = transform.Find("Main_canvas").Find("TMP(R)").GetComponent<TextMeshProUGUI>();
        tmp_a = transform.Find("Main_canvas").Find("TMP(A)").GetComponent<TextMeshProUGUI>();
        tmp_v_alt = transform.Find("Main_canvas").Find("TMP(V~)").GetComponent<TextMeshProUGUI>();
        tmp_v = transform.Find("Main_canvas").Find("TMP(V)").GetComponent<TextMeshProUGUI>();

        main_camera = main_camera_obj.GetComponent<Camera>();
        Transform switch_low = transform.Find("Switch_low");
        outline = switch_low.GetComponent<Outline>();
        outline.OutlineWidth = 0;

        anim = GetComponent<Animator>();
        animation_ended = true;
    }


    // Update is called once per frame
    void Update()
    {
        mouse_on_switch = SwitchLighting();
        switch_positions_wiev = SwitchControll(switch_positions_wiev, mouse_on_switch, anim);
        Multimeter_model.switch_positions = switch_positions_wiev;

        screen_text.text = answer;

        DisplayCanvas(switch_positions_wiev, tmp_r, tmp_a, tmp_v_alt, tmp_v);
    }


    void DisplayCanvas(int switch_positions_wiev, TextMeshProUGUI tmp_r, TextMeshProUGUI tmp_a, TextMeshProUGUI tmp_v_alt, TextMeshProUGUI tmp_v)
    {
        switch (switch_positions_wiev)
        {
            case 0:
                tmp_r.SetText("Ω " + "0");
                tmp_a.SetText("A " + "0");
                tmp_v.SetText("V " + "0");
                tmp_v_alt.SetText("~ " + "0");
                break;
            case 1:
                tmp_r.SetText("Ω " + answer);
                tmp_a.SetText("A " + "0");
                tmp_v.SetText("V " + "0");
                tmp_v_alt.SetText("~ " + "0");
                break;
            case 2:
                tmp_r.SetText("Ω " + "0");
                tmp_a.SetText("A " + answer);
                tmp_v.SetText("V " + "0");
                tmp_v_alt.SetText("~ " + "0");
                break;
            case 3:
                tmp_r.SetText("Ω " + "0");
                tmp_a.SetText("A " + "0");
                tmp_v.SetText("V " + "0");
                tmp_v_alt.SetText("~ " + answer);
                break;
            case 4:
                tmp_r.SetText("Ω " + "0");
                tmp_a.SetText("A " + "0");
                tmp_v.SetText("V " + answer);
                tmp_v_alt.SetText("~ " + "0");
                break;
        }
    }


    bool SwitchLighting()
    {
        Ray ray = main_camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 350f, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 350f))
        {
            if (hit.collider.tag == "Switch" && mouse_on_switch == false)
            {
                outline.OutlineWidth = 10;
                mouse_on_switch = true;
            }
        }
        else if (mouse_on_switch == true)
        {
            outline.OutlineWidth = 0;
            mouse_on_switch = false;
        }
        return mouse_on_switch;
    }
    int SwitchControll(int switch_positions_wiev, bool mouse_on_switch, Animator anim)
    {
        if (mouse_on_switch == true)
        {
            if (Input.mouseScrollDelta.y < 0.0f && animation_ended)
            {
                if (switch_positions_wiev == 0)
                {
                    switch_positions_wiev = 4;
                }
                else
                {
                    switch_positions_wiev -= 1;
                }
                anim = AnimDown(switch_positions_wiev, anim);
                animation_ended = false;
            }
            if (Input.mouseScrollDelta.y > 0.0f && animation_ended)
            {
                if (switch_positions_wiev == 4)
                {
                    switch_positions_wiev = 0;
                }
                else
                {
                    switch_positions_wiev += 1;
                }
                anim = AnimUp(switch_positions_wiev, anim);
                animation_ended = false;
            }
        }
        return switch_positions_wiev;
    }
    Animator AnimUp(int switch_positions_wiev, Animator anim) 
    {
        anim.SetInteger("switch_positions_wiev_anim", switch_positions_wiev);
        return anim;
    }
    Animator AnimDown(int switch_positions_wiev, Animator anim)
    {
        anim.SetInteger("switch_positions_wiev_anim", switch_positions_wiev * -1);
        return anim;
    }
    void EndAnimationEvent()
    {
        animation_ended = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeHour : MonoBehaviour, IInteractable
{

    [SerializeField]
    private int correctHour = 0;
    static private string[] hours = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };
    private int currentIndex = 0;
    private TMP_Text text;
    private System.Diagnostics.Stopwatch stopWatch;

    private void Start() {
        stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();
        activate(true);
        text = GetComponentInChildren<TMP_Text>();
        text.text = hours[currentIndex];
    }

    public void activate(bool flag)
    {
       this.enabled = flag;     
    }

    public AnimationsEnum Interact()
    {
        double time = stopWatch.Elapsed.TotalMilliseconds/1000;
        if(time > 0.2)
        {
            nextNumber();
            stopWatch.Restart();
            return AnimationsEnum.PRESS_BTN;
        }
        stopWatch.Restart();
        return AnimationsEnum.NONE;
       
    }

    public void nextNumber(){
        if(currentIndex != 11) { currentIndex++;} 
        else { currentIndex = 0; }
        text.text = hours[currentIndex];
    }

    public bool isActive()
    {
        return this.enabled;
    }

    public void Power()
    {
        
    }

    void IInteractable.Update()
    {
        
    }
}

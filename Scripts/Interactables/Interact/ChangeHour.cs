using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeHour : MonoBehaviour, IInteractable
{

    [SerializeField]
    private int correctHour = 0;
    [SerializeField]
    private List<Sprite> images;
    [SerializeField]
    private int currentIndex = 0;
    private Image image;
    private System.Diagnostics.Stopwatch stopWatch;

    private void Start() {
        stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();
        activate(true);
        image = GetComponentInChildren<Image>();
        image.sprite = images[currentIndex];
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
        if(currentIndex != 2) { currentIndex++;} 
        else { currentIndex = 0; }
        image.sprite = images[currentIndex];
    }

    public bool isActive()
    {
        return  currentIndex != correctHour;
    }

    public void Power()
    {
        
    }

    void IInteractable.Update()
    {
        
    }
}

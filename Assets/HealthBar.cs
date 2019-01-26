using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float maxValue = 3;
    public SpriteRenderer firstHeart;
    public SpriteRenderer secondHeart;
    public SpriteRenderer thirdHeart;
    
    public void SetValue(float value)
    {
        this.thirdHeart.enabled = value > maxValue / 3 * 2;
        this.secondHeart.enabled = value > maxValue / 3;
        this.firstHeart.enabled = value > 0;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}

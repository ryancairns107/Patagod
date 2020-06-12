using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shiphealth : MonoBehaviour
{
	// ship health applies the values of the health of the ship to the sliders so it will be visualized

	public Slider slider;
	public Gradient gradient;
	public Image fill;
	public int startvalue;
	public float health;
     void Update()
    {
		
		health = slider.value;

	}
    public void SetMaxHealth(int health)
	{
		// sets the health of the ship into the slider and change the health color based on the fradient created
		slider.maxValue = health;
		slider.minValue = 0;
		slider.value = startvalue;

		fill.color = gradient.Evaluate(1f);
	}

	public void SetHealth(int health)
	{
		slider.value = health;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}

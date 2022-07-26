using UnityEngine;
using System.Collections;

public class Slider_Handler : MonoBehaviour {

	public UILabel labelSliderValue;
	public UISlider sliderRatings;

	static float sliderValue = 0.02f ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnSliderValueChanged()
	{
		//update Text 
		sliderValue = 20 * sliderRatings.value;
		sliderValue = Mathf.Round(sliderValue  );
		sliderValue = sliderValue/2;

		labelSliderValue.text = ""+sliderValue;

		//update Position of Label 
	
		labelSliderValue.transform.position = new Vector3 (sliderRatings.thumb.transform.position.x, labelSliderValue.transform.position.y, labelSliderValue.transform.position.z);

		//labelSliderValue.transform.position.x = sliderRatings.thumb.transform.position.x;

	}






	public static float SliderValue {
		get {
			return sliderValue;
		}
	}
}

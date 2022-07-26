using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AddImagesGrid : MonoBehaviour {
	// Use this for initialization

	public GameObject imagesPrefab;
	public GameObject imagesGridRoot;
	//public static GameObject textureSelextedSelfie;
	public   GameObject thanksDialog;


	public  bool isDialogEnabled = false ;
	ArrayList selfieList = new ArrayList();
	static List<ImagePlaneData> m_imagePlanesDataList = new List<ImagePlaneData>();
	static int currentselfieIndex;

	int m_noOf_images;
	void Start () {
		isDialogEnabled = false;
		m_noOf_images = 0;
		m_imagePlanesDataList.Clear();
		StartCoroutine(GetSelfieFromServer() );

	}
	
	// Update is called once per frame
	void Update () {

		if((ImagePlaneData.DownloadCount-1) < (m_noOf_images )){

			for (int i = 0; i< selfieList.Count; i++)
			{
				//Debug.Log ("Called AddImages"+i );
				GameObject g = selfieList[i] as GameObject;
				UITexture ut = g.GetComponent<UITexture>();
				ut.mainTexture = m_imagePlanesDataList[i].SelfieImage;
				//UISprite Us = g.GetComponent<UITexture>();
				//ut.material = new Material(Shader.Find("Unlit/Transparent Colored"));
				//ut.material.mainTexture = m_imagePlanesDataList[i].SelfieImage;

			}
		}
	}

	void AddImagesinGrid()
	{

		for (int j = 0; j < m_noOf_images; j++) {

			GameObject item = NGUITools.AddChild(imagesGridRoot, imagesPrefab);
			item.name = ""+j;
			selfieList.Add(item);

		}
		//yield return new WaitForEndOfFrame();
		Debug.Log ("Called AddImages2");
		imagesGridRoot.GetComponent<UIGrid>().Reposition();
	}

	IEnumerator GetSelfieFromServer()
	{
		AskServer.GetLatestCarouselData(OncarosuelData, OnErrorInCarouselData);
		yield break;
	}

	void OncarosuelData(Hashtable data)
	{
		if(data.ContainsKey("data")){
			m_noOf_images = Convert.ToInt32(data["data"]);
		}
		Debug.Log("m_noOf_images  "+m_noOf_images);
		
		ArrayList dataArray = (ArrayList)data["images"];
		m_noOf_images = dataArray.Count;

		for(int i = 0; i < dataArray.Count; i++){
			
			m_imagePlanesDataList.Add(new ImagePlaneData((Hashtable) dataArray[i])); 
			
		}
		AddImagesinGrid();
	}
	
	void OnErrorInCarouselData(string Error)
	{
		Debug.Log (""+Error );
	}


	public void OnselectedaGridItem(GameObject sender)
	{
		int index = Convert.ToInt32(""+sender.name);
		//Debug.Log ("HUrrrrrr   "+sender  + "   index  = " +index +" "+selfieList.Count );
		UITexture  uit = GameObject.Find("Texture").GetComponent<UITexture>();
		uit.mainTexture = m_imagePlanesDataList[index].SelfieImage;
		currentselfieIndex = m_imagePlanesDataList[index].id;
	}


	public void ONSubmitButton()
	{
		if(!isDialogEnabled)
			AskServer.SubmitRatings( currentselfieIndex, Slider_Handler.SliderValue, OnSubmitRatings , OnErrorInSubmitRating );

	}


	void OnSubmitRatings(Hashtable data)
	{

		if(data.ContainsKey("Save"))
		{


			thanksDialog.SetActive(true);
			//GameObject go = gameObject.("ThanksDialog");
			//go.SetActive(true);
			isDialogEnabled = true;
		}


		
	}

	void OnErrorInSubmitRating(string Error )
	{
		Debug.Log("Error in Submit Ratings" +Error);
	}


	public void  OnDialogOkButton()
	{
		Debug.Log ("Called OK");

		thanksDialog.SetActive(false);
//		GameObject go = GameObject.Find("ThanksDialog");
//		go.SetActive(false);
		isDialogEnabled = false;
	}



}




//	Debug.Log("Load Inventory: " + Inventory.instance.items.Count);
//	for (int i = 0; i < Inventory.instance.items.Count; i++) {
//		StoreInfo info = Inventory.instance.items[i].info;
//		
//		GameObject item = NGUITools.AddChild(listGridRoot, listItemPrefab);
//		item.name = "InventoryItem" + i;
//		item.transform.FindChild("Name").GetComponent<UILabel>().text = info.displayName;
//		item.transform.FindChild("Description").GetComponent<UILabel>().text = info.description;
//	}
//	yield return new WaitForEndOfFrame();
//	listGridRoot.GetComponent<UIGrid>().Reposition();
//	yield return new WaitForEndOfFrame();
//	labelLoading.enabled = false;


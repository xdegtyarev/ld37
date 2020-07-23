using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class SpecialContentSizeFitter : MonoBehaviour {
	RectTransform rt;
	[SerializeField] GameObject targetContent;
	[SerializeField] Vector2 padding;
	[SerializeField]bool x;
	[SerializeField]bool y;
	ILayoutElement targetLayout;

	void Awake(){
		targetLayout = targetContent.GetComponent<ILayoutElement>();
		rt = GetComponent<RectTransform>();
	}

	void Update(){
		if(y){
			rt.sizeDelta = new Vector2(rt.sizeDelta.x,targetLayout.preferredHeight + padding.y);
		}
		if(x){
			rt.sizeDelta = new Vector2(targetLayout.preferredWidth + padding.x,rt.sizeDelta.y);
		}
		if(x && y){
			rt.sizeDelta = new Vector2(targetLayout.preferredWidth + padding.x,targetLayout.preferredHeight + padding.y);
		}
	}
}

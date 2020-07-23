using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayHelper{
	public static T Random<T>(this List<T> list){
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	public static T Random<T>(this T[] list){
		return list[UnityEngine.Random.Range(0, list.Length)];
	}

}

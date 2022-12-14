using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public static class ArrayExtensions
{
	public static bool Contains<T>(this T[] array, T value)
	{
		for (int a = 0; a < array.Length; a++)
		{
			if (array[a].Equals(value))
				return true;
		}

		return false;
	}

	public static void StepForward<T>(this T[] array, int steps)
	{
		for (int a = array.Length - 1; a >= steps; a--)
			array[a] = array[a - steps];

		for (int a = 0; a < steps; a++)
			array[a] = default;
	}

	public static void DebugLogArray<T>(this T[] array)
	{
		for (int a = 0; a < array.Length; a++)
		{
			Debug.Log($"[{a}]{array[a]}");
		}
	}

	public static T Random<T>(this T[] array, System.Random random) => array[random.Next(0, array.Length)];
	public static T Random<T>(this T[] array) => array.Random<T>(random: PixLi.RandomDistribution.Random._GlobalRandom);
}
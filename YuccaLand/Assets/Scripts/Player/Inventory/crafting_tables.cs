using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crafting_tables : MonoBehaviour {

	public int find_recipe (int id1, int id2) {
		Debug.Log ("ID1="+id1+" ID2="+id2);
		int result = -1;

		switch (id1) {
		case 1:
			result = crafting_id_1 (id2);
			break;
		default:
			break;
		}

		return result;
	}

	int crafting_id_1 (int id2) {
		int result = -1;

		switch (id2) {
		case 1001:
			
			break;
		}

		return result;
	}
		
}

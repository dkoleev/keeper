using UnityEngine;

namespace ThirdParty.Low_Poly_Animated_Animals {
	public class PanelControll : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
	


		} 

		public void PanelToggle(){
			if (this.gameObject.activeSelf) {
				this.gameObject.SetActive (false);
			
			} else {
				this.gameObject.SetActive (true);
			}
		}
	}
}

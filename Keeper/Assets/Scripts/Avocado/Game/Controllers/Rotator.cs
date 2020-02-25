using Avocado.Game.Managers.InputManager;
using UnityEngine;

namespace Avocado.Game.Controllers
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 2.0f;
        
        private InputManager _inputManager;
        
        private void Awake() {
            _inputManager = GameObject.FindWithTag("InputManager").GetComponent<InputManager>();
        }

        private void Update()
        {
            Twist();
        }

        void Twist()
        {
            var h1 = _inputManager.MoveAxis.x;
            var v1 = _inputManager.MoveAxis.y;
            
            if (h1 == 0f && v1 == 0f)
            {
                /*// this statement allows it to recenter once the inputs are at zero 
                Vector3 curRot = transform.localEulerAngles; // the object you are rotating
                Vector3 homeRot;
                if (curRot.y > 180f)
                {
                    // this section determines the direction it returns home 
                    homeRot = new Vector3(0f, 359.999f, 0f); //it doesnt return to perfect zero 
                }
                else
                {
                    // otherwise it rotates wrong direction 
                    homeRot = Vector3.zero;
                }

                transform.localEulerAngles = Vector3.Slerp(curRot, homeRot, Time.deltaTime * 2);*/
            }
            else
            {
                transform.localEulerAngles = new Vector3(0f, Mathf.Atan2(h1, v1) * 180 / Mathf.PI,0f);
                //Vector3 curRot = transform.localEulerAngles;
               //transform.localEulerAngles = Vector3.Slerp(curRot, new Vector3(curRot.x, Mathf.Atan2(h1, v1) * 180 / Mathf.PI, curRot.z), Time.deltaTime * 2);
            }
        }
    }
}
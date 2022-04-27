using Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Enums;
using DG.Tweening;
using UnityEngine.AI;
namespace Scripts.Behaviours
{
    public class CarBehaviour : MonoBehaviour
    { 
        [SerializeField]private LevelEnum.Side _side;

        public  void Initialize(Color colorLeft, Color colorRight)
        {
            SetMaterial(colorLeft, colorRight);
        }

        private void SetMaterial(Color colorLeft, Color colorRight)
        {
            var renderer = GetComponent<MeshRenderer>();
            if (_side == LevelEnum.Side.Left)
            {
                renderer.materials[0].color = colorLeft;
            }
            else
            {
                renderer.materials[0].color = colorRight;
            }
        }


        private void MoveNextPosition()
        {

        }

        public void CarMovement(Vector3[] pathPoins, float reachTime)
        {
            transform.DOPath(pathPoins, reachTime).SetEase(Ease.InOutSine);
            Debug.Log("car moving " + transform.name);
        }



      
    }
}

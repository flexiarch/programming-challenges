using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Puzzle : MonoBehaviour
    {
        public int Number;
        private MainScript main;
        private bool inMotion = false;
        private Vector3 targetPosition;

        private void Start()
        {
            var r = GameObject.Find("Board");
            main = r.GetComponent<MainScript>();
        }

        public void StartMoveSequenceTo(Vector3 target)
        {
            if (inMotion) return;
            targetPosition = target;
            inMotion = true;
        }

        private void OnMouseDown()
        {
            main.HitPuzzle(Number);
        }

        private void Update()
        {
            if (inMotion)
            {
                float step = main.speed * Time.deltaTime;

                transform.position =
                    Vector3.MoveTowards(
                        transform.position,
                        targetPosition,
                        step);

                if (transform.position == targetPosition)
                {
                    inMotion = false;
                }
            }
        }
    }

}

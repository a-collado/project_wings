using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FasTPS
{
    public class Manager : MonoBehaviour
    {
        public List<Point> allPoints = new List<Point>();

        private void Start()
        {
            PopulateAllPoints();
        }

        public void Init()
        {
            PopulateAllPoints();
        }

        void PopulateAllPoints()
        {
            Point[] allP = GetComponentsInChildren<Point>();

            foreach(Point p in allP)
            {
                if (!allPoints.Contains(p))
                {
                    if (!allPoints.Contains(p))
                        allPoints.Add(p);
                }
            }
        }

        public Point ReturnNeighbourPointFromDirection(Vector3 inputDirection, Point curPoint)
        {
            Point retVal = null;

            foreach(Neighbour n in curPoint.neighbours)
            {
                if(n.direction == inputDirection)
                {
                    retVal = n.target;
                }
            }
            return retVal;
        }

        public Neighbour ReturnNeighbour(Vector3 inputDirection, Point curPoint)
        {
            Neighbour retVal = null;

            foreach(Neighbour n in curPoint.neighbours)
            {
                if(n.direction == inputDirection)
                {
                    retVal = n;
                }
            }
            return retVal;
        }

        public Point ReturnClosest(Vector3 from)
        {
            Point retVal = null;

            float minDist = Mathf.Infinity;

            for(int i = 0; i < allPoints.Count; i++)
            {
                float dist = Vector3.Distance(from, allPoints[i].transform.parent.position);

                if (allPoints[i].dismountPoint)
                    continue;

                if(dist < minDist)
                {
                    retVal = allPoints[i];
                    minDist = dist;
                }
            }
            return retVal;
        }
    }
}

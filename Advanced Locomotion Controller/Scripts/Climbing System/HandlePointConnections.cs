#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FasTPS
{
    [ExecuteInEditMode]
    public class HandlePointConnections : MonoBehaviour
    {
        public float minDistance = 2.5f;
        public float directThreshold = 1;
        public bool updateConnections;
        public bool resetConnections;

        List<Point> allPoints = new List<Point>();
        Vector3[] availableDirections = new Vector3[15];

        void CreateDirections()
        {
            availableDirections[0] = new Vector3(1, 0, 0);
            availableDirections[1] = new Vector3(-1, 0, 0);
            availableDirections[2] = new Vector3(0, 1, 0);
            availableDirections[3] = new Vector3(0, -1, 0);
            availableDirections[4] = new Vector3(-1, -1, 0);
            availableDirections[5] = new Vector3(1, 1, 0);
            availableDirections[6] = new Vector3(1, -1, 0);
            availableDirections[7] = new Vector3(-1, 1, 0);
            availableDirections[8] = new Vector3(0, 0, -1);
            availableDirections[9] = new Vector3(0, 0, 1);
            availableDirections[10] = new Vector3(1, 0, -1);
            availableDirections[11] = new Vector3(-1, 0, -1);
            availableDirections[12] = new Vector3(1, 0, 1);
            availableDirections[13] = new Vector3(-1, 0, 1);
            availableDirections[14] = new Vector3(0, 0, 2);
        }

        private void Update()
        {
            if (updateConnections)
            {
                GetPoints();
                CreateDirections();
                CreateConnections();
                FindDismountCandidates();
                RefreshAll();

                updateConnections = false;
            }

            if (resetConnections)
            {
                GetPoints();
                for (int p = 0; p < allPoints.Count; p++)
                {
                    allPoints[p].neighbours.Clear();
                }
                RefreshAll();
                resetConnections = false;
            }

        }

        void GetPoints()
        {
            allPoints.Clear();
            Point[] hp = GetComponentsInChildren<Point>();
            allPoints.AddRange(hp);
        }

        void CreateConnections()
        {
            for (int p = 0; p < allPoints.Count; p++)
            {
                Point curPoint = allPoints[p];

                for (int d = 0; d < availableDirections.Length; d++)
                {

                    List<Point> candidatePoints = CandidatePointsOnDirection(availableDirections[d], curPoint);

                    Point closest = ReturnClosest(candidatePoints, curPoint, availableDirections[d]);

                    if (closest != null)
                    {
                        if (Vector3.Distance(curPoint.transform.position, closest.transform.position) < minDistance)
                        {
                            if (Mathf.Abs(availableDirections[d].y) > 0 && Mathf.Abs(availableDirections[d].x) > 0)
                            {
                                if (Vector3.Distance(curPoint.transform.position, closest.transform.position) > directThreshold)
                                {
                                    continue;
                                }
                            }

                            AddNeighbour(curPoint, closest, availableDirections[d]);
                        }
                    }
                }
            }
        }
        List<Point> CandidatePointsOnDirection(Vector3 targetDirection, Point from)
        {
            List<Point> retVal = new List<Point>();

            for (int p = 0; p < allPoints.Count; p++)
            {
                Point targetPoint = allPoints[p];

                bool skip = false;

                foreach(Neighbour n in from.neighbours)
                {
                    if(targetPoint == n.target && n.direction == new Vector3(0, 0, 1) && targetDirection == new Vector3(0, 0, 2))
                    {
                        skip = true;
                    }
                }

                if (skip)
                    continue;

                if (targetPoint == from)
                    continue;

                Vector3 relativePosition = from.transform.InverseTransformPoint(targetPoint.transform.position);

                if (IsDirectionValid(targetDirection, relativePosition))
                {
                    retVal.Add(targetPoint);
                }
            }

            return retVal;
        }

        Point ReturnClosest(List<Point> l, Point from, Vector3 dir)
        {
            Point retVal = null;

            float minDist = Mathf.Infinity;

            for (int i = 0; i < l.Count; i++)
            {
                float tempDist = Vector3.Distance(l[i].transform.position, from.transform.position);

                if (tempDist < minDist && l[i] != from)
                {
                    minDist = tempDist;
                    retVal = l[i];
                }
            }

            if(dir == new Vector3(0, 0, 2))
            {
                float minAngle = 180;

                foreach(Point p in l)
                {
                    float tempAngle = Vector3.Angle(from.transform.position, p.transform.position);

                    if (tempAngle < minAngle && p != from)
                    {
                        minAngle = tempAngle;
                        retVal = p;
                    }
                }
            }

            return retVal;
        }

        bool IsDirectionValid(Vector3 targetDirection, Vector3 candidate)
        {
            bool retVal = false;

            float targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.y) * Mathf.Rad2Deg;
            float angle = Mathf.Atan2(candidate.x, candidate.y) * Mathf.Rad2Deg;

            if(targetDirection.y != 0)
            {
                targetAngle = Mathf.Abs(targetAngle);
                angle = Mathf.Abs(angle);
            }

            if(targetDirection.z != 0)
            {
                targetAngle = Mathf.Abs(Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg);
                angle = Mathf.Abs(Mathf.Atan2(candidate.x, candidate.z) * Mathf.Rad2Deg);

                if (angle < targetAngle + 15f && angle > targetAngle - 15f)
                {
                    retVal = true;
                }
            }
            else
            {
                if (angle < targetAngle + 15f && angle > targetAngle - 15f)
                {
                    retVal = true;
                }
            }

            /*/if (angle < targetAngle + 22.5f && angle > targetAngle - 22.5f)
            {
                retVal = true;
            }/*/

            return retVal;
        }

        void AddNeighbour(Point from, Point target, Vector3 targetDir)
        {
            Neighbour n = new Neighbour();
            n.direction = targetDir;
            n.target = target;
            n.cType = (Vector3.Distance(from.transform.position, target.transform.position) < directThreshold) ? ConnectionType.inBetween : ConnectionType.direct;

            if (targetDir == availableDirections[8]) { n.cType = ConnectionType.direct; }

            from.neighbours.Add(n);

            UnityEditor.EditorUtility.SetDirty(from);
        }

        void RefreshAll()
        {

        }

        public List<Connection> GetAllConections()
        {
            List<Connection> retVal = new List<Connection>();

            for (int p = 0; p < allPoints.Count; p++)
            {
                for (int n = 0; n < allPoints[p].neighbours.Count; n++)
                {
                    Connection con = new Connection();
                    con.target1 = allPoints[p];
                    con.target2 = allPoints[p].neighbours[n].target;
                    con.cType = allPoints[p].neighbours[n].cType;

                    if (!ContainsConnection(retVal, con))
                    {
                        retVal.Add(con);
                    }
                }
            }
            return retVal;
        }

        bool ContainsConnection(List<Connection> l, Connection c)
        {
            bool retVal = false;

            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].target1 == c.target1 && l[i].target2 == c.target2 || l[i].target2 == c.target1 && l[i].target1 == c.target2)
                {
                    retVal = true;
                    break;
                }
            }
            return retVal;
        }

        void FindDismountCandidates()
        {
            GameObject dismountPrefab = Resources.Load("Nested-Characters-Don'tDelete/Dismount") as GameObject;
            if (dismountPrefab == null)
            {
                Debug.Log("No Dismount Prefab Found!");
                return;
            }

            HandlePoints[] hp = GetComponentsInChildren<HandlePoints>();

            List<Point> candiates = new List<Point>();

            Point[] disPoint = GetComponentsInChildren<Point>();

            for (int i = 0; i < hp.Length; i++)
            {
                if (hp[i].dismountPoint)
                {
                    candiates.AddRange(hp[i].pointsInOrder);
                }
            }

            for (int i = 0; i < disPoint.Length; i++)
            {
                if (disPoint[i].dismountPoint)
                {
                    if (!candiates.Contains(disPoint[i]))
                    {
                        candiates.Add(disPoint[i]);
                    }
                }
            }

            if (candiates.Count > 0)
            {
                GameObject parentObj = new GameObject();
                parentObj.name = "Dismount Points";
                parentObj.transform.parent = transform;
                parentObj.transform.localPosition = Vector3.zero;
                parentObj.transform.position = candiates[0].transform.localPosition;

                foreach (Point p in candiates)
                {
                    Transform worldP = p.transform.parent;
                    GameObject dismountObject = Instantiate(dismountPrefab, worldP.position, worldP.rotation) as GameObject;

                    Vector3 targetPosition = worldP.position + ((worldP.forward / 1.6f) + Vector3.up * 1.2f);
                    dismountObject.transform.position = targetPosition;

                    Point dismountPoint = dismountObject.GetComponentInChildren<Point>();

                    dismountPoint.dismountPoint = true;

                    Neighbour n = new Neighbour();
                    n.direction = Vector3.up;
                    n.target = dismountPoint;
                    n.cType = ConnectionType.dismount;
                    p.neighbours.Add(n);

                    Neighbour n2 = new Neighbour();
                    n2.direction = -Vector3.up;
                    n2.target = p;
                    n2.cType = ConnectionType.dismount;
                    dismountPoint.neighbours.Add(n2);

                    dismountObject.transform.parent = parentObj.transform;
                }
            }
        }
    }

    public class Connection
    {
        public Point target1;
        public Point target2;
        public ConnectionType cType;
    }
}
#endif

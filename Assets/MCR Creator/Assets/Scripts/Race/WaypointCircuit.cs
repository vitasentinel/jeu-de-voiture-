// Description: script based on the deprecated Standard Asset WaypointCircuit.cs
using System;
using System.Collections;
using UnityEngine;


    public class WaypointCircuit : MonoBehaviour
    {
        public WaypointList waypointList = new WaypointList();
        private Vector3[] points;
        private float[] distances;

        public float Length { get; private set; }

        private int p0n;
        private int p1n;
        private int p2n;
        private int p3n;

        private float i;
        private Vector3 P0;
        private Vector3 P1;
        private Vector3 P2;
        private Vector3 P3;

        // Use this for initialization
        private void Awake()
        {
            if(waypointList.items.Length > 1)CachePositionsAndDistances();
        }
        

        public RoutePoint GetRoutePoint(float dist)
        {
            // position and direction
            Vector3 p1 = GetRoutePosition(dist);
            Vector3 p2 = GetRoutePosition(dist + 0.1f);
            Vector3 delta = p2 - p1;
            return new RoutePoint(p1, delta.normalized);
        }


        public Vector3 GetRoutePosition(float dist)
        {
            int point = 0;

            if (Length == 0)
            {
                Length = distances[distances.Length - 1];
            }

            dist = Mathf.Repeat(dist, Length);

            while (distances[point] < dist)
            {
                ++point;
            }


            // get nearest two points, ensuring points wrap-around start & end of circuit
            p1n = ((point - 1) + waypointList.items.Length) % waypointList.items.Length;
            p2n = point;

            // found point numbers, now find interpolation value between the two middle points

            i = Mathf.InverseLerp(distances[p1n], distances[p2n], dist);


                // get indices for the surrounding 2 points, because
                // four points are required by the catmull-rom function
                p0n = ((point - 2) + waypointList.items.Length) % waypointList.items.Length;
                p3n = (point + 1)% waypointList.items.Length;

                // 2nd point may have been the 'last' point - a dupe of the first,
                // (to give a value of max track distance instead of zero)
                // but now it must be wrapped back to zero if that was the case.
                p2n = p2n % waypointList.items.Length;

                P0 = points[p0n];
                P1 = points[p1n];
                P2 = points[p2n];
                P3 = points[p3n];

                return CatmullRom(P0, P1, P2, P3, i);
        }


        private Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float i)
        {
            return 0.5f*
                   ((2*p1) + (-p0 + p2)*i + (2*p0 - 5*p1 + 4*p2 - p3)*i*i +
                    (-p0 + 3*p1 - 3*p2 + p3)*i*i*i);
        }


    private void CachePositionsAndDistances()
    {
        // transfer the position of each point and distances between points to arrays for
        // speed of lookup at runtime
        points = new Vector3[waypointList.items.Length + 1];
        distances = new float[waypointList.items.Length + 1];

        float accumulateDistance = 0;
        for (int i = 0; i < points.Length; ++i)
        {
            var t1 = waypointList.items[(i)%waypointList.items.Length];
            var t2 = waypointList.items[(i + 1)%waypointList.items.Length];
            if (t1 != null && t2 != null)
            {
                Vector3 p1 = t1.position;
                Vector3 p2 = t2.position;
                points[i] = waypointList.items[i%waypointList.items.Length].position;
                distances[i] = accumulateDistance;
                accumulateDistance += (p1 - p2).magnitude;
            }
        }
    }

    
    private void OnDrawGizmos()
    {
        waypointList.circuit = this;
        if (waypointList.items.Length > 1)
        {
            CachePositionsAndDistances();
            Length = distances[distances.Length - 1];

            Gizmos.color = Color.yellow;
            Vector3 prev = waypointList.items[0].position;
            
            for (float dist = 0; dist < Length; dist += Length / 100)
            {
                Vector3 next = GetRoutePosition(dist + 1);
                Gizmos.DrawLine(prev, next);
                prev = next;
            }
            Gizmos.DrawLine(prev, waypointList.items[0].position);
        }
    }


    [Serializable]
    public class WaypointList
    {
        public WaypointCircuit circuit;
        public Transform[] items = new Transform[0];
    }

    public struct RoutePoint
    {
        public Vector3 position;
        public Vector3 direction;


        public RoutePoint(Vector3 position, Vector3 direction)
        {
            this.position = position;
            this.direction = direction;
        }
    }
}


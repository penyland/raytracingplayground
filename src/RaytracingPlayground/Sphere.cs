// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;

namespace RaytracingPlayground
{
    public class Sphere : Hittable
    {
        public Sphere(Vector3 center, float r, Material material)
        {
            this.Center = center;
            this.Radius = r;
            this.Material = material;
        }

        public Vector3 Center { get; set; }

        public float Radius { get; set; }

        public Material Material { get; }

        public override bool Hit(Ray ray, float t_min, float t_max, out HitRecord hitRecord)
        {
            hitRecord = default;

            Vector3 oc = ray.Origin - this.Center;
            float a = Vector3.Dot(ray.Direction, ray.Direction);
            float b = Vector3.Dot(oc, ray.Direction);
            float c = Vector3.Dot(oc, oc) - (this.Radius * this.Radius);
            float discriminant = (b * b) - (a * c);

            if (discriminant > 0)
            {
                float temp = (float)((-b - Math.Sqrt(discriminant)) / a);
                if (temp < t_max && temp > t_min)
                {
                    hitRecord.t = temp;
                    hitRecord.P = ray.PointAt(hitRecord.t);
                    hitRecord.Normal = (hitRecord.P - this.Center) / this.Radius;
                    hitRecord.Material = this.Material;
                    return true;
                }

                temp = (float)((-b + Math.Sqrt(discriminant)) / a);
                if (temp < t_max && temp > t_min)
                {
                    hitRecord.t = temp;
                    hitRecord.P = ray.PointAt(hitRecord.t);
                    hitRecord.Normal = (hitRecord.P - this.Center) / this.Radius;
                    hitRecord.Material = this.Material;
                    return true;
                }
            }

            return false;
        }
    }
}

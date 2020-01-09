// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace RaytracingPlayground
{
    internal class Metal : Material
    {
        private float fuzz;

        public Metal(Vector3 albedo, float fuzziness)
            : base(albedo)
        {
            this.fuzz = fuzziness < 1 ? fuzziness : 1;
        }

        public override bool Scatter(Ray ray, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered)
        {
            Vector3 reflected = this.Reflect(Vector3.Normalize(ray.Direction), hitRecord.Normal);
            scattered = new Ray(hitRecord.P, reflected + (this.fuzz * RandomUtils.InUnitSphere()));
            attenuation = this.Albedo;
            return Vector3.Dot(scattered.Direction, hitRecord.Normal) > 0;
        }

        public Vector3 Reflect(Vector3 v, Vector3 n)
        {
            return v - (2 * Vector3.Dot(v, n) * n);
        }
    }
}

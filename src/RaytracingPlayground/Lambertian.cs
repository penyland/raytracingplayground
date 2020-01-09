// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace RaytracingPlayground
{
    public class Lambertian : Material
    {
        public Lambertian(Vector3 albedo)
            : base(albedo)
        {
        }

        public override bool Scatter(Ray ray, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered)
        {
            Vector3 target = hitRecord.P + hitRecord.Normal + RandomUtils.InUnitSphere();
            scattered = new Ray(hitRecord.P, target - hitRecord.P);
            attenuation = this.Albedo;
            return true;
        }
    }
}

// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace RaytracingPlayground
{
    public abstract class Material
    {
        public Material(Vector3 albedo)
        {
            this.Albedo = albedo;
        }

        public Vector3 Albedo { get; }

        public abstract bool Scatter(Ray ray, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered);
    }
}

// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace RaytracingPlayground
{
    public abstract class Hittable : IHittable
    {
        public abstract bool Hit(Ray ray, float t_min, float t_max, ref HitRecord hitRecord);
    }
}

// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace RaytracingPlayground
{
    public interface IHittable
    {
        bool Hit(Ray ray, float t_min, float t_max, out HitRecord hitRecord);
    }
}

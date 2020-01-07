// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace RaytracingPlayground
{
    public class HittableList : IHittable
    {
        public HittableList()
        {
            this.Hittables = new List<IHittable>();
        }

        public List<IHittable> Hittables { get; }

        public bool Hit(Ray ray, float t_min, float t_max, ref HitRecord hitRecord)
        {
            HitRecord tempHitRecord = default;
            bool hit_anything = false;

            float closest_so_far = t_max;

            foreach (var hittable in this.Hittables)
            {
                if (hittable.Hit(ray, t_min, closest_so_far, ref tempHitRecord))
                {
                    hit_anything = true;
                    closest_so_far = tempHitRecord.t;
                    hitRecord = tempHitRecord;
                }
            }

            return hit_anything;
        }
    }
}

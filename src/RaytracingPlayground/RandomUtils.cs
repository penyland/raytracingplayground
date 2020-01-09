// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;

namespace RaytracingPlayground
{
    public static class RandomUtils
    {
        private static readonly Random Random = new Random();

        public static Vector3 InUnitSphere()
        {
            Vector3 p;
            do
            {
                p = (2.0f * new Vector3(
                    (float)Random.NextDouble(),
                    (float)Random.NextDouble(),
                    (float)Random.NextDouble())) - Vector3.One;
            }
            while (p.LengthSquared() >= 1.0f);

            return p;
        }
    }
}

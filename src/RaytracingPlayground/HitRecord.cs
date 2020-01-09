// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace RaytracingPlayground
{
    public struct HitRecord
    {
        public float t;

        public Vector3 P;

        public Vector3 Normal { get; set; }

        public Material Material { get; set; }
    }
}

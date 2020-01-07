// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace RaytracingPlayground
{
    public class Ray
    {
        private Vector3 origin;
        private Vector3 direction;

        public Ray(Vector3 a, Vector3 b)
        {
            this.origin = a;
            this.direction = b;
        }

        public Vector3 Origin => this.origin;

        public Vector3 Direction => this.direction;

        public Vector3 PointAt(float t) => this.origin + (t * this.direction);
    }
}

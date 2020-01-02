// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RaytracingPlayground
{
    public class Ray
    {
        public Ray(Vector3 a, Vector3 b)
        {
            this.A = a;
            this.B = b;
        }

        public Vector3 A { get; }

        public Vector3 B { get; }

        public Vector3 Origin => this.A;

        public Vector3 Direction => this.B;

        public Vector3 PointAtParameter(float t) => this.A + (t * this.B);
    }
}

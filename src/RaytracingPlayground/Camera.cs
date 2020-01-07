// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace RaytracingPlayground
{
    public class Camera
    {
        // Viewport
        private Vector3 lowerLeftCorner = new Vector3(-2.0f, -1.0f, -1.0f);
        private Vector3 horizontal = new Vector3(4.0f, 0.0f, 0.0f);
        private Vector3 vertical = new Vector3(0.0f, 2.0f, 0.0f);
        private Vector3 origin = new Vector3(0.0f, 0.0f, 0.0f);

        public Camera()
        {
        }

        public Ray GetRay(float u, float v)
        {
            return new Ray(this.origin, this.lowerLeftCorner + (u * this.horizontal) + (v * this.vertical));
        }
    }
}

// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace RaytracingPlayground
{
    public class FrameBuffer
    {
        public FrameBuffer(int width, int height, byte[] buffer)
        {
            this.Width = width;
            this.Height = height;

            this.Buffer = buffer;
        }

        public int Width { get; }

        public int Height { get; }

        public byte[] Buffer { get; }

        public int Length => this.Buffer.Length;
    }
}

// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;

namespace RaytracingPlayground
{
    public static class PPMWriter
    {
        public static void WriteBGR(Stream stream, in FrameBuffer buffer)
        {
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine("P3");
                writer.WriteLine($"{buffer.Width} {buffer.Height}");
                writer.WriteLine("255"); // maxcolor - BGR

                for (int i = 0; i < buffer.Length; i += 4)
                {
                    writer.Write(buffer.Buffer[i + 2]);  // R
                    writer.Write(" ");
                    writer.Write(buffer.Buffer[i + 1]);  // G
                    writer.Write(" ");
                    writer.Write(buffer.Buffer[i]);      // B
                    writer.Write("\n");
                }
            }
        }

        public static void WriteRGB(Stream stream, in FrameBuffer buffer)
        {
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine("P3");
                writer.WriteLine($"{buffer.Width} {buffer.Height}");
                writer.WriteLine("255"); // maxcolor - BGR

                for (int i = 0; i < buffer.Length; i += 3)
                {
                    writer.Write(buffer.Buffer[i]);  // R
                    writer.Write(" ");
                    writer.Write(buffer.Buffer[i + 1]);  // G
                    writer.Write(" ");
                    writer.Write(buffer.Buffer[i + 2]);      // B
                    writer.Write("\n");
                }
            }
        }
    }
}

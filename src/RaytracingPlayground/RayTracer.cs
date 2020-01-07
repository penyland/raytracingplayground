// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Graphics.Canvas;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace RaytracingPlayground
{
    public class RayTracer
    {
        private static readonly Random Random = new Random();

        private readonly CanvasRenderTarget renderTarget;

        public RayTracer(float width, float height, int dpi)
        {
            var canvasDevice = CanvasDevice.GetSharedDevice();
            this.renderTarget = new CanvasRenderTarget(
                canvasDevice,
                width,
                height,
                dpi,
                Windows.Graphics.DirectX.DirectXPixelFormat.B8G8R8A8UIntNormalized,
                CanvasAlphaMode.Premultiplied);
        }

        public CanvasRenderTarget RenderTarget => this.renderTarget;

        public async Task<CanvasRenderTarget> RenderAsync(World world)
        {
            var camera = new Camera();

            int width = (int)this.RenderTarget.SizeInPixels.Width;
            int height = (int)this.RenderTarget.SizeInPixels.Height;
            int ns = 10;

            byte[] buffer = this.renderTarget.GetPixelBytes();
            int index = 0;

            await Task.Run(() =>
            {
                for (int j = height - 1; j >= 0; j--)
                {
                    for (int i = 0; i < width; i++)
                    {
                        Vector3 color = Vector3.Zero;
                        for (int s = 0; s < ns; s++)
                        {
                            float u = (float)(i + Random.NextDouble()) / width;
                            float v = (float)(j + Random.NextDouble()) / height;

                            Ray ray = camera.GetRay(u, v);
                            color += this.ComputeColor(ray, world.Items);
                        }

                        color /= ns;

                        // var color = new Vector3((float)i / width, (float)j / height, 0.2f);
                        buffer[index + 0] = (byte)(color.Z * 0xFF);
                        buffer[index + 1] = (byte)(color.Y * 0xFF);
                        buffer[index + 2] = (byte)(color.X * 0xFF);
                        buffer[index + 3] = 1;

                        index += 4;
                    }
                }
            });

            this.renderTarget.SetPixelBytes(buffer);

            return this.renderTarget;
        }

        private Vector3 ComputeColor(Ray ray, IHittable world)
        {
            HitRecord hit = default;
            if (world.Hit(ray, 0.0f, float.MaxValue, ref hit))
            {
                return 0.5f * new Vector3(hit.Normal.X + 1.0f, hit.Normal.Y + 1.0f, hit.Normal.Z + 1.0f);
            }
            else
            {
                // Ray hits background
                return this.ComputeBackgroundColor(ray);
            }
        }

        private Vector3 ComputeBackgroundColor(Ray ray)
        {
            float t = 0.5f * (Vector3.Normalize(ray.Direction).Y + 1.0f);
            return Vector3.Lerp(Vector3.One, new Vector3(0.5f, 0.7f, 1.0f), t);
        }
    }
}

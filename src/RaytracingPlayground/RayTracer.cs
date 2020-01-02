// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Windows.UI;

namespace RaytracingPlayground
{
    public class RayTracer
    {
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

        public async Task<CanvasRenderTarget> RenderAsync()
        {
            int width = (int)this.RenderTarget.SizeInPixels.Width;
            int height = (int)this.RenderTarget.SizeInPixels.Height;

            using (CanvasDrawingSession ds = this.renderTarget.CreateDrawingSession())
            {
                ds.Clear(Color.FromArgb(0xff, 0xee, 0xaa, 0x44));
            }

            byte[] buffer = this.renderTarget.GetPixelBytes();

            for (int j = 0; j < height; ++j)
            {
                int total_row_len = j * width;
                for (uint i = 0; i < width; ++i)
                {
                    long index = (total_row_len + i) * 4;

                    var color = new Vector3((float)i / width, (float)j / height, 0.2f);

                    buffer[index + 0] = (byte)(color.Z * 0xFF);
                    buffer[index + 1] = (byte)(color.Y * 0xFF);
                    buffer[index + 2] = (byte)(color.X * 0xFF);
                    buffer[index + 3] = 1;
                }
            }

            this.renderTarget.SetPixelBytes(buffer);

            return this.renderTarget;
        }

        public CanvasRenderTarget RenderBackground()
        {
            int width = (int)this.RenderTarget.SizeInPixels.Width;
            int height = (int)this.RenderTarget.SizeInPixels.Height;

            byte[] buffer = this.renderTarget.GetPixelBytes();

            Vector3 lower_left_corner = new Vector3(-2.0f, -1.0f, -1.0f);
            Vector3 horizontal = new Vector3(4.0f, 0.0f, 0.0f);
            Vector3 vertical = new Vector3(0.0f, 2.0f, 0.0f);
            Vector3 origin = new Vector3(0.0f, 0.0f, 0.0f);

            for (int j = height - 1; j >= 0; j--)
            {
                int total_row_len = j * width;
                for (uint i = 0; i < width; ++i)
                {
                    long index = (total_row_len + i) * 4;

                    float u = (float)i / width;
                    float v = (float)j / height;
                    Ray ray = new Ray(origin, lower_left_corner + (u * horizontal) + (v * vertical));
                    Vector3 color = this.InterpolateColor(ray);

                    buffer[index + 0] = (byte)(color.Z * 0xFF);
                    buffer[index + 1] = (byte)(color.Y * 0xFF);
                    buffer[index + 2] = (byte)(color.X * 0xFF);
                    buffer[index + 3] = 1;
                }
            }

            this.renderTarget.SetPixelBytes(buffer);

            return this.renderTarget;
        }

        public CanvasRenderTarget RenderSphere()
        {
            int width = (int)this.RenderTarget.SizeInPixels.Width;
            int height = (int)this.RenderTarget.SizeInPixels.Height;

            byte[] buffer = this.renderTarget.GetPixelBytes();

            Vector3 lower_left_corner = new Vector3(-2.0f, -1.0f, -1.0f);
            Vector3 horizontal = new Vector3(4.0f, 0.0f, 0.0f);
            Vector3 vertical = new Vector3(0.0f, 2.0f, 0.0f);
            Vector3 origin = new Vector3(0.0f, 0.0f, 0.0f);

            for (int j = height - 1; j >= 0; j--)
            {
                int total_row_len = j * width;
                for (uint i = 0; i < width; ++i)
                {
                    long index = (total_row_len + i) * 4;

                    float u = (float)i / width;
                    float v = (float)j / height;
                    Ray ray = new Ray(origin, lower_left_corner + (u * horizontal) + (v * vertical));
                    Vector3 color = this.RenderRay(ray);

                    buffer[index + 0] = (byte)(color.Z * 0xFF);
                    buffer[index + 1] = (byte)(color.Y * 0xFF);
                    buffer[index + 2] = (byte)(color.X * 0xFF);
                    buffer[index + 3] = 1;
                }
            }

            this.renderTarget.SetPixelBytes(buffer);

            return this.renderTarget;
        }

        private Vector3 RenderRay(Ray ray)
        {
            if (this.HitSphere(new Vector3(0, 0, -1), 0.5f, ray))
            {
                return new Vector3(1, 0, 0);
            }

            var unit_direction = Vector3.Normalize(ray.Direction);
            float t = 0.5f * (unit_direction.Y + 1.0f);
            return ((1.0f - t) * new Vector3(1.0f, 1.0f, 1.0f)) + (t * new Vector3(0.5f, 0.7f, 1.0f));
        }

        private Vector3 InterpolateColor(Ray ray)
        {
            var unit_direction = Vector3.Normalize(ray.Direction);
            float t = 0.5f * (unit_direction.Y + 1.0f);
            return ((1.0f - t) * new Vector3(1.0f, 1.0f, 1.0f)) + (t * new Vector3(0.5f, 0.7f, 1.0f));
        }

        private bool HitSphere(Vector3 center, float radius, Ray ray)
        {
            Vector3 oc = ray.Origin - center;
            float a = Vector3.Dot(ray.Direction, ray.Direction);
            float b = 2.0f * Vector3.Dot(oc, ray.Direction);
            float c = Vector3.Dot(oc, oc) - (radius * radius);
            float discriminant = (b * b) - (4 * a * c);
            return discriminant > 0;
        }
    }
}

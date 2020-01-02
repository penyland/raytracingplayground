// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Windows.UI;

namespace RaytracingPlayground
{
    public class RayTracer
    {
        private CanvasRenderTarget renderTarget;

        public RayTracer(float width, float height, int dpi)
        {
            CanvasDevice canvasDevice = CanvasDevice.GetSharedDevice();
            this.renderTarget = new CanvasRenderTarget(
                canvasDevice,
                width,
                height,
                dpi,
                Windows.Graphics.DirectX.DirectXPixelFormat.B8G8R8A8UIntNormalized,
                CanvasAlphaMode.Premultiplied);
        }

        public CanvasRenderTarget RenderTarget { get => this.renderTarget; }

        public async Task<CanvasRenderTarget> RenderAsync()
        {
            int width = (int)this.RenderTarget.SizeInPixels.Width;
            int height = (int)this.RenderTarget.SizeInPixels.Height;

            using (var ds = this.renderTarget.CreateDrawingSession())
            {
                ds.Clear(Color.FromArgb(0xff, 0xee, 0xaa, 0x44));
            }

            byte[] buffer = this.renderTarget.GetPixelBytes();

            for (int j = 0; j < height; ++j)
            {
                var total_row_len = j * width;
                for (uint i = 0; i < width; ++i)
                {
                    var index = (total_row_len + i) * 4;

                    float r = (float)i / width;
                    float g = (float)j / height;
                    float b = (float)0.2f;

                    buffer[index + 0] = (byte)(b * 0xFF);
                    buffer[index + 1] = (byte)(g * 0xFF);
                    buffer[index + 2] = (byte)(r * 0xFF);
                    buffer[index + 3] = 1;
                }
            }

            this.renderTarget.SetPixelBytes(buffer);

            return this.renderTarget;
        }
    }
}

// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RaytracingPlayground
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private RayTracer rayTracer;
        private Microsoft.Graphics.Canvas.CanvasRenderTarget renderTarget;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void btnRender_Click(object sender, RoutedEventArgs e)
        {
            this.btnSave.IsEnabled = false;
            this.btnRender.IsEnabled = false;

            if (this.rayTracer == null)
            {
                this.rayTracer = new RayTracer((float)this.canvas.ActualWidth, (float)this.canvas.ActualHeight, (int)this.canvas.Dpi);
            }

            // Setup world and items
            var world = new World();
            world.Add(new Sphere(new Vector3(0, 0, -1), 0.5f));
            world.Add(new Sphere(new Vector3(0, -100.5f, -1), 100f));

            this.renderTarget = await this.rayTracer.RenderAsync(
                world,
                new Progress<RenderProgress>(p =>
                {
                    this.progress.Maximum = p.Total;
                    this.progress.Value = p.Current;
                }));

            this.canvas.Invalidate();

            this.btnSave.IsEnabled = true;
            this.btnRender.IsEnabled = true;
        }

        private void canvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            if (this.renderTarget == null)
            {
                args.DrawingSession.Clear(Colors.CornflowerBlue);
                args.DrawingSession.DrawText("Hello, World!", 100, 100, Colors.Black);
                args.DrawingSession.DrawCircle(125, 125, 100, Colors.Green);
                args.DrawingSession.DrawLine(0, 0, 50, 200, Colors.Red);
            }
            else
            {
                args.DrawingSession.DrawImage(this.renderTarget);
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            this.canvas.RemoveFromVisualTree();
            this.canvas = null;
        }

        private async void btnTest_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder folder = KnownFolders.PicturesLibrary;
            var file = await folder.CreateFileAsync("raytracing.ppm", CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenStreamForWriteAsync())
            {
                var frameBuffer = new FrameBuffer((int)this.canvas.ActualWidth, (int)this.canvas.ActualHeight, this.renderTarget.GetPixelBytes());

                PPMWriter.WriteBGR(stream, frameBuffer);
            }
        }

        private static async Task SaveFile(string name, FrameBuffer frameBuffer)
        {
            StorageFolder folder = KnownFolders.PicturesLibrary;
            var file = await folder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenStreamForWriteAsync())
            {
                PPMWriter.WriteRGB(stream, frameBuffer);
            }
        }
    }
}

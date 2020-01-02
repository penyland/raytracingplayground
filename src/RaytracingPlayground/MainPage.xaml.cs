// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
            if (this.rayTracer == null)
            {
                this.rayTracer = new RayTracer((float)this.canvas.ActualWidth, (float)this.canvas.ActualHeight, (int)this.canvas.Dpi);
            }

            //this.renderTarget = await this.rayTracer.RenderAsync();
            //this.renderTarget = this.rayTracer.RenderBackground();
            this.renderTarget = this.rayTracer.RenderSphere();

            this.canvas.Invalidate();
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
    }
}

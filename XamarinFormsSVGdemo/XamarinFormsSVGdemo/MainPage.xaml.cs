using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace XamarinFormsSVGdemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SKCanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            if (!(sender is SKCanvasView container))
            {
                return;
            }

            SKCanvas canvas = e.Surface.Canvas;
            canvas.Clear();

            var scale = (float)(e.Info.Width / container.Width);
#if DEBUG
#if !WINDOWS_UWP
            Console.WriteLine("Scale = {0}", scale);
#else
			System.Diagnostics.Debug.WriteLine("Scale = {0}", scale);
#endif
#endif
            canvas.Scale(scale);
            var centerX = (float)container.Width / 2;
            var centerY = (float)container.Height / 2;

            canvas.Translate(centerX, centerY);

            using (var stream = GetType().Assembly.GetManifestResourceStream(@"XamarinFormsSVGdemo.black_cat.svg"))
            using(var paint = new SKPaint())
            {
                var svg = new SkiaSharp.Extended.Svg.SKSvg();
                svg.Load(stream);

                SKRect bounds = svg.ViewBox;
                
                canvas.Save();
                
                canvas.Translate(-bounds.MidX, -bounds.MidY);
                canvas.DrawPicture(svg.Picture, paint);

                canvas.Restore();
            }

        }
    }
}

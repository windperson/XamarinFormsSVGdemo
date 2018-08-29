using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace XamarinFormsSVGdemo
{
    public partial class MainPage : ContentPage
    {
        private const string SvgStr =
            @"<svg height='300' width='300'>
            <rect y='0' x='0' height='200' width='100' fill='#ff0000' />
            <circle cx='150' cy='150' r='40' fill='#00ff00' stroke-width='3' stroke='#ffff00' />
            </svg>";

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

            var centerX = (float) container.Width / 2;
            var centerY = (float) container.Height / 2;
            canvas.Translate(centerX, centerY);

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(SvgStr));
            var svg = new SkiaSharp.Extended.Svg.SKSvg();
            svg.Load(stream);
            
            using (var paint = new SKPaint()) //Note: be sure to create & use SKPaint object
            {
                canvas.DrawPicture(svg.Picture, 0, 0, paint);
            }
        }
    }
}

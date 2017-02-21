using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using IA;
using IA.iOS;


[assembly: ExportRenderer(typeof(EditorGrows), typeof(EditorGrowsRenderer))]
namespace IA.iOS
{
	public class EditorGrowsRenderer : EditorRenderer
	{
		public EditorGrowsRenderer() : base()
		{

		}

		protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
		{
			base.OnElementChanged(e);
			if (Control != null)
			{
				Control.ScrollEnabled = false;

				var layer = Control.Layer;
				layer.BorderColor = Color.FromHex("#CCCCCC").ToCGColor();
				layer.BorderWidth = 0.5f;
				layer.CornerRadius = 5;
			}
		}

	}
}
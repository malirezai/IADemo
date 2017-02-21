using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using IA;
using IA.Droid;
using Android.Graphics;
using Android.Graphics.Drawables;


[assembly: ExportRenderer(typeof(EditorGrows), typeof(EditorGrowsRenderer))]
namespace IA.Droid
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
				Control.VerticalScrollBarEnabled = false;
				Control.NestedScrollingEnabled = false;

				var layer = new GradientDrawable();
				layer.SetCornerRadius(10);
				layer.SetStroke(4, Android.Graphics.Color.ParseColor("#AAAAAA"));

				Control.SetBackground(layer);

			}
		}

	}
}
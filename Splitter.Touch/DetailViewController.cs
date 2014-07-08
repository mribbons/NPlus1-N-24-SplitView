using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.CoreFoundation;

namespace Splitter.Touch
{
	public class DetailViewController : UIViewController
	{
		UILabel label;
		public DetailViewController () : base()
		{
			View.BackgroundColor = UIColor.White;
			label = new UILabel(new RectangleF(100,100,300,50));
			label.Text = "This is the detail view";
			View.AddSubview (label);
		}
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}
}


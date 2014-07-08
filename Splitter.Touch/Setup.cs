using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Platform;
using Splitter.Core.ViewModels;
using Cirrious.CrossCore.Platform;
using System;
using Splitter.Touch.Views;

namespace Splitter.Touch
{
	public class Setup : MvxTouchSetup
	{
	    private UIWindow _window;

		public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
		{
		    _window = window;
		}

		protected override Cirrious.MvvmCross.ViewModels.IMvxApplication CreateApp ()
		{
			return new Core.App();
		}

        protected override IMvxTouchViewPresenter CreatePresenter()
        {
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
                return new SplitPresenter(_window);

            return base.CreatePresenter();
        }
	}

    public class SplitPresenter : MvxBaseTouchViewPresenter
    {
        private SplitViewController _svc;
		private UIWindow _window;
		private UIViewController left, right;

        public SplitPresenter(UIWindow window)
        {
			this.left = new UIViewController ();
			this.right = new UIViewController ();
			_svc = new SplitViewController(left, right);
			_svc.Delegate = new SplitViewControllerDelegate ();
            window.RootViewController = _svc;
			_window = window;
        }

        public override void Show(Cirrious.MvvmCross.ViewModels.MvxViewModelRequest request)
        {
            var viewController = (UIViewController)Mvx.Resolve<IMvxTouchViewCreator>().CreateView(request);

			if (request.ViewModelType == typeof(FirstViewModel))
				left = viewController;
			else
                //_svc.SetRight(viewController);
				right = viewController;


			_window.RootViewController = new SplitViewController ( left, right );
        }



    }

    public class SplitViewController : UISplitViewController
    {
        public SplitViewController()
        {
            this.ViewControllers = new UIViewController[]
                {
				new UIViewController(), 
					new UIViewController(), 
                };
				
		} 

		public SplitViewController(UIViewController left, UIViewController right)
		{
			this.ViewControllers = new UIViewController[] { left, right };
		}

        public void SetLeft(UIViewController left)
        {
            /*this.ViewControllers = new UIViewController[]
                {
                    left,
                    this.ViewControllers[1]
                };*/

			this.AddChildViewController (left);
        }

        public void SetRight(UIViewController right)
        {
            /*this.ViewControllers = new UIViewController[]
                {
                    this.ViewControllers[0],
                    right,
                };*/

			this.AddChildViewController (right);
        }

		public override void ViewWillLayoutSubviews ()
		{
			MvxTrace.Trace("ViewWillLayoutSubviews: " + this.ViewControllers.Length);

			base.ViewWillLayoutSubviews ();

			foreach (UIViewController vc in this.ViewControllers)
				MvxTrace.Trace ("View: " + vc.GetType ());
		}


    }


	class SplitViewControllerDelegate : UISplitViewControllerDelegate
	{
		public override bool ShouldHideViewController(
			UISplitViewController svc,
			UIViewController viewController,
			UIInterfaceOrientation inOrientation)
		{
			MvxTrace.Trace ("ShouldHideViewController");
			return true;
		}
	}
}
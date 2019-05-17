using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using LoadingFormsViewDemo;
using LoadingFormsViewDemo.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
namespace LoadingFormsViewDemo.iOS
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.TouchUpInside += Control_TouchUpInside;
            }
        }

        private void Control_TouchUpInside(object sender, EventArgs e)
        {
            UIViewController vc = new UIViewController();
            vc.View.BackgroundColor = UIColor.White;
            vc.View.AddSubview(CreateCustomView(new FormsView(), new CGRect(50, 50, 200, 200)));

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(vc, true, null);
        }

        public UIView CreateCustomView(View customView, CGRect size)
        {
            var renderer = Platform.CreateRenderer(customView);

            renderer.NativeView.Frame = size;

            renderer.NativeView.AutoresizingMask = UIViewAutoresizing.All;
            renderer.NativeView.ContentMode = UIViewContentMode.ScaleToFill;

            renderer.Element.Layout(size.ToRectangle());

            var nativeView = renderer.NativeView;

            nativeView.SetNeedsLayout();

            return nativeView;
        }
    }
}
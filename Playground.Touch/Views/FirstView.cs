using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace Playground.Touch.Views
{
    public partial class FirstView : MvxViewController
    {
        public FirstView()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var myButton = UIButton.FromType(UIButtonType.System);
            myButton.SetTitle("click", UIControlState.Normal);
            Add(myButton);

            var set = this.CreateBindingSet<FirstView, Core.ViewModels.FirstViewModel>();
            set.Bind(myButton).To(vm => vm.ShowListCommand);
            set.Apply();

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            var buttonHeight = 44;
            View.AddConstraints(
                myButton.AtTopOf(View, 100),
                myButton.WithSameCenterX(View),
                myButton.Height().EqualTo(buttonHeight),
                myButton.WithSameWidth(View)
                );
        }
    }
}

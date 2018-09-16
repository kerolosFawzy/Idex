using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Foundation;
using IDEX.iOS.Effects;

[assembly: ResolutionGroupName("IDEX")]
[assembly: ExportEffect(typeof(CapitalizeKeyboardEffect), nameof(CapitalizeKeyboardEffect))]
namespace IDEX.iOS.Effects
{
    [Preserve(AllMembers = true)]
    public class CapitalizeKeyboardEffect : PlatformEffect
    {
        UITextAutocapitalizationType old;

        protected override void OnAttached()
        {
            if (Control is UITextField editText)
            {
                old = editText.AutocapitalizationType;
                editText.AutocapitalizationType = UITextAutocapitalizationType.AllCharacters;
            }
        }

        protected override void OnDetached()
        {
            if (Control is UITextField editText)
                editText.AutocapitalizationType = old;
        }
    }
}
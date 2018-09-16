using Android.Text;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Runtime;
using System.Linq;
using IDEX.Droid.Effects;
[assembly: ResolutionGroupName("IDEX")]
[assembly: ExportEffect(typeof(CapitalizeKeyboardEffect), nameof(CapitalizeKeyboardEffect))]
namespace IDEX.Droid.Effects
{
    [Preserve(AllMembers = true)]
    public class CapitalizeKeyboardEffect : PlatformEffect
    {
        InputTypes old;
        IInputFilter[] oldFilters;

        protected override void OnAttached()
        {
            if (Control is EditText editText)
            {
                old = editText.InputType;
                oldFilters = editText.GetFilters().ToArray();

                editText.SetRawInputType(InputTypes.ClassText | InputTypes.TextFlagCapCharacters);

                var newFilters = oldFilters.ToList();
                newFilters.Add(new InputFilterAllCaps());
                editText.SetFilters(newFilters.ToArray());
            }
        }

        protected override void OnDetached()
        {
            if (Control is EditText editText)
            {
                editText.SetRawInputType(old);
                editText.SetFilters(oldFilters);
            }
        }
    }
}
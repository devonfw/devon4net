using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;

namespace MyThaiStar.Droid.Renderers
{
    public class TagEntryRenderer : EntryRenderer
    {
        public static void Init()
        {
            new TagEntryRenderer();
        }

        public TagEntryRenderer()
        {
        }

        public TagEntryRenderer(Context context) : base()
        {
        }

        public TagEntryRenderer(Context context, Android.Util.IAttributeSet attrs) : base()
        {
        }

        public TagEntryRenderer(Context context, Android.Util.IAttributeSet attrs, int defStyle) : base()
        {
        }
        public TagEntryRenderer(IntPtr a, JniHandleOwnership b) : base()
        {
        }
    }
}
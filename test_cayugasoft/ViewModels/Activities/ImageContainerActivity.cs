using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace test_cayugasoft.ViewModels.Activities
{
    [Activity(Label = "Image Container")]
    public class ImageContainerActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ImageContainer);

            //Get image to manipulate its properties
            ImageView imageContainerItem = FindViewById<ImageView>(Resource.Id.ImageContainerItem);

            //rotate 90 clockwise
            Button rotateBtn = FindViewById<Button>(Resource.Id.RotateBtn);
            imageContainerItem.Rotation = 0;
            rotateBtn.Click += delegate
            {
                imageContainerItem.Rotation += 90;
                imageContainerItem.Rotation = (imageContainerItem.Rotation >= 360) ? 0 : imageContainerItem.Rotation;
            };

            //flip horizontal
            Button flipHorizontalBtn = FindViewById<Button>(Resource.Id.FlipHorizontalBtn);
            flipHorizontalBtn.Click += delegate
            {
                imageContainerItem.Rotation = 0;
                imageContainerItem.Rotation = 90;
            };

            //flip vertical
            Button flipVerticalBtn = FindViewById<Button>(Resource.Id.FlipVerticalBtn);
            flipVerticalBtn.Click += delegate
            {
                imageContainerItem.Rotation = 0;
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Locations;
using Android.Net;
using Android.OS;
using Android.Provider;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Exception = System.Exception;
using Uri = System.Uri;


namespace test_cayugasoft.ViewModels.Activities
{
    [Activity(Label = "Gallery", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation)]
    public class GalleryActivity : Activity
    {
        const string kittyJpgUrl = "http://thecatapi.com/api/images/get?format=src&type=jpg";

        WebClient webClient;
        Button downloadButton;
        LinearLayout galleryContentLl;
        CancellationTokenSource cts;
        ScrollView gallerySv;
        LinearLayout downloadLl;
        RelativeLayout bigImageRl;
        ImageView bigImageIv;
        Button closeGalleryItemBtn;
        Button editGalleryItemBtn;
        Button saveGalleryItemBtn;
        Button deleteGalleryItemBtn;
        View tempDeleteView;
        byte[] tempSaveImageBytes;
        public static int GellerySize { get; set; }

        Matrix matrix = new Matrix();
        Matrix savedMatrix = new Matrix();
        private const int NONE = 0;
        private const int DRAG = 1;
        private const int ZOOM = 2;
        private int mode = NONE;
        private PointF start = new PointF();
        private PointF mid = new PointF();
        private float oldDist = 1f;
        private float d = 0f;
        private float newRot = 0f;
        private float[] lastEvent = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Gallery);
            this.galleryContentLl = FindViewById<LinearLayout>(Resource.Id.GalleryLl);
            this.downloadButton = FindViewById<Button>(Resource.Id.DownloadButton);
            this.closeGalleryItemBtn = FindViewById<Button>(Resource.Id.CloseGalleryItemBtn);
            this.editGalleryItemBtn = FindViewById<Button>(Resource.Id.EditGalleryItemBtn);
            this.saveGalleryItemBtn = FindViewById<Button>(Resource.Id.SaveGalleryItemBtn);
            this.deleteGalleryItemBtn = FindViewById<Button>(Resource.Id.DeleteGalleryItemBtn);
            this.gallerySv = FindViewById<ScrollView>(Resource.Id.GallerySv);
            this.downloadLl = FindViewById<LinearLayout>(Resource.Id.DownloadLl);
            this.bigImageRl = FindViewById<RelativeLayout>(Resource.Id.BigImageRl);
            this.bigImageIv = FindViewById<ImageView>(Resource.Id.BigImageIv);

            closeGalleryItemBtn.Click += delegate
            {
                bigImageRl.Visibility = ViewStates.Invisible;
                gallerySv.Alpha = 1;
                downloadLl.Visibility = ViewStates.Visible;
            };
            editGalleryItemBtn.Click += delegate
            {
                editGalleryItemBtn.Visibility = ViewStates.Invisible;
                saveGalleryItemBtn.Visibility = ViewStates.Visible;
                deleteGalleryItemBtn.Visibility = ViewStates.Visible;
            };
            deleteGalleryItemBtn.Click += delegate
            {
                galleryContentLl.RemoveView(tempDeleteView);
                bigImageRl.Visibility = ViewStates.Invisible;
                gallerySv.Alpha = 1;
                downloadLl.Visibility = ViewStates.Visible;
            };
            saveGalleryItemBtn.Click += delegate
            {
                Java.IO.File rootsd = Android.OS.Environment.ExternalStorageDirectory;
                string documentsPath = rootsd.AbsolutePath + "/" + Android.OS.Environment.DirectoryDcim;
                string localFilename = "downloaded.png";
                string localPath = System.IO.Path.Combine(documentsPath, localFilename);

                //Save the Image using write
                FileStream fs = new FileStream(localPath, FileMode.OpenOrCreate);
                fs.Write(tempSaveImageBytes, 0, tempSaveImageBytes.Length);

                Console.WriteLine("localPath:" + localPath);
                fs.Close();
            };

            GellerySize = 50;
            downloadButton.Click += DownloadAsync;

            bigImageIv.Touch += ImageViewTestOnTouch;
        }

        async void DownloadAsync(object sender, System.EventArgs ea)
        {
            if (IsNetworkConnected())
            {
                this.galleryContentLl.RemoveAllViews();
                this.downloadButton.Text = "Cancel";
                this.downloadButton.Click -= DownloadAsync;
                this.downloadButton.Click += CancelDownload;
                cts = new CancellationTokenSource();
                try
                {
                    for (int i = 0; i < GellerySize; i++)
                    {
                        CancellationToken ct = cts.Token;
                        ct.ThrowIfCancellationRequested();
                        bool isLoad = await DownloadImage(ct);
                        Console.WriteLine("Loaded:" + isLoad);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                this.downloadButton.Click -= CancelDownload;
                this.downloadButton.Click += DownloadAsync;
                this.downloadButton.Text = "Refresh";
            }
            else
            {
                Toast.MakeText(this, "There no availabel internet connection.", ToastLength.Short).Show();
            }
        }

        async Task<bool> DownloadImage(CancellationToken token)
        {
            Uri url = null;
            byte[] bytes = null;
            RelativeLayout rl = null;
            RelativeLayout.LayoutParams layoutParams = null;
            ImageButton iv = null;
            ProgressBar pb = null;

            try
            {
                webClient = new WebClient();
                url = new Uri(kittyJpgUrl);
                rl = new RelativeLayout(this);
                rl.SetPadding(0, 10, 0, 10);
                rl.LayoutParameters = new RelativeLayout.LayoutParams(200, 200);
                iv = new ImageButton(this);
                iv.LayoutParameters = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent);
                pb = new ProgressBar(this);
                pb.LayoutParameters = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent);
                rl.AddView(iv);
                rl.AddView(pb);
                this.downloadButton.Tag = rl;
                galleryContentLl.AddView(rl);

                bytes = await webClient.DownloadDataTaskAsync(url);
                iv.Tag = bytes;

                BitmapFactory.Options options = new BitmapFactory.Options();
                options.InJustDecodeBounds = true;
                await BitmapFactory.DecodeByteArrayAsync(bytes, 0, bytes.Length, options);

                options.InSampleSize = options.OutWidth > options.OutHeight
                    ? options.OutHeight / iv.Height
                    : options.OutWidth / iv.Width;
                options.InJustDecodeBounds = false;

                Bitmap bitmap = await BitmapFactory.DecodeByteArrayAsync(bytes, 0, bytes.Length, options);

                pb.Visibility = ViewStates.Invisible;
                iv.SetImageBitmap(bitmap);
                iv.Click += LoadBigImage;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.galleryContentLl.RemoveView(rl);
                return false;
            }
            return true;
        }

        async private void LoadBigImage(object sender, System.EventArgs ea)
        {
            ImageButton iv = (ImageButton) sender;
            byte[] bytes = null;
            tempDeleteView = null;
            tempSaveImageBytes = null;
            bytes = (byte[])iv.Tag;
            gallerySv.Alpha = 0.3f;
            downloadLl.Visibility = ViewStates.Invisible;
            bigImageRl.Visibility = ViewStates.Visible;
            editGalleryItemBtn.Visibility = ViewStates.Visible;
            saveGalleryItemBtn.Visibility = ViewStates.Invisible;
            deleteGalleryItemBtn.Visibility = ViewStates.Invisible;
            tempDeleteView = (View) iv.Parent;
            tempSaveImageBytes = bytes;
           
            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InJustDecodeBounds = true;
            await BitmapFactory.DecodeByteArrayAsync(bytes, 0, bytes.Length, options);

            options.InSampleSize = options.OutWidth > options.OutHeight
                ? options.OutHeight / iv.Height
                : options.OutWidth / iv.Width;
            options.InJustDecodeBounds = false;

            Bitmap bitmap = await BitmapFactory.DecodeByteArrayAsync(bytes, 0, bytes.Length, options);

            bigImageIv.SetImageBitmap(bitmap);
            
            //get display width and height
            Display display = WindowManager.DefaultDisplay;
            int width = display.Width;
            int height = display.Height;
            matrix.Reset();
            matrix.PostTranslate(((float)width - bitmap.Width) / 2, ((float)height - bitmap.Height) / 2);
            bigImageIv.ImageMatrix = matrix;
        }

        void CancelDownload(object sender, System.EventArgs ea)
        {

            if ((View)((Button)sender).Tag != null)
                galleryContentLl.RemoveView((View)((Button)sender).Tag);

            if (webClient != null)
                webClient.CancelAsync();

            cts.Cancel();
        }

        bool IsNetworkConnected()
        {
            ConnectivityManager cm = (ConnectivityManager)GetSystemService(Context.ConnectivityService);
            NetworkInfo ni = cm.ActiveNetworkInfo;
            if (ni == null)
            {
                // There are no active networks.
                return false;
            }
            else
                return true;
        }

        #region Multi Touch
        private void ImageViewTestOnTouch(object sender, View.TouchEventArgs tea)
        {
            ImageView view = (ImageView)sender;
            switch (tea.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    savedMatrix.Set(matrix);
                    start.Set(tea.Event.GetX(), tea.Event.GetY());
                    mode = DRAG;
                    lastEvent = null;
                    break;
                case MotionEventActions.PointerDown:
                    oldDist = Spacing(tea.Event);
                    if (oldDist > 10f)
                    {
                        savedMatrix.Set(matrix);
                        MidPoint(mid, tea.Event);
                        mode = ZOOM;
                    }
                    lastEvent = new float[4];
                    lastEvent[0] = tea.Event.GetX(0);
                    lastEvent[1] = tea.Event.GetX(1);
                    lastEvent[2] = tea.Event.GetY(0);
                    lastEvent[3] = tea.Event.GetY(1);
                    d = Rotation(tea.Event);
                    break;
                case MotionEventActions.Up:
                case MotionEventActions.PointerUp:
                    mode = NONE;
                    lastEvent = null;
                    break;
                case MotionEventActions.Move:
                    if (mode == DRAG)
                    {
                        matrix.Set(savedMatrix);
                        float dx = tea.Event.GetX(0) - start.X;
                        float dy = tea.Event.GetY(0) - start.Y;
                        matrix.PostTranslate(dx, dy);
                    }
                    else
                    {
                        if (mode == ZOOM)
                        {
                            float newDist = Spacing(tea.Event);
                            if (newDist > 10f)
                            {
                                matrix.Set(savedMatrix);
                                float scale = (newDist / oldDist);
                                matrix.PostScale(scale, scale, mid.X, mid.Y);
                            }
                            if (lastEvent != null && tea.Event.PointerCount == 3)
                            {
                                newRot = Rotation(tea.Event);
                                float r = newRot - d;
                                float[] values = new float[9];
                                matrix.GetValues(values);
                                float tx = values[2];
                                float ty = values[5];
                                float sx = values[0];
                                float xc = ((float)view.Width / 2) * sx;
                                float yc = ((float)view.Height / 2) * sx;
                                matrix.PostRotate(r, tx + xc, ty + yc);
                            }
                        }
                    }
                    break;
            }

            view.ImageMatrix = matrix;
        }

        private float Spacing(MotionEvent me)
        {
            float x = me.GetX(0) - me.GetX(1);
            float y = me.GetY(0) - me.GetY(1);
            return (float)(System.Math.Sqrt(x * x + y * y));
        }

        private void MidPoint(PointF point, MotionEvent me)
        {
            float x = me.GetX(0) + me.GetX(1);
            float y = me.GetY(0) + me.GetY(1);
            point.Set(x / 2, y / 2);
        }

        private float Rotation(MotionEvent me)
        {
            double delta_x = me.GetX(0) - me.GetX(1);
            double delta_y = me.GetY(0) - me.GetY(1);
            double radians = System.Math.Atan2(delta_y, delta_x);
            return (float)(radians * (180 / System.Math.PI));
        }
        #endregion
    }
}
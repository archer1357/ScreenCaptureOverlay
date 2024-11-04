using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

using System.Drawing;
using System.Drawing.Imaging;

namespace Project1
{
    public class Screenshot
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr onj);

        public static BitmapSource take(int x,int y,int w,int h)
        {
            //int w = (int)SystemParameters.PrimaryScreenWidth;
            //int h = (int)SystemParameters.PrimaryScreenHeight;

            Bitmap bitmap = new Bitmap(w,h, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(x, y, 0, 0, bitmap.Size);
            }

            IntPtr handle = IntPtr.Zero;
            BitmapSource source=null;

            try
            {
                handle = bitmap.GetHbitmap();
                source = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            }
            catch (Exception)
            {
            }
            finally
            {
                DeleteObject(handle);
            }

            return source;
        }
    }
}

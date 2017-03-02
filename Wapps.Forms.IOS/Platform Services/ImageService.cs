using System;
using CoreGraphics;
using UIKit;
using Wapps.MVVM;

namespace Wapps.Forms.IOS
{
	public class ImageService : IImageService
	{
		public ImageService()
		{
		}


		public ICrossImage LoadFromResource (string fileName)
		{
			var nativeImage = UIImage.FromBundle (fileName);
			if (nativeImage != null)
			{
				return new CrossImage(nativeImage);
			}
			else 
			{
				return null;
			}
		}

		public ICrossImage LoadFromPath(string path)
		{
			var nativeImage = UIImage.FromFile (path);
			if (nativeImage != null)
			{
				return new CrossImage(nativeImage);
			}
			else
			{
				return null;
			}
		}

	}

	public class CrossImage : ICrossImage
	{
		public CrossImage (UIImage image)
		{
			this.Image = image;
		}

		public UIImage Image { get; private set; }

		public CrossImageSize Size
		{
			get
			{
				return new CrossImageSize { 
					Width = (float)this.Image.Size.Width,
					Height = (float)this.Image.Size.Height 
				};
			}
		}

		public void Cropp (float x, float y, float width, float height)
		{
			var imgSize = this.Image.Size;
			UIGraphics.BeginImageContext(new CGSize(width, height));
			var context = UIGraphics.GetCurrentContext();
			var clippedRect = new CGRect(0, 0, width, height);
			context.ClipToRect(clippedRect);
			var drawRect = new CGRect(-x, -y, imgSize.Width, imgSize.Height);
			this.Image.Draw(drawRect);
			var modifiedImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			this.Image = modifiedImage;
		}

		public void Resize(float width, float height)
		{
			UIImage originalImage = this.Image;
			UIImageOrientation orientation = originalImage.Orientation;

			//create a 24bit RGB image
			using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
												 (int)width, (int)height, 8,
												 (int)(4 * width), CGColorSpace.CreateDeviceRGB(),
												 CGImageAlphaInfo.PremultipliedFirst))
			{

				CGRect imageRect = new CGRect(0, 0, width, height);

				// draw the image
				context.DrawImage(imageRect, originalImage.CGImage);

				this.Image = UIKit.UIImage.FromImage(context.ToImage(), 0, orientation);
			}
		}

		public void Rotate(float angle)
		{
			UIGraphics.BeginImageContext (this.Image.Size);
			var context = UIGraphics.GetCurrentContext();
			context.RotateCTM((float)angle);
			this.Image.Draw (new CGPoint(0, 0));
			this.Image = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
		}

		public byte[] ToStream()
		{
			return this.Image.AsJPEG().ToArray();
		}

		UIImage ImageFromByteArray(byte[] data)
		{
			if (data == null)
			{
				return null;
			}

			UIKit.UIImage image;
			try
			{
				image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
			}
			catch (Exception e)
			{
				Console.WriteLine("Image load failed: " + e.Message);
				return null;
			}
			return image;
		}

	}
}


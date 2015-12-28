﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Lib.Csharp.Tools
{
    public class ImageHelper
    {
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式 HW:指定高宽缩放（可能变形） W:指定宽，高按比例 H:指定高，宽按比例 Cut:指定高宽裁减（不变形）</param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            var towidth = width;
            var toheight = height;

            var x = 0;
            var y = 0;
            var ow = originalImage.Width;
            var oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            var bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            var g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        /// 在图片上增加文字水印
        /// </summary>
        /// <param name="path">原服务器图片路径</param>
        /// <param name="pathSy">生成的带文字水印的图片路径</param>
        /// <param name="addText">生成的文字</param>
        /// <param name="isDeleteOld">是否删除旧图片</param>
        public static void AddWater(string path, string pathSy, string addText, bool isDeleteOld)
        {
            var image = System.Drawing.Image.FromFile(path);
            var g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(image, 0, 0, image.Width, image.Height);
            var x = image.Width / 2 - 50;
            var y = image.Height / 2 + 80;

            var gp = new GraphicsPath();
            var rec = new Rectangle(new Point(x, y), new Size(90, 20));
            gp.AddRectangle(rec);
            //g.FillRectangle(new Pen(Color.Gold), rec);
            g.FillRectangle(Brushes.White, rec);

            var f = new System.Drawing.Font("Verdana", 18);
            var b = new System.Drawing.SolidBrush(System.Drawing.Color.Gold);

            var rng = new System.Drawing.RectangleF(x, y, 90, 20);

            g.DrawString(addText, f, b, x, y);

            g.Dispose();

            image.Save(pathSy);
            image.Dispose();

            if (isDeleteOld && File.Exists(path))
                File.Delete(path);

        }

        /// <summary>
        /// 在图片上生成图片水印
        /// </summary>
        /// <param name="path">原服务器图片路径</param>
        /// <param name="pathSyp">生成的带图片水印的图片路径</param>
        /// <param name="pathSypf">水印图片路径</param>
        /// <param name="isDeleteOld">是否删除旧图片</param>
        public static void AddWaterPic(string path, string pathSyp, string pathSypf, bool isDeleteOld)
        {
            var image = System.Drawing.Image.FromFile(path);
            var copyImage = System.Drawing.Image.FromFile(pathSypf);
            var g = System.Drawing.Graphics.FromImage(image);
            var x = (image.Width - copyImage.Width) / 2;
            var y = (image.Width - copyImage.Width) / 2;
            var height = copyImage.Height;
            var width = copyImage.Width;
            g.DrawImage(copyImage, new System.Drawing.Rectangle(x, y, height, width), 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();

            image.Save(pathSyp);
            image.Dispose();

            if (isDeleteOld && File.Exists(path))
                File.Delete(path);
        }

        ///<summary>
        ///检查文件类型是否允许上传
        ///</summary>
        public static bool IsAllowedExtension(System.Web.UI.WebControls.FileUpload controls, string configNodeName)
        {
            var fileConfig = new string[] { ".jpg", ".bmp", ".png", ".gif" };

            if (!string.IsNullOrEmpty(controls.PostedFile.FileName))
            {
                var fileExtension = System.IO.Path.GetExtension(controls.PostedFile.FileName);
                for (var i = 0; i < fileConfig.Length; i++)
                {
                    if (fileExtension.Equals(fileConfig[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 判断上传文件大小是否超过设置的最大值
        /// </summary>
        public static bool IsAllowedLength(System.Web.UI.WebControls.FileUpload Controls)
        {
            var maxLength = 100;
            try
            {
                maxLength = 10 * 1024 * 1024;
            }
            catch
            {
                maxLength = 100;
            }

            maxLength = maxLength * 1024;

            if (Controls.PostedFile.ContentLength > maxLength)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 根据指定路径，获取其下所有文件列表，
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <returns>文件名 list</returns>
        public static List<string> GetFileInfo(string filePath)
        {
            FileSystemInfo fileinfo = new DirectoryInfo(filePath);
            var filelist = ListFileSort(fileinfo);
            return filelist;
        }

        /// <summary>
        /// 获取其下所有文件列表
        /// </summary>
        /// <param name="fileinfo"></param>
        /// <returns></returns>
        public static List<string> ListFileSort(FileSystemInfo fileinfo)
        {
            var filelist = new List<string>();
            var indent = 0;
            if (!fileinfo.Exists) return null;
            var dirinfo = fileinfo as DirectoryInfo;
            if (dirinfo == null) return null; //不是目录
            indent++;//缩进加一
            var files = dirinfo.GetFileSystemInfos();
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i] as FileInfo;
                if (file != null) // 是文件
                {
                    filelist.Add(file.Name);

                }
                else   //是目录
                {
                    //this.richTextBox1.Text += files[i].FullName + "/r/n/r/n";
                    //sb.Append(files[i].FullName + "/r/n/r/n");
                    ListFileSort(files[i]);  //对子目录进行递归调用
                }
            }
            indent--;//缩进减一
            return filelist;
        }
    }
}
// Amplify Color - Advanced Color Grading for Unity
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>

using System;
using System.Linq;
using UnityEngine;

namespace AmplifyColor
{
    public static class PhotoshopTouchHelper
    {
        public static bool LoadTexture2DLutFromPhotoshopData(byte[] data, LUTSettings settings, ref Texture2D texture)
        {
            int columns = settings.Columns;
            int rows = settings.Rows;
            int size = settings.Size;

            System.Collections.Generic.IEnumerable<byte> decryptedBytes = data.AsEnumerable();
            //byte imageType = decryptedBytes.Take(1).First();
            decryptedBytes = decryptedBytes.Skip(1);

            byte[] tempbytes = decryptedBytes.Take(4).Reverse().ToArray();
            decryptedBytes = decryptedBytes.Skip(4);
            int imageWidth = BitConverter.ToInt32(tempbytes, 0);

            tempbytes = decryptedBytes.Take(4).Reverse().ToArray();
            decryptedBytes = decryptedBytes.Skip(4);
            int imageHeight = BitConverter.ToInt32(tempbytes, 0);

            //tempbytes = decryptedBytes.Take(4).Reverse().ToArray();
            decryptedBytes = decryptedBytes.Skip(4);
            //int rowBytes = BitConverter.ToInt32(tempbytes, 0);

            //byte colorMode = decryptedBytes.Take(1).First();
            decryptedBytes = decryptedBytes.Skip(1);

            //byte channelCount = decryptedBytes.Take(1).First();
            decryptedBytes = decryptedBytes.Skip(1);

            //byte bitsChannel = decryptedBytes.Take(1).First();
            decryptedBytes = decryptedBytes.Skip(1);

            Color[,] imageData = new Color[imageWidth, imageHeight];

            byte[] bytesarray = decryptedBytes.ToArray();

            for (int i = 0, k = 0; i < imageHeight; i++)
            {
                for (int j = 0; j < imageWidth; j++)
                {
                    imageData[j, i] = new Color(bytesarray[k++] / 255f, bytesarray[k++] / 255f, bytesarray[k++] / 255f, 1f);
                }
            }

            Texture2D lutTexture = new Texture2D(size * size, size, TextureFormat.ARGB32, false);
            Color[] lutData = new Color[size * size * size];

            for (int h = 0, i = 0; h < size; h++)
            {
                for (int r = 0; r < rows; r++)
                {
                    for (int w = 0; w < size * columns; w++)
                    {
                        lutData[i++] = imageData[w, h + r * size];
                    }
                }
            }

            lutTexture.SetPixels(lutData);
            lutTexture.Apply();

            if (texture != null)
                Texture2D.DestroyImmediate(texture);

            texture = lutTexture;

            return true;
        }

        public static bool LoadTexture2DLutFromImage(Texture2D texture, ToolSettings settings, ref Texture2D lutTexture)
        {
            int width = settings.Resolution.TargetWidth;
            int height = settings.Resolution.TargetHeight;

            int size = settings.LUT.Size;
            int cols = settings.LUT.Columns;
            int rows = settings.LUT.Rows;

            Color[] imageData = texture.GetPixels();

            Texture2D lutText = new Texture2D(size * size, size, TextureFormat.ARGB32, false);
            Color[] lutData = new Color[size * size * size];


            for (int h = 0, i = 0; h < size; h++)
            {
                for (int r = 0; r < rows; r++)
                {
                    for (int w = 0; w < size * cols; w++)
                    {
                        int x = w;
                        int y = h + r * size;
                        y = height - 1 - y;
                        lutData[i++] = imageData[x + y * width];
                    }
                }
            }

            lutText.SetPixels(lutData);
            lutText.Apply();

            if (lutTexture != null)
                Texture2D.DestroyImmediate(lutTexture);

            lutTexture = lutText;

            return true;
        }
    }
}

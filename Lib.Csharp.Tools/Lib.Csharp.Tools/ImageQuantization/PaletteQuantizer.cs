/* 
  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF 
  ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
  THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A 
  PARTICULAR PURPOSE. 
  
    This is sample code and is freely distributable. 
*/

using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;

namespace Lib.Csharp.Tools.ImageQuantization
{
    /// <summary>
    /// Summary description for PaletteQuantizer.
    /// </summary>
    public class PaletteQuantizer : Quantizer
    {
        /// <summary>
        /// Lookup table for colors
        /// </summary>
        private readonly Hashtable _colorMap;

        /// <summary>
        /// List of all colors in the palette
        /// </summary>
        protected Color[] _colors;

        /// <summary>
        /// Construct the palette quantizer
        /// </summary>
        /// <param name="palette">The color palette to quantize to</param>
        /// <remarks>
        /// Palette quantization only requires a single quantization step
        /// </remarks>
        public PaletteQuantizer(ArrayList palette) : base(true)
        {
            _colorMap = new Hashtable();

            _colors = new Color[palette.Count];
            palette.CopyTo(_colors);
        }

        /// <summary>
        /// Override this to process the pixel in the second pass of the algorithm
        /// </summary>
        /// <param name="pixel">The pixel to quantize</param>
        /// <returns>The quantized value</returns>
        protected override byte QuantizePixel(Color32 pixel)
        {
            byte colorIndex = 0;
            var colorHash = pixel.ARGB;

            // Check if the color is in the lookup table
            if (_colorMap.ContainsKey(colorHash))
                colorIndex = (byte) _colorMap[colorHash];
            else
            {
                // Not found - loop through the palette and find the nearest match.
                // Firstly check the alpha value - if 0, lookup the transparent color
                if (0 == pixel.Alpha)
                {
                    // Transparent. Lookup the first color with an alpha value of 0
                    for (var index = 0; index < _colors.Length; index++)
                    {
                        if (0 == _colors[index].A)
                        {
                            colorIndex = (byte) index;
                            break;
                        }
                    }
                }
                else
                {
                    // Not transparent...
                    var leastDistance = int.MaxValue;
                    int red = pixel.Red;
                    int green = pixel.Green;
                    int blue = pixel.Blue;

                    // Loop through the entire palette, looking for the closest color match
                    for (var index = 0; index < _colors.Length; index++)
                    {
                        var paletteColor = _colors[index];

                        var redDistance = paletteColor.R - red;
                        var greenDistance = paletteColor.G - green;
                        var blueDistance = paletteColor.B - blue;

                        var distance = (redDistance*redDistance) +
                                       (greenDistance*greenDistance) +
                                       (blueDistance*blueDistance);

                        if (distance < leastDistance)
                        {
                            colorIndex = (byte) index;
                            leastDistance = distance;

                            // And if it's an exact match, exit the loop
                            if (0 == distance)
                                break;
                        }
                    }
                }

                // Now I have the color, pop it into the hashtable for next time
                _colorMap.Add(colorHash, colorIndex);
            }

            return colorIndex;
        }

        /// <summary>
        /// Retrieve the palette for the quantized image
        /// </summary>
        /// <param name="palette">Any old palette, this is overrwritten</param>
        /// <returns>The new color palette</returns>
        protected override ColorPalette GetPalette(ColorPalette palette)
        {
            for (var index = 0; index < _colors.Length; index++)
                palette.Entries[index] = _colors[index];

            return palette;
        }
    }
}
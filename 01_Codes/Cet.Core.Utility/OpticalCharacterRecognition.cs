using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronOcr;

namespace Cet.Core.Utility
{
    public static class OCRExtension
    {
        public static OcrResult RaedTextFromFile(string fullFilepath)
        {
            var Ocr = new AdvancedOcr()
            {
                CleanBackgroundNoise = true,
                EnhanceContrast = true,
                EnhanceResolution = true,
                Language = IronOcr.Languages.Thai.OcrLanguagePack,
                //Language = new IronOcr.Languages.MultiLanguage(IronOcr.Languages.Thai.OcrLanguagePack, IronOcr.Languages.English.OcrLanguagePack),
                Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
                ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
                DetectWhiteTextOnDarkBackgrounds = false,
                InputImageType = AdvancedOcr.InputTypes.AutoDetect,
                RotateAndStraighten = true,
                ReadBarCodes = false,
                ColorDepth = 4
            };
            var results = Ocr.Read(fullFilepath);
            return results;
        }

    }
}

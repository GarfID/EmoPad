using SharpDX.DirectWrite;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace GameOverlayExample.Examples
{
    public class ASDF
    {
        public static void GetFonts()
        {
            var fontList = new List<string>();

            using (var factory = new Factory())
            {
                using (var fontCollection = factory.GetSystemFontCollection(true))
                {
                    var familyCount = fontCollection.FontFamilyCount;
                    for (int i = 0; i < familyCount; i++)
                    {
                        try
                        {
                            using (var fontFamily = fontCollection.GetFontFamily(i))
                            {
                                var familyNames = fontFamily.FamilyNames;
                                int index;

                                if (!familyNames.FindLocaleName(CultureInfo.CurrentCulture.Name, out index))
                                    familyNames.FindLocaleName("en-us", out index);

                                string name = familyNames.GetString(index);
                                string display = name;
                                using (var font = fontFamily.GetFont(index))
                                {
                                    if (font.IsSymbolFont)
                                        display = "Segoe UI";
                                }

                                fontList.Add(name);
                            }
                        }
                        catch { }       // Corrupted font files throw an exception - ignore them
                    }
                }
            }

            fontList.Sort();

            foreach(string f in fontList)
            {
                Debug.WriteLine("Шрифт " + f);
            }
        }
    }
}
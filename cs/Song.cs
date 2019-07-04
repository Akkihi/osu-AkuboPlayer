using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace osu_akuboplayer
{
    public class Song
    {

        public string Title { get; set; }
        public string Artist { get; set; }
        public string ThumbnailPath { get; set; }
        public string Audiofile { get; set; }
        public string Tag { get; set; }

        public static List<string> Files = new List<string>();

        public static List<string> mp3 = new List<string>();

        public static List<string> Images = new List<string>();
        public static string[] files;


        public readonly bool IsBeatmap = true;

        public Song(DirectoryInfo folder)
        {
            

            
            var osuFile = folder.GetFiles("*.osu", SearchOption.AllDirectories);


            

            for (int i = 0; i < osuFile.Length; i++)
            {
                string lastFolderName = osuFile[i].DirectoryName;

                if (osuFile.Length < 1)
                {
                    IsBeatmap = true;
                    return;
                }



                var stream = new StreamReader(osuFile[i].FullName);
                while (!stream.EndOfStream)
                {
                    
                    var line = stream.ReadLine();

                    if (line != null && Regex.IsMatch(line, @"^Title:.+$"))
                    {
                        Title = new Regex(@"^Title:(.+)$").Match(line).Groups[1].ToString();
                    }

                    if (line != null && Regex.IsMatch(line, @"^TitleUnicode:.+$"))
                    {
                        Title = new Regex(@"^TitleUnicode:(.+)$").Match(line).Groups[1].ToString();
                    }

                    if (line != null && Regex.IsMatch(line, @"^Artist:.+$"))
                    {
                        Artist = new Regex(@"^Artist:(.+)$").Match(line).Groups[1].ToString();
                    }

                    if (line != null && Regex.IsMatch(line, @"^ArtistUnicode:.+$"))
                    {
                        Artist = new Regex(@"^ArtistUnicode:(.+)$").Match(line).Groups[1].ToString();
                    }

                    if (line != null && Regex.IsMatch(line, @"^AudioFilename:\s?.+$"))
                    {
                        Audiofile = "" + new Regex(@"AudioFilename[^:]*:[\s]* ([^\r\n]*)").Match(line).Groups[1];
                    }


                    if (ThumbnailPath == null && line != null &&
                        Regex.IsMatch(line, @"^\d,\d,(\d,)?""(.+\.(jpg|png))""(,\d,\d)?$", RegexOptions.IgnoreCase))
                    {
                        ThumbnailPath =
                            folder.FullName + @"\" + new Regex(@"^\d,\d,(\d,)?""(.+)""(,\d,\d)?$").Match(line).Groups[2];
                    }

                    
                    Tag = Title + "\t" + Artist + "\t" + Audiofile;

                }
                ;

                stream.Dispose();
                


                if (!Files.Contains(Artist + " — " + Title) && (!mp3.Contains(Audiofile)))
                {
                    Files.Add(Artist + " — " + Title );
                    mp3.Add(lastFolderName + @"\" + Audiofile);
                }

                                


                    if (ThumbnailPath == null || !File.Exists(ThumbnailPath))
                {

                    ThumbnailPath = "Resources/unknown.png";
                }

            }


        }

    }
}
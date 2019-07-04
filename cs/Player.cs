using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;

namespace osu_akuboplayer
{
    class Player
    {
        
        private static int Ghz = 44100; //Frequency

        public static bool InitDefaultDevice;// Status: Initialization

        public static int Stream; //Channel

        public static int Volume = 100; //Ofc, It's volume

        private static bool InitPlayer(int ghz)
        {
            if (!InitDefaultDevice)
                InitDefaultDevice = Bass.BASS_Init(-1, ghz, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            return InitDefaultDevice;
        }

        public static void Play(string filename, int vol)
        {
            Stop();
            if (InitPlayer(Ghz))
            {
                Stream = Bass.BASS_StreamCreateFile(filename, 0, 0, BASSFlag.BASS_DEFAULT);
                if(Stream != 0)
                {
                    Volume = vol;
                    Bass.BASS_ChannelSetAttribute(Stream, BASSAttribute.BASS_ATTRIB_VOL, Volume / 100F);
                    Bass.BASS_ChannelPlay(Stream, false);


                }
            }

        }

        

        //Stop button
        public static void Stop()
        {

            Bass.BASS_ChannelStop(Stream);
            Bass.BASS_StreamFree(Stream);
        }

        public static int GetTimeStream(int stream)
        {
            long TimeBytes = Bass.BASS_ChannelGetLength(stream);
            double Time = Bass.BASS_ChannelBytes2Seconds(stream, TimeBytes);
            return (int)Time;

        }

        public static int GetOfPosTrack(int stream)
        {
            long pos = Bass.BASS_ChannelGetPosition(Stream);
            double posSec = Bass.BASS_ChannelBytes2Seconds(stream, pos);
            return (int)posSec;
        }

        public static void PosScroll(int stream, int pos)
        {
            Bass.BASS_ChannelSetPosition(stream, (double)pos);
        }


        //Volume
        public static void SetVolumeToStream(int stream, int vol)
        {
            Volume = vol;
            Bass.BASS_ChannelSetAttribute(stream, BASSAttribute.BASS_ATTRIB_VOL, Volume / 100F);                       
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveIT2._1.Enums;
using LiveIT2._1.Terrain;

using NAudio.Wave;
using LiveIT2._1.Animals;
using System.IO;

namespace LiveIT2._1.Animation
{
    public partial class SoundEnvironment
    {
        WaveOut waveOutAnimalSpawn;
        public void PlaySpawnSound(Animal a)
        {
            if (!_isStopped)
            {
                if (File.Exists("../../../sounds/Animals/" + a.Texture.ToString() + "/spawn.wav"))
                {
                    var readerAnimalSpawn = new WaveFileReader("../../../sounds/Animals/" + a.Texture.ToString() + "/spawn.wav");
                    this.waveOutAnimalSpawn = new WaveOut();
                    this.waveOutAnimalSpawn.Init(readerAnimalSpawn);
                    this.waveOutAnimalSpawn.Volume = 0.7f;
                    waveOutAnimalSpawn.Play();
                }
            }
        }
    }
}

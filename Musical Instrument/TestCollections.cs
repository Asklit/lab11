using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musical_Instrument
{
    public class TestCollections
    {
        public Queue<Guitar> firstQueue { get; set; }
        public Queue<string> secondQueue { get; set; }
        public Dictionary<MusicalInstrument, Guitar> firstDict { get; set; }
        public Dictionary<string, Guitar> secondDict { get; set; }

        public TestCollections(int count)
        {
            firstQueue = new Queue<Guitar>();
            secondQueue = new Queue<string>();
            firstDict = new Dictionary<MusicalInstrument, Guitar>();
            secondDict = new Dictionary<string, Guitar>();
            AddRandomItems(count);
        }

        public void AddItem(Guitar guitar)
        {
            firstQueue.Enqueue(guitar);

            MusicalInstrument mi = guitar.GetBase;
            firstDict.Add(mi, guitar);

            secondQueue.Enqueue(guitar.ToString());

            secondDict.Add(guitar.ToString(), guitar);
        }

        public void AddRandomItems(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Guitar guitar = new Guitar();
                guitar.RandomInit();
                firstQueue.Enqueue(guitar);

                MusicalInstrument mi = guitar.GetBase;
                firstDict.Add(mi, guitar);

                secondQueue.Enqueue(guitar.ToString());

                secondDict.Add(guitar.ToString(), guitar);
            }
        }
    }
}

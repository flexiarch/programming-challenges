using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

namespace Assets.Scripts
{
    public class Timer : MonoBehaviour
    {
        public string mask;
        private Stopwatch stopwatch;
        private Text text;
        private StringBuilder sb;
        private int lastSeconds=-1;
        private const string ZeroZero = "{0:00}";
        private const char Colon = ':';

        void Start()
        {
            stopwatch = new Stopwatch();
            sb = new StringBuilder(mask.Length + 8);
            text = GetComponent<Text>();
        }

        void Update()
        {
            if (stopwatch.Elapsed.Seconds != lastSeconds)
            {
                lastSeconds = stopwatch.Elapsed.Seconds;
                var t = 
                 sb
                    .Remove(0, sb.Length)
                        .Append(mask)
                        .AppendFormat(ZeroZero, stopwatch.Elapsed.Hours)
                        .Append(Colon)
                        .AppendFormat(ZeroZero, stopwatch.Elapsed.Minutes)
                        .Append(Colon)
                        .AppendFormat(ZeroZero, stopwatch.Elapsed.Seconds)
                    .ToString();

                text.text = t;
            }
        }

        public void RunTimer()
        {
            stopwatch.Start();
        }

        public void StopTimer()
        {
            stopwatch.Stop();
        }

        public void ResetTimer()
        {
            stopwatch.Reset();
        }
    }
}

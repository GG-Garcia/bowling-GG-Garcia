using System;
using System.Collections.Generic;
/// <summary>
/// bowling application
/// </summary>
/// Author-Gregory Garcia
/// Date-10/3/2021
namespace bowling_GG_Garcia
{
   public class Program
    {
       Random randomScore = new();
       public Dictionary<int, (int, int, int?)> FrameScoringDic = new();
       public Dictionary<int, int> TotalScorePerFrame = new();
       public int totalScore = 0;
       public static void Main(string[] args)
        {
            Console.WriteLine("Bowling app v1");
            Program p = new();
            p.StartBowling();
            Console.WriteLine("End of Bowling app v1");
        }
      
        public int GetRandomValue(int rollValue)
        {
            try
            {
                return randomScore.Next(0, 11 - rollValue);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void StartBowling()
        {
            int frame = 1;
            //Sample Game and Final score;
            TestSample();
            FrameScoring();
            DisplayResults();
            FrameScoringDic.Clear();
            TotalScorePerFrame.Clear();
            totalScore = 0;
            // Random generated
            while (frame < 11)
            {
                FrameScoringDic.Add(frame, FrameProcess(frame));                
                FrameScoring();
                frame++;
            }
            DisplayResults();
        }
        public void TestSample()
        {
            FrameScoringDic.Add(1,(4,3, null));
            FrameScoringDic.Add(2,(7,3, null ));
            FrameScoringDic.Add(3,(5,2, null));
            FrameScoringDic.Add(4,(8,1, null));
            FrameScoringDic.Add(5,(4,6, null));
            FrameScoringDic.Add(6,(2,4, null));
            FrameScoringDic.Add(7,(8,0, null));
            FrameScoringDic.Add(8,(8,0, null));
            FrameScoringDic.Add(9,(8,2, null));
            FrameScoringDic.Add(10,(10,1,7));
        }
        public void FrameScoring()
        {
            // spare scoring = +10 and total of next frame
            // strike scoring = +10 and total next two roll
            bool strike = false;
            bool strike2 = false;
            bool spare = false;
            // this logic may be inefficient, however the scoring will be more accurate in real time.
            foreach(var frame in FrameScoringDic)
            {
                // in 10th frame
                if (frame.Key == 10)
                {
                    // previous roll
                    if (strike)
                    {
                        // roll before previous
                        if (strike2) { UpdateTotalScorePerFrame(frame.Key - 2, 20 + frame.Value.Item1); }
                        UpdateTotalScorePerFrame(frame.Key - 1, 10 + frame.Value.Item1 + frame.Value.Item2);
                    }
                    else if (spare)
                    {
                        UpdateTotalScorePerFrame(frame.Key - 1, 10 + frame.Value.Item1);
                    }
                    UpdateTotalScorePerFrame(frame.Key, frame.Value.Item1 + frame.Value.Item2 + (int)frame.Value.Item3);
                }
                else
                {
                    //if rolls a strike
                    if (frame.Value.Item1 == 10)
                    {
                        if (strike)
                        {
                            if (strike2) { UpdateTotalScorePerFrame(frame.Key - 2, 30); }
                            strike2 = true;
                        }
                        strike = true;
                        if (spare) { UpdateTotalScorePerFrame(frame.Key - 1, 20); spare = false; }
                    }
                    // if rolls a spare
                    else if (frame.Value.Item1 + frame.Value.Item2 == 10)
                    {
                        if (strike) 
                        {
                            if (strike2) 
                            { 
                               UpdateTotalScorePerFrame(frame.Key - 2, 20 + frame.Value.Item1); 
                               strike2 = false; 
                            }
                            UpdateTotalScorePerFrame(frame.Key - 1, 10 + frame.Value.Item1 + frame.Value.Item2);
                            strike = false;
                        }
                        if (spare) { UpdateTotalScorePerFrame(frame.Key - 1, 10 + frame.Value.Item1); }
                        spare = true;
                    }
                    //not a strike or spare
                    else
                    {
                        if (strike)
                        {
                            if (strike2)
                            {
                                UpdateTotalScorePerFrame(frame.Key - 2, 20 + frame.Value.Item1);
                                strike2 = false;
                            }                            
                            UpdateTotalScorePerFrame(frame.Key - 1, 10 + frame.Value.Item1 + frame.Value.Item2);
                            strike = false;                            
                        }
                        else if (spare)
                        {
                            UpdateTotalScorePerFrame(frame.Key - 1, 10 + frame.Value.Item1);
                            spare = false;
                        }
                        UpdateTotalScorePerFrame(frame.Key, frame.Value.Item1 + frame.Value.Item2);                        
                    }
                }
            }            
        }
        public void UpdateTotalScorePerFrame(int frame, int score)
        {
            // if totalscoreperframe dictionary contains the frame, we update the score, otherwise we insert a new frame.
            if (TotalScorePerFrame.ContainsKey(frame))
            {
                TotalScorePerFrame[frame] = score;
            }
            else
            {
                TotalScorePerFrame.Add(frame, score);
            }
        }
        public void DisplayResults()
        {
            foreach (var frame in FrameScoringDic)
            {
                totalScore += TotalScorePerFrame[frame.Key];
                Console.WriteLine("Random Roll: Frame: " + frame.Key + " Roll 1: " + frame.Value.Item1 + " Roll2: " + frame.Value.Item2 + " Role 3: " + frame.Value.Item3 + " Current Score: " + totalScore );
            }
        }
        public (int, int, int?) FrameProcess(int frame)
        {                        
            int roll1 = GetRandomValue(0);
            int roll2 = 0;
            int? roll3 = null;
            if(roll1 < 10)
            {
                roll2 = GetRandomValue(roll1);
            }
            if (frame == 10)
            {
                roll3 = 0;
                if (roll1 == 10)
                {
                    roll2 = GetRandomValue(roll2);
                    roll3 = GetRandomValue(roll2);                    
                }
                if (roll1 + roll2 == 10)
                {
                    roll3 = GetRandomValue((int)roll3);
                }                
            }                        
          
            return (roll1, roll2, roll3);
        }
    }
}

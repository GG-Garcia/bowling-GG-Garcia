using System;
using Xunit;
using System.IO;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void GetRandomValue()
        {
            bowling_GG_Garcia.Program p = new();
            Assert.True(p.GetRandomValue(0) < 11);
            Assert.True(p.GetRandomValue(10) == 0);
        }
        [Fact]
        public void TestSampleValidation()
        {
            bowling_GG_Garcia.Program p = new();
            p.TestSample();
            p.FrameScoring();
            p.DisplayResults();
            Assert.Equal(110,p.totalScore);
        }
        [Theory]
        [InlineData(1, 5)]
        [InlineData(4, 49)]
        [InlineData(6, 79)]
        [InlineData(10, 300)]
        [InlineData(9,99)]        
        public void UpdateTotalScorePerFrame(int frame, int score)
        {
            bowling_GG_Garcia.Program p = new();
            p.UpdateTotalScorePerFrame(frame, score);
            Assert.Equal(score, p.TotalScorePerFrame[frame]);
        }
        [Fact]
        public void TestScoringAllStrikesEqual300()
        {
            bowling_GG_Garcia.Program p = new();
            p.FrameScoringDic.Add(1, (10, 0, 0));
            p.FrameScoringDic.Add(2, (10, 0, 0));
            p.FrameScoringDic.Add(3, (10, 0, 0));
            p.FrameScoringDic.Add(4, (10, 0, 0));
            p.FrameScoringDic.Add(5, (10, 0, 0));
            p.FrameScoringDic.Add(6, (10, 0, 0));
            p.FrameScoringDic.Add(7, (10, 0, 0));
            p.FrameScoringDic.Add(8, (10, 0, 0));
            p.FrameScoringDic.Add(9, (10, 0, 0));
            p.FrameScoringDic.Add(10, (10, 10, 10));
            p.FrameScoring();
            p.DisplayResults();
            Assert.Equal(300, p.totalScore);
        }
        [Fact]
        public void TestScoringAllSparesEqual190()
        {
            bowling_GG_Garcia.Program p = new();
            p.FrameScoringDic.Add(1, (9, 1, 0));
            p.FrameScoringDic.Add(2, (9, 1, 0));
            p.FrameScoringDic.Add(3, (9, 1, 0));
            p.FrameScoringDic.Add(4, (9, 1, 0));
            p.FrameScoringDic.Add(5, (9, 1, 0));
            p.FrameScoringDic.Add(6, (9, 1, 0));
            p.FrameScoringDic.Add(7, (9, 1, 0));
            p.FrameScoringDic.Add(8, (9, 1, 0));
            p.FrameScoringDic.Add(9, (9, 1, 0));
            p.FrameScoringDic.Add(10, (9, 1, 9));
            p.FrameScoring();
            p.DisplayResults();
            Assert.Equal(190, p.totalScore);
        }
        [Fact]
        public void TestScoringRandomEqual164()
        {
            bowling_GG_Garcia.Program p = new();
            p.FrameScoringDic.Add(1, (9,1,0));
            p.FrameScoringDic.Add(2, (4,5,0));
            p.FrameScoringDic.Add(3, (6,2,0));
            p.FrameScoringDic.Add(4, (10,0,0));
            p.FrameScoringDic.Add(5, (5,5,0));
            p.FrameScoringDic.Add(6, (10,0,0));
            p.FrameScoringDic.Add(7, (10,0,0));
            p.FrameScoringDic.Add(8, (7,1,0));
            p.FrameScoringDic.Add(9, (10,0,0));
            p.FrameScoringDic.Add(10, (7,3,10));
            p.FrameScoring();
            p.DisplayResults();
            Assert.Equal(164, p.totalScore);
        }
        [Fact]
        public void TestScoringRandomSingleStrike9frameEqual164()
        {
            bowling_GG_Garcia.Program p = new();
            p.FrameScoringDic.Add(1, (9, 1, 0));
            p.FrameScoringDic.Add(2, (4, 5, 0));
            p.FrameScoringDic.Add(3, (6, 2, 0));
            p.FrameScoringDic.Add(4, (10, 0, 0));
            p.FrameScoringDic.Add(5, (5, 5, 0));
            p.FrameScoringDic.Add(6, (10, 0, 0));
            p.FrameScoringDic.Add(7, (10, 0, 0));
            p.FrameScoringDic.Add(8, (7, 1, 0));
            p.FrameScoringDic.Add(9, (10, 0, 0));
            p.FrameScoringDic.Add(10, (4,1,0));
            p.FrameScoring();
            p.DisplayResults();
            Assert.Equal(144, p.totalScore);
        }
    }
}

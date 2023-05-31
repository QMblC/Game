using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace MyGame.Code
{
    [TestFixture]
    public class TestChunk
    {
        public Chunk chunk;

        public static List<Vector2> vectors = new()
        {
            new Vector2(1,1),
            new Vector2(2,1),
            new Vector2(1,2),
            new Vector2(2,2),
        };

        [TestCase()]
        public void TestChunkBorderWalls1()
        {
            chunk = new Chunk(vectors[0]);
            Assert.AreEqual("###", chunk.FirstRow.ToString());
            Assert.AreEqual("#  ", chunk.SecondRow.ToString());
            Assert.AreEqual("#  ", chunk.ThirdRow.ToString());
            Assert.AreEqual(4, chunk.FreeCellsCount);
        }

        [TestCase()]
        public void TestChunkBorderWalls2()
        {
            chunk = new Chunk(vectors[1]);
            Assert.AreEqual("###", chunk.FirstRow.ToString());
            Assert.AreEqual("   ", chunk.SecondRow.ToString());
            Assert.AreEqual("   ", chunk.ThirdRow.ToString());

            Assert.AreEqual(6, chunk.FreeCellsCount);
        }

        [TestCase()]
        public void TestChunkBorderWalls3()
        {
            chunk = new Chunk(vectors[2]);
            Assert.AreEqual("#  ", chunk.FirstRow.ToString());
            Assert.AreEqual("#  ", chunk.SecondRow.ToString());
            Assert.AreEqual("#  ", chunk.ThirdRow.ToString());

            Assert.AreEqual(6, chunk.FreeCellsCount);
        }

        [TestCase()]
        public void TestChunkBorderWalls4()
        {
            chunk = new Chunk(vectors[3]);
            Assert.AreEqual("   ", chunk.FirstRow.ToString());
            Assert.AreEqual("   ", chunk.SecondRow.ToString());
            Assert.AreEqual("   ", chunk.ThirdRow.ToString());

            Assert.AreEqual(9, chunk.FreeCellsCount);
        }

        [TestCase()]
        public void TestChunkBorderWalls5()
        {
            var map = new List<string>
            {
                "############",
                "#          #",
                "#          #",
                "#          #",
                "#          #",
                "#          #",
                "#          #",
                "#          #",
                "#          #",
                "#          #",
                "#          #",
                "############"
            };
            var answer = new List<string>
            {
                "            ",
                "            ",
                "            ",
                "            ",
                "            ",
                "            ",
                "            ",
                "            ",
                "            ",
                "            ",
                "            ",
                "            "
            };
            var row1 = new StringBuilder();
            var row2 = new StringBuilder();
            var row3 = new StringBuilder();

            for (var i = 1; i < 11; i += 3)
                for(var j = 1; j < 11; j+=3)
                {
                    var chunk = new Chunk(new Vector2(i, j));
                    if (row1.Length == 12)
                    {
                        answer[j - 1] = row1.ToString();
                        answer[j] = row2.ToString();
                        answer[j + 1] = row3.ToString();
                    }            
                    else
                    {
                        row1.Append(chunk.FirstRow);
                        row2.Append(chunk.SecondRow);
                        row3.Append(chunk.ThirdRow);
                    }

                }
            for (var i = 1; i < 11; i += 3)
                for (var j = 1; j < 11; j += 3)
                    Assert.AreEqual(map[i][j], answer[i][j]);
        }
        [TestCase()]
        public void TestEmptyCellsCounter()
        {
            var chunk = new Chunk(vectors[0]);

            var pos = new List<Vector2>
            {
                new Vector2(1,1),
                new Vector2(1,2),
                new Vector2(2,1),
                new Vector2(2,2),
            };
            for (var i = 0; i < chunk.FreeCells.Count; i++)
                Assert.AreEqual(pos[i], chunk.FreeCells[i]);
        }
    }
}

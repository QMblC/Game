using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Code
{
    /*
     * Алгоритм:
     * Спавним рандомную точку выхода не в первой четверти карты
     * Чанк с выходом запоминаем и его генерируем последним
     * Идем по чанкам рандомно ставим стены и проверяем, чтобы из всех предыдущих подтверждённых стен был доступ к выходу
     * Проверяем чтобы не было кривых углов
     * Если что-то не совпадает переделываем чанк
     * Стены ставим рандомное количество от 0 до кол-ва пустых клеток в чанке уменьшая на 1 за каждые 3 фейла, чтобы упростить генерацию
     * 
     */
    public class Chunk
    {
        public Vector2 CenterPostion;

        public StringBuilder FirstRow = new("   ");
        public StringBuilder SecondRow = new("   ");
        public StringBuilder ThirdRow = new("   ");


        public List<Vector2> FreeCells => GetFreeCells();
        public int FreeCellsCount => FirstRow.ToString().Where(r => r == ' ').Count()
            + SecondRow.ToString().Where(r => r == ' ').Count()
            + ThirdRow.ToString().Where(r => r == ' ').Count();

        public Chunk(Vector2 centerPostion)
        {
            if (centerPostion.X < 1 || centerPostion.Y < 1
                || centerPostion.X > 10 || centerPostion.Y > 10)
                throw new Exception("Center ne mozhet  bit\' s krau");
            CenterPostion = centerPostion;
            SetBorderWalls();
        }

        private List<Vector2> GetFreeCells()
        {
            var list = new List<Vector2>();
            for(var i = 0; i < 3; i++)
            {
                if (FirstRow[i] == ' ')
                    list.Add(new Vector2(CenterPostion.X + i - 1, CenterPostion.Y - 1));
                if (SecondRow[i] == ' ')
                    list.Add(new Vector2(CenterPostion.X + i - 1, CenterPostion.Y));
                if (ThirdRow[i] == ' ')
                    list.Add(new Vector2(CenterPostion.X + i - 1, CenterPostion.Y + 1));
            }
            return list;
        }

        public void FillChunk()
        {
            var rnd = new Random();
            var isFinishedGenerating = false;
            var failedAttempt = 0;
            var additionalRatio = 0;
            var list = new List<Vector2>();

            var wallCount = rnd.Next(0, FreeCellsCount + 1);

            while (!isFinishedGenerating)
            {
                if (failedAttempt % 3 == 0)
                    additionalRatio++;
                wallCount -= additionalRatio;
                for (var i = 0; i < wallCount; i++)
                    list.Add(FreeCells[rnd.Next(0, wallCount + 1)]);   
            }
        }

        private void SetBorderWalls()
        {
            if (CenterPostion.Y == 1)
                FirstRow = new StringBuilder("###");
            if (CenterPostion.X == 1)
            {
                FirstRow[0] = '#';
                SecondRow[0] = '#';
                ThirdRow[0] = '#';
            }
            if (CenterPostion.Y == 10)
                ThirdRow = new StringBuilder("###");
            if (CenterPostion.X == 10)
            {
                FirstRow[2] = '#';
                SecondRow[2] = '#';
                ThirdRow[2] = '#';
            }
        }
    }
}

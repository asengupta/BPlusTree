using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BPlusTree
{
    
    [TestFixture]
    public class LayoutTest
    {
        private Box currentBox;

        [Test]
        public void ArbitraryStuff()
        {
            var boundingBox = new Box(0, 0, 500, 500);
            var picture = new Box(60, 60, 100, 100);
            var picture2 = new Box(300, 80, 400, 200);
            var picture3 = new Box(10, 130, 100, 200);

            var pictures = new List<Box> {picture, picture2, picture3};
            var horizontalLines = new List<HorizontalLine>{ new HorizontalLine(boundingBox.Y1), new HorizontalLine(boundingBox.Y2)};
            var verticalLines = new List<VerticalLine>{new VerticalLine(boundingBox.X1), new VerticalLine(boundingBox.X2)};
            pictures.ForEach(box =>
                                 {
                                     horizontalLines.Add(new HorizontalLine(box.Y1));
                                     horizontalLines.Add(new HorizontalLine(box.Y2));
                                     verticalLines.Add(new VerticalLine(box.X1));
                                     verticalLines.Add(new VerticalLine(box.X2));
                                 });

            horizontalLines.Sort((line1,line2) =>
                                     {
                                         if (line1.Y == line2.Y) return 0;
                                         if (line1.Y < line2.Y) return -1;
                                         return 1;
                                     });
            verticalLines.Sort((line1,line2) =>
                                     {
                                         if (line1.X == line2.X) return 0;
                                         if (line1.X < line2.X) return -1;
                                         return 1;
                                     });

            var allMergedBoxes = new List<Box>();
            foreach (var horizontalLine in horizontalLines)
            {
                if (horizontalLines.Last() == horizontalLine) break;
                var top = horizontalLine;
                var bottom = horizontalLines[horizontalLines.IndexOf(horizontalLine) + 1];
                currentBox = null;

                foreach (var verticalLine in verticalLines)
                {
                    if (verticalLines.Last() == verticalLine)
                    {
                        TerminateBox(currentBox, allMergedBoxes);
                        break;
                    }
                    var left = verticalLine;
                    var right = verticalLines[verticalLines.IndexOf(verticalLine) + 1];
                    if (new Box(left.X, top.Y, right.X, bottom.Y).OverlapsWith(pictures))
                    {
                        TerminateBox(currentBox, allMergedBoxes);
                        continue;
                    }
                    AddToCurrentBox(currentBox, left.X, top.Y, right.X, bottom.Y);
                }
            }

            allMergedBoxes.ForEach(box => Console.Out.WriteLine(box));
        }

        private void TerminateBox(Box box, List<Box> allMergedBoxes)
        {
            if (currentBox == null) return;
            allMergedBoxes.Add(currentBox);
            currentBox = null;
        }

        private void AddToCurrentBox(Box box, int x1, int y1, int x2, int y2)
        {
            currentBox = currentBox == null ? new Box(x1, y1, x2, y2) : new Box(currentBox.X1, currentBox.Y1, x2, y2);
        }
    }

    public class HorizontalLine
    {
        private readonly int y;

        public HorizontalLine(int y)
        {
            this.y = y;
        }

        public int Y
        {
            get { return y; }
        }
    }

    public class VerticalLine
    {
        private readonly int x;

        public VerticalLine(int x)
        {
            this.x = x;
        }

        public int X
        {
            get { return x; }
        }
    }

    public class Box
    {
        private readonly int x1;
        private readonly int y1;
        private readonly int x2;
        private readonly int y2;

        public Box(int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        public int X1
        {
            get { return x1; }
        }

        public int Y1
        {
            get { return y1; }
        }

        public int X2
        {
            get { return x2; }
        }

        public int Y2
        {
            get { return y2; }
        }

        public bool OverlapsWith(List<Box> pictures)
        {
            foreach (var picture in pictures)
            {
                if (picture.Equals(this)) return true;
                if (picture.x1 == x1 && picture.x2 == x2 && ((y1 > picture.y1 && y1 < picture.y2) || (y2 > picture.y1 && y2 < picture.y2))) return true;
            }
            return false;
        }

        private bool Contains(int x, int y)
        {
            return x > x1 && x < x2 && y > y1 && y < y2;
        }

        public override string ToString()
        {
            return string.Format("({0},{1})-({2},{3})", x1, y1, x2, y2);
        }

        public bool Equals(Box other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.x1 == x1 && other.y1 == y1 && other.x2 == x2 && other.y2 == y2;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Box)) return false;
            return Equals((Box) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = x1;
                result = (result*397) ^ y1;
                result = (result*397) ^ x2;
                result = (result*397) ^ y2;
                return result;
            }
        }
    }
}

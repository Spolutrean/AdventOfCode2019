using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace standard {
    internal class Program {
        public static void Main(string[] args) {
            String[] path1, path2;
            IEnumerable<string> paths = File.ReadLines("/home/spolutrean/adventofcode2019/day3/standard/standard/in.txt");
            path1 = paths.First().Split(',');
            path2 = paths.Last().Split(',');
            int ansDist = (int)1e9;
            Point ans;
            List<Line> 
                lines1 = new List<Line>(), 
                lines2 = new List<Line>();
            Point currBegin = new Point(0, 0);
            foreach (var part in path1) {
                char direction = part[0];
                int count = Convert.ToInt32(part.Substring(1));
                Point currEnd = new Point(currBegin.x, currBegin.y);
                if (direction == 'U') {
                    currEnd.y += count;
                } else if (direction == 'D') {
                    currEnd.y -= count;
                } else if (direction == 'L') {
                    currEnd.x -= count;
                } else if (direction == 'R') {
                    currEnd.x += count;
                }
                lines1.Add(new Line(currBegin.Clone(), currEnd.Clone()));
                currBegin = currEnd;
            }
            currBegin = new Point(0, 0);
            foreach (var part in path2) {
                char direction = part[0];
                int count = Convert.ToInt32(part.Substring(1));
                Point currEnd = new Point(currBegin.x, currBegin.y);
                if (direction == 'U') {
                    currEnd.y += count;
                } else if (direction == 'D') {
                    currEnd.y -= count;
                } else if (direction == 'L') {
                    currEnd.x -= count;
                } else if (direction == 'R') {
                    currEnd.x += count;
                }
                lines2.Add(new Line(currBegin.Clone(), currEnd.Clone()));
                currBegin = currEnd;
            }

            foreach (var line1 in lines1) {
                foreach (var line2 in lines2) {
                    Point p = intersectLines(line1, line2);
                    if (p == null || (p.x == 0 && p.y == 0)) {
                        continue;
                    }

                    int dist = Math.Abs(p.x) + Math.Abs(p.y);
                    if (dist < ansDist) {
                        ansDist = dist;
                        ans = p;
                    }
                }
            }
            
            Console.WriteLine(ansDist);
        }

        public static Point intersectLines(Line a, Line b) {
            if (a.isVertical() && b.isVertical() || a.isHorizontal() && b.isHorizontal()) {
                return null;
            }

            int x = 0, y = 0;
            if (a.isVertical()) {
                x = a.begin.x;
            }
            else {
                y = a.begin.y;
            }
            
            if (b.isVertical()) {
                x = b.begin.x;
            }
            else {
                y = b.begin.y;
            }

            Point p = new Point(x, y);
            if (a.contains(p) && b.contains(p)) {
                return p;
            }
            else {
                return null;
            }
        }
    }

    class Point {
        public int x, y;

        public Point(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Point Clone() {
            return new Point(x, y);
        }
    }

    class Line {
        public Point begin, end;
        
        public Line(Point begin, Point end) {
            this.begin = begin;
            this.end = end;
            normalize();
        }

        private void normalize() {
            if (begin.x > end.x) {
                (begin.x, end.x) = (end.x, begin.x);
            }
            
            if (begin.y > end.y) {
                (begin.y, end.y) = (end.y, begin.y);
            }
        }

        public bool isVertical() {
            return begin.x == end.x;
        }
        
        public bool isHorizontal() {
            return begin.y == end.y;
        }

        public bool contains(Point p) {
            return (p.x >= begin.x && p.x <= end.x) && (p.y >= begin.y && p.y <= end.y);
        }
    }
}
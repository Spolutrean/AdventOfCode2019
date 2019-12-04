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
            IEnumerable<string> paths = File.ReadLines("/home/spolutrean/adventofcode2019/day3/extra/extra/in.txt");
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

            for (int i = 0; i < lines1.Count; ++i) {
                for(int j = 0; j < lines2.Count; ++j) {
                    Line line1 = lines1[i];
                    Line line2 = lines2[j];
                    
                    int length1 = 0, length2 = 0;
                    for (int q = 0; q < i; ++q) {
                        length1 += lines1[q].length();
                    }
                    for (int q = 0; q < j; ++q) {
                        length2 += lines2[q].length();
                    }
                    
                    Point p = intersectLines(line1, line2);
                    if (p == null || (p.x == 0 && p.y == 0)) {
                        continue;
                    }

                    int additionalLength1 = line1.getDistanceOnPoint(p), 
                        additionalLength2 = line2.getDistanceOnPoint(p);
                    
                    
                    int dist = length1 + length2 + additionalLength1 + additionalLength2;
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

            return null;
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
        }
        
        public bool isVertical() {
            return begin.x == end.x;
        }
        
        public bool isHorizontal() {
            return begin.y == end.y;
        }

        public bool contains(Point p) {
            int maxX, minX, maxY, minY;
            maxX = Math.Max(begin.x, end.x);
            minX = Math.Min(begin.x, end.x);
            maxY = Math.Max(begin.y, end.y);
            minY = Math.Min(begin.y, end.y);
            return (p.x >= minX && p.x <= maxX) && (p.y >= minY && p.y <= maxY);
        }

        public int length() {
            return Math.Abs(begin.x - end.x) + Math.Abs(begin.y - end.y);
        }

        public int getDistanceOnPoint(Point p) {
            return Math.Abs(p.x - begin.x) + Math.Abs(p.y - begin.y);
        }
    }
}
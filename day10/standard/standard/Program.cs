using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace standard {
    internal class Program {
        public static void Main(string[] args) {
            String[] lines = File.ReadAllLines("/home/spolutrean/adventofcode2019/day10/standard/standard/in.txt");
            List<Point> points = new List<Point>();
            for (int i = 0; i < lines.Length; ++i) {
                for (int j = 0; j < lines[0].Length; ++j) {
                    if (lines[i][j] == '#') {
                        points.Add(new Point(j * 1.0, i * 1.0));
                    }
                }
            }

            int ans = 0;
            int ansPointPos = 0;
            for (int i = 0; i < points.Count; ++i) {
                Point from = points[i];
                List<Point> watched = new List<Point>();
                for (int j = 0; j < points.Count; ++j) {
                    if (i == j) {
                        continue;
                    }

                    Point to = points[j];
                    Point direction = new Point(to.x - from.x, to.y - from.y);
                    direction.normalize();
                    if (!watched.Contains(direction)) {
                        watched.Add(direction);
                    }
                }

                if (watched.Count > ans) {
                    ans = watched.Count;
                    ansPointPos = i;
                }
            }
            Point center = points[ansPointPos];
            points.RemoveAt(ansPointPos);
            
            Dictionary<Point, List<Point>> pointLines = new Dictionary<Point, List<Point>>(new MyComparer());
            for (int i = 0; i < points.Count; ++i) {
                Point to = points[i];
                Point direction = new Point(to.x - center.x, to.y - center.y);
                direction.normalize();
                if (!pointLines.ContainsKey(direction)) {
                    pointLines.Add(direction, new List<Point>());
                }
                pointLines[direction].Add(to);
            }

            Point verticalDirection = new Point(0, -1);
            List<Point> directions = new List<Point>(pointLines.Keys).OrderBy(o => -o.getAngle(verticalDirection)).ToList();
            
            foreach(Point direction in directions) {
                pointLines[direction] = pointLines[direction].OrderBy(p => p.distanceTo(center)).ToList();
            }
            
            int cntDestroyed = 0;
            Point destroyed = null;
            int step = 0;
            while (cntDestroyed != 200) {
                Point direction = directions[step];
                if (pointLines[direction].Count == 0) {
                    ++step;
                    step %= directions.Count;
                    continue;
                }

                destroyed = pointLines[direction][0];
                Console.WriteLine("Destroyed: " + destroyed);
                pointLines[direction].RemoveAt(0);
                ++cntDestroyed;
                ++step;
                step %= directions.Count;
            }
            Console.Write(destroyed);
        }

        class MyComparer : IEqualityComparer<Point> {
            public bool Equals(Point x, Point y) {
                return x.Equals(y);
            }

            public int GetHashCode(Point obj) {
                return base.GetHashCode();
            }
        }
        
        class Point {
            private static double eps = 1e-13;
            public double x, y;

            public Point(double x, double y) {
                this.x = x;
                this.y = y;
            }

            double getLength() {
                return Math.Sqrt(x * x + y * y);
            }
            
            public void normalize() {
                double length = getLength();
                x /= length;
                y /= length;
            }

            public double distanceTo(Point p) {
                double dx = p.x - x, dy = p.y - y;
                return Math.Sqrt(dx * dx + dy * dy);
            }

            public double getAngle(Point r) {
                if (Equals(r)) {
                    return 2 * Math.PI;
                }
                double angle = Math.Atan2(r.y, r.x) - Math.Atan2(y, x);
                if (angle < 0) {
                    angle += 2 * Math.PI;
                }
                return angle;
            }

            public override bool Equals(object obj) {
                Point to = (Point) obj;
                return Math.Abs(to.x - x) < eps && Math.Abs(to.y - y) < eps;
            }

            public override string ToString() {
                return "x=" + x + " y=" + y;
            }
        }
    }
}
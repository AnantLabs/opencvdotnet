using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace OpenCVDotNet.UI
{
    /// <summary>
    /// Shows values over time in a moving graph.
    /// </summary>
    public partial class ValueOverTime : UserControl
    {
        private const int SPACING = 1;
        private Random random = new Random();
        private List<Measurement> values = new List<Measurement>();
        private Color defaultColorGroup = Color.Blue;
        private Dictionary<Color, Statistics> statistics = new Dictionary<Color, Statistics>();

        /// <summary>
        /// Creates control.
        /// </summary>
        public ValueOverTime()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Adds a value to the graph, in the default color group.
        /// </summary>
        /// <param name="val"></param>
        public void AddValue(double val)
        {
            AddValue(val, defaultColorGroup);
        }

        /// <summary>
        /// The default color applied to measurements added without a group.
        /// </summary>
        public Color DefaultColorGroup
        {
            get { return defaultColorGroup; }
            set { defaultColorGroup = value; }
        }

        /// <summary>
        /// Adds another value to the measurement list with a specific color group.
        /// </summary>
        public void AddValue(double val, Color color)
        {
            AddValue(new Measurement(val, color));
        }

        /// <summary>
        /// Adds a Measurement object.
        /// </summary>
        public void AddValue(Measurement measurement)
        {
            values.Add(measurement);
            Invalidate();
        }

        /// <summary>
        /// Removes all measurements.
        /// </summary>
        public void Reset()
        {
            values.Clear();
            Invalidate();
        }

        /// <summary>
        /// Called to paint the control.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            int xPos = 0;

            int visibleValues = (Width - xPos) / SPACING;
            int firstValueIdx = values.Count - visibleValues;
            if (firstValueIdx < 0) firstValueIdx = 0;

            double minValue = 0.0;
            double maxValue = 0.0;
            double total = 0.0;

            // clear all stats.
            statistics.Clear();

            for (int valIdx = firstValueIdx; valIdx < values.Count; ++valIdx)
            {
                Measurement msr = values[valIdx];
                double val = msr.Value;


                if (valIdx != firstValueIdx)
                {
                    if (val < minValue) minValue = val;
                    if (val > maxValue) maxValue = val;
                }
                else
                {
                    maxValue = val + 1.0;
                    minValue = val - 1.0;
                }

                // add statistics value for this group if it's not there yet.
                if (!statistics.ContainsKey(msr.Color))
                {
                    statistics[msr.Color] = new Statistics(msr.Color, val);
                }
                else
                {
                    statistics[msr.Color].AddValue(val);
                }

                total += val;
            }

            int avg = (int)Math.Round(total / values.Count);

            for (int valIdx = firstValueIdx; valIdx < values.Count; ++valIdx)
            {
                Measurement msr = values[valIdx];
                double val = msr.Value;
                double ratio = (val - minValue) / (maxValue - minValue);
                int yPos = (int)Math.Round(ratio * (double)Height);

                e.Graphics.DrawLine(
                    new Pen(msr.Color),
                    new Point(xPos, Height - yPos),
                    new Point(xPos, Height));

                xPos += SPACING;
            }
        }

        /// <summary>
        /// Returns statistics for a color group.
        /// </summary>
        public Statistics GetStatistics(Color group)
        {
            if (!statistics.ContainsKey(group))
                return null;

            return statistics[group];
        }
    }

    /// <summary>
    /// Represents statistical information for a specific group.
    /// </summary>
    public class Statistics
    {
        private double total;
        private int values;
        private double minValue;
        private double maxValue;
        private Color group;
        private double lastValue;

        /// <summary>
        /// Creates a new statistics object with initial min and max values.
        /// </summary>
        public Statistics(Color group, double firstValue)
        {
            this.group = group;
            this.minValue = firstValue - 1.0;
            this.maxValue = firstValue + 1.0;
            this.total = firstValue;
            this.values = 1;
            this.lastValue = firstValue;
        }

        /// <summary>
        /// Adds another value to the statistics.
        /// </summary>
        public void AddValue(double val)
        {
            total += val;
            values++;

            if (val < MinValue) minValue = val;
            if (val > MaxValue) maxValue = val;

            lastValue = val;
        }

        public int Values { get { return values; } }
        public double Average { get { return Total / Values; } }
        public double Total { get { return total; } }
        public Color Group { get { return group; } }
        public double MinValue { get { return minValue; } }
        public double MaxValue { get { return maxValue; } }
        public double LastValue { get { return lastValue; } }
    }

    /// <summary>
    /// Represents a measurement that can be added to the ValueOverTime control.
    /// </summary>
    public struct Measurement
    {
        private double value;
        private Color color;

        /// <summary>
        /// Creates a new measurement object.
        /// </summary>
        /// <param name="val">The value of the measurement</param>
        /// <param name="grp">Group index of the measurement</param>
        public Measurement(double val, Color color)
        {
            this.value = val;
            this.color = color;
        }

        public double Value { get { return value; } }
        public Color Color { get { return color; } }
    }
}

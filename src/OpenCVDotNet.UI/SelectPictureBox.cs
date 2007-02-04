using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace OpenCVDotNet.UI
{
    public partial class SelectPictureBox : PictureBox
    {
        private SelectBox selectBox;
        private bool showSelectBox = true;

        public SelectPictureBox()
        {
            InitializeComponent();

            this.MouseMove += new MouseEventHandler(SelectPictureBox_MouseMove);
            this.MouseDown += new MouseEventHandler(SelectPictureBox_MouseDown);
            this.MouseUp += new MouseEventHandler(SelectPictureBox_MouseUp);

            this.selectBox = new SelectBox(this, new Rectangle(120, 120, 50, 50));
            this.selectBox.AddHandle(new HandleMove());
            this.selectBox.AddHandle(new HandleResizeNWSE());
            this.selectBox.AddHandle(new HandleResizeSouth());
            this.selectBox.AddHandle(new HandleResizeEast());
            this.selectBox.OnBoxChanged += new EventHandler(selectBox_OnBoxChanged);
        }

        void selectBox_OnBoxChanged(object sender, EventArgs e)
        {
            if (SelectionChanged != null)
                SelectionChanged(this, e);
        }

        public Rectangle SelectionRect
        {
            get
            {
                return selectBox.Rect;
            }

            set
            {
                selectBox.Rect = value;
                Invalidate();
            }
        }

        public event EventHandler SelectionChanged;

        void SelectPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            selectBox.OnMouseUp(e);
        }

        void SelectPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            selectBox.OnMouseDown(e);
        }

        void SelectPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            selectBox.OnMouseMove(e);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (showSelectBox)
                this.selectBox.OnPaint(pe);
        }

        /// <summary>
        /// Determines if the selection box is shown.
        /// </summary>
        public bool ShowSelection
        {
            get { return showSelectBox; }
            set
            {
                showSelectBox = value;
                Invalidate();
            }
        }
    }

    public abstract class SelectBoxHandle
    {
        public const int INFLATE_SIZE = 2;
        private SelectBox sb = null;

        public SelectBox SelectBox
        {
            get { return this.sb; }
            set { this.sb = value; }
        }

        public abstract Rectangle HandleRect { get; }
        public abstract Cursor Cursor { get; }
        public abstract void OnPaint(PaintEventArgs pe);

        public bool HitTest(int x, int y)
        {
            Rectangle inflated = HandleRect;
            inflated.Inflate(INFLATE_SIZE, INFLATE_SIZE);
            return (inflated.Contains(x, y));
        }

        public virtual void OnDragStart(MouseEventArgs e) { }
        public virtual void OnDragEnd(MouseEventArgs e) { }
        public virtual void OnDragging(MouseEventArgs e) { }

    }

    public abstract class HandleResize : SelectBoxHandle
    {
        public const int HANDLE_SIZE = 6;

        public override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.FillRectangle(Brushes.White, HandleRect);
            pe.Graphics.DrawRectangle(Pens.Black, HandleRect);
        }

        public override Rectangle HandleRect
        {
            get
            {
                return new Rectangle(new Point(
                  Position.X - HANDLE_SIZE / 2,
                  Position.Y - HANDLE_SIZE / 2),
                  new Size(HANDLE_SIZE, HANDLE_SIZE));
            }
        }

        protected abstract Point Position { get; }
    }

    public class HandleResizeNWSE : HandleResize
    {
        public override Cursor Cursor { get { return Cursors.SizeNWSE; } }
        protected override Point Position { get { return new Point(SelectBox.Rect.Right, SelectBox.Rect.Bottom); } }

        public override void OnDragging(MouseEventArgs e)
        {
            SelectBox.Rect.Width = e.X - SelectBox.Rect.X;
            SelectBox.Rect.Height = e.Y - SelectBox.Rect.Y;
            SelectBox.Parent.Invalidate();
        }
    }

    public class HandleResizeEast : HandleResize
    {
        public override Cursor Cursor { get { return Cursors.SizeWE; } }
        protected override Point Position { get { return new Point(SelectBox.Rect.Right, SelectBox.Rect.Top + SelectBox.Rect.Height / 2); } }
        public override void OnDragging(MouseEventArgs e)
        {
            SelectBox.Rect.Width = e.X - SelectBox.Rect.X;
            SelectBox.Parent.Invalidate();
        }
    }

    public class HandleResizeSouth : HandleResize
    {
        protected override Point Position
        {
            get
            {
                return new Point(SelectBox.Rect.Left + SelectBox.Rect.Width / 2,
                  SelectBox.Rect.Bottom);
            }
        }

        public override Cursor Cursor { get { return Cursors.SizeNS; } }

        public override void OnDragging(MouseEventArgs e)
        {
            SelectBox.Rect.Height = e.Y - SelectBox.Rect.Y;
            SelectBox.Parent.Invalidate();
        }
    }

    public class HandleMove : SelectBoxHandle
    {
        public override Rectangle HandleRect
        {
            get
            {
                Rectangle sbr = SelectBox.Rect;
                Rectangle mine = new Rectangle(sbr.X, sbr.Y, sbr.Width, sbr.Height);
                return mine;
            }
        }

        public override void OnPaint(PaintEventArgs pe) { return; }
        public override Cursor Cursor { get { return Cursors.SizeAll; } }


        Point dragStart;

        public override void OnDragStart(MouseEventArgs e)
        {
            dragStart = new Point(e.X - SelectBox.Rect.X, e.Y - SelectBox.Rect.Y);
        }

        public override void OnDragging(MouseEventArgs e)
        {
            SelectBox.Rect.X = e.X - dragStart.X;
            SelectBox.Rect.Y = e.Y - dragStart.Y;
            SelectBox.Parent.Invalidate();
        }
    }

    public class SelectBox
    {
        public Rectangle Rect;
        private List<SelectBoxHandle> handles = new List<SelectBoxHandle>();
        Control parent;
        private SelectBoxHandle activeHandle = null;

        public SelectBox(Control parent, Rectangle rect)
        {
            this.Rect = rect;
            this.parent = parent;
        }

        public void AddHandle(SelectBoxHandle handle)
        {
            handle.SelectBox = this;
            handles.Add(handle);
        }

        public virtual void OnPaint(PaintEventArgs pe)
        {
            Pen p = new Pen(Brushes.DarkBlue, 2.0f);
            pe.Graphics.DrawRectangle(p, this.Rect);

            foreach (SelectBoxHandle sbh in handles)
                sbh.OnPaint(pe);
        }

        public bool HitTest(int x, int y)
        {
            return this.Rect.Contains(x, y);
        }

        public virtual void OnMouseMove(MouseEventArgs e)
        {
            bool cursorChanged = false;

            foreach (SelectBoxHandle sbh in handles)
            {
                if (sbh.HitTest(e.X, e.Y))
                {
                    parent.Cursor = sbh.Cursor;
                    cursorChanged = true;
                }
            }

            if (!cursorChanged)
            {
                parent.Cursor = Cursors.Default;
            }

            if (activeHandle != null)
            {
                activeHandle.OnDragging(e);
            }
        }

        public virtual void OnMouseDown(MouseEventArgs e)
        {
            foreach (SelectBoxHandle sbh in handles)
            {
                if (sbh.HitTest(e.X, e.Y))
                {
                    activeHandle = sbh;
                }
            }

            if (activeHandle != null)
            {
                activeHandle.OnDragStart(e);
            }
        }

        public virtual void OnMouseUp(MouseEventArgs e)
        {
            if (activeHandle != null)
            {
                activeHandle.OnDragEnd(e);
                activeHandle = null;

                if (OnBoxChanged != null)
                    OnBoxChanged(this, null);
            }
        }

        public Control Parent
        {
            get { return parent; }
        }

        public event EventHandler OnBoxChanged;
    }
}

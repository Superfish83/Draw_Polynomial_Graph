namespace DrawPolynomialGraph
{
    struct Polnom
    {
        public double[] coef;
        public int degree;

        public Polnom()
        {
            this.coef = new double[30];
            this.degree = -1;
        }

        public double F(double x)
        {
            double result = 0;
            for(int i = 0; i <= degree; i++)
            {
                double tmp = coef[i];
                for(int j = 0; j < i; j++)
                {
                    tmp *= x;
                }
                result += tmp;
            }
            return result;
        }
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Polnom pol;
        Polnom pol_deriv;
        Polnom pol_tangent;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Set Polynomial Function
            pol = new Polnom();
            pol.degree = 9;
            pol.coef[1] = 1;
            pol.coef[2] = 1;
            pol.coef[3] = -1;
            pol.coef[4] = 2;
            pol.coef[6] = -4;
            pol.coef[7] = 1;
            pol.coef[8] = 1;
            pol.coef[9] = -1;

            pol_deriv = GetDerivative(pol);

            pol_tangent = new Polnom();

            panel1.Invalidate();
        }

        private Polnom GetDerivative(Polnom pol_org)
        {
            Polnom deriv = new Polnom();
            if(pol_org.degree == 0)
            {
                deriv.degree = 0;
            }
            else
            {
                deriv.degree = pol_org.degree - 1;
                for(int i = 0; i <= deriv.degree; i++)
                {
                    deriv.coef[i] = (i + 1) * pol_org.coef[i+1];
                }
            }
            return deriv;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            g.DrawLine(Pens.Black, 0, 300, 800, 300);
            g.DrawLine(Pens.Black, 400, 0, 400, 600);

            for (double i = -10; i < 10; i+=0.1)
            {
                Point ps = new Point((int)(i * 40), (int)(pol.F(i) * 40));
                Point pe = new Point((int)((i+0.1) * 40), (int)(pol.F(i+0.1) * 40));
                try
                {
                    g.DrawLine(Pens.Red, transform(ps), transform(pe));
                }
                catch { }
            }

            if (pol_deriv.degree >= 0)
            {
                for (double i = -10; i < 10; i += 0.1)
                {
                    Point ps = new Point((int)(i * 40), (int)(pol_deriv.F(i) * 40));
                    Point pe = new Point((int)((i + 0.1) * 40), (int)(pol_deriv.F(i + 0.1) * 40));
                    try
                    {
                        g.DrawLine(Pens.Blue, transform(ps), transform(pe));
                    }
                    catch { }
                }
            }

            if (pol_tangent.degree >= 0)
            {
                for (double i = -10; i < 10; i += 0.1)
                {
                    Point ps = new Point((int)(i * 40), (int)(pol_tangent.F(i) * 40));
                    Point pe = new Point((int)((i + 0.1) * 40), (int)(pol_tangent.F(i + 0.1) * 40));
                    try
                    {
                        g.DrawLine(Pens.Purple, transform(ps), transform(pe));
                    }
                    catch { }
                }
            }
        }
        private Point transform(Point org)
        {
            Point result = new Point();

            result.X = org.X + 400;
            result.Y = -org.Y + 300;

            return result;
        }

        private void tb_Tangent_TextChanged(object sender, EventArgs e)
        {
            if (tb_Tangent.Text == "") return;

            double a = double.Parse(tb_Tangent.Text);
            pol_tangent.degree = 1;
            pol_tangent.coef[0] = pol.F(a) - (a * pol_deriv.F(a));
            pol_tangent.coef[1] = pol_deriv.F(a);

           // MessageBox.Show(pol_tangent.F(0).ToString());

            panel1.Invalidate();
        }
    }
}
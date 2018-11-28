using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinMaths.src.bean.function
{
    public abstract class Function
    {
        public double ParamA { get; set; }
        public double ParamB { get; set; }
        public double ParamC { get; set; }
        public string Formula { get; set; }

        public abstract double CalculateF(double x);
    }

    // Si no se pone static despues del new no lo coge en otras clases 

    public class SenXFunction : Function
    {
        public SenXFunction(double a, double b)
        {
            this.ParamA = a;
            this.ParamB = b;
            this.ParamC = -1;
        }

        public new static string Formula
        {
            get { return "a*sen(b*x)"; }
        }

        public override string ToString()
        {
            return String.Format("{0}*sen({1}*x)", this.ParamA, this.ParamB);
        }

        public override double CalculateF(double x)
        {
            return this.ParamA * Math.Sin(this.ParamB*x);
        }
    }

    public class CosXFunction : Function
    {
        public CosXFunction(double a, double b)
        {
            this.ParamA = a;
            this.ParamB = b;
            this.ParamC = -1;
        }

        public new static string Formula
        {
            get { return "a*cos(b*x)"; }
        }

        public override string ToString()
        {
            return String.Format("{0}*cos({1}*x)", this.ParamA, this.ParamB);
        }

        public override double CalculateF(double x)
        {
            return this.ParamA * Math.Cos(this.ParamB * x);
        }
    }


    public class ExponentialFunction : Function
    {
        public ExponentialFunction(double a, double b)
        {
            this.ParamA = a;
            this.ParamB = b;
            this.ParamC = -1;
        }

        public new static string Formula
        {
            get { return "a*x^b"; }
        }

        public override string ToString()
        {
            return String.Format("{0}*x^{1}", this.ParamA, this.ParamB);
        }

        public override double CalculateF(double x)
        {
            return this.ParamA * Math.Pow(x, this.ParamB);
        }
    }

    public class FirstGradeFunction : Function
    {
        public FirstGradeFunction(double a, double b)
        {
            this.ParamA = a;
            this.ParamB = b;
            this.ParamC = -1;
        }

        public new static string Formula
        {
            get { return "a*x+b"; }
        }

        public override string ToString()
        {
            return String.Format("{0}*x+{1}", this.ParamA, this.ParamB);
        }

        public override double CalculateF(double x)
        {
            return this.ParamA * x + this.ParamB;
        }
    }

    public class SecondGradeFunction : Function
    {
        public SecondGradeFunction(double a, double b, double c)
        {
            this.ParamA = a;
            this.ParamB = b;
            this.ParamC = c;
        }

        public new static string Formula
        {
            get { return "a*x^2+b*x+c"; }
        }

        public override string ToString()
        {
            return String.Format("{0}*x^2+{1}*x+{2}", this.ParamA, this.ParamB, this.ParamC);
        }

        public override double CalculateF(double x)
        {
            return this.ParamA * Math.Pow(x,2) + this.ParamB * x + this.ParamC;
        }
    }

    public class FractionalFunction : Function
    {
        public FractionalFunction(double a, double b)
        {
            this.ParamA = a;
            this.ParamB = b;
            this.ParamC = -1;
        }

        public new static string Formula
        {
            get { return "a/(b*x)"; }
        }

        public override string ToString()
        {
            return String.Format("{0}/({1}*x)", this.ParamA, this.ParamB);
        }

        public override double CalculateF(double x)
        {
            return this.ParamA / (this.ParamB * x);
        }
    }

}

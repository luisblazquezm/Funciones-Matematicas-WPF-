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
    }

    // Si no se pone static despues del new no lo coge en otras clases 

    public class SenXFunction : Function
    {
        public SenXFunction(double a, double b)
        {
            this.ParamA = a;
            this.ParamB = b;
        }

        public new static string Formula
        {
            get { return "a*sen(b*x)"; }
        }
    }

    public class CosXFunction : Function
    {
        public CosXFunction(double a, double b)
        {
            this.ParamA = a;
            this.ParamB = b;
        }

        public new static string Formula
        {
            get { return "a*cos(b*x)"; }
        }
    }


    public class ExponentialFunction : Function
    {
        public ExponentialFunction(double a, double b)
        {
            this.ParamA = a;
            this.ParamB = b;
        }

        public new static string Formula
        {
            get { return "a*x^b"; }
        }
    }

    public class FirstGradeFunction : Function
    {
        public FirstGradeFunction(double a, double b)
        {
            this.ParamA = a;
            this.ParamB = b;
        }

        public new static string Formula
        {
            get { return "a*x+b"; }
        }
    }

    public class SecondGradFunction : Function
    {
        public SecondGradFunction(double a, double b, double c)
        {
            this.ParamA = a;
            this.ParamB = b;
            this.ParamC = c;
        }

        public new static string Formula
        {
            get { return "a*x^2+b*x+c"; }
        }
    }

    public class FractionalFunction : Function
    {
        public FractionalFunction(double a, double b)
        {
            this.ParamA = a;
            this.ParamB = b;
        }

        public new static string Formula
        {
            get { return "a/(b*x)"; }
        }
    }

}
